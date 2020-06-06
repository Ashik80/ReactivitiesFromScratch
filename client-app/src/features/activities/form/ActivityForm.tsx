import React, { useState, useContext, useEffect } from 'react'
import { Segment, Form, Button, Grid } from 'semantic-ui-react'
import { ActivityFormValues } from '../../../app/models/activity'
import { v4 as uuid } from 'uuid'
import ActivityStore from '../../../app/stores/activityStore'
import { observer } from 'mobx-react-lite'
import { RouteComponentProps } from 'react-router-dom'
import { Form as FinalForm, Field } from 'react-final-form'
import TextInput from '../../../app/common/form/TextInput'
import TextAreaInput from '../../../app/common/form/TextAreaInput'
import SelectInput from '../../../app/common/form/SelectInput'
import { category } from '../../../app/common/options/categoryOptions'
import DateInput from '../../../app/common/form/DateInput'
import { combineDateAndTime } from '../../../app/common/util/util'
import { combineValidators, isRequired, composeValidators, hasLengthGreaterThan } from 'revalidate'

const validate = combineValidators({
    title: isRequired({ message: 'Title is required' }),
    category: isRequired('Category'),
    description: composeValidators(
        isRequired('Description'),
        hasLengthGreaterThan(4)({ message: 'Description needs to be at least 5 characters' })
    )(),
    city: isRequired('City'),
    venue: isRequired('Venue'),
    date: isRequired('Date'),
    time: isRequired('Time')
})

interface DetailsParam {
    id: string
}

const ActivityForm: React.FC<RouteComponentProps<DetailsParam>> = ({ match, history }) => {
    const activityStore = useContext(ActivityStore)
    const { createActivity, editActivity, submitting, loadActivity } = activityStore

    const [activity, setActivity] = useState(new ActivityFormValues())
    const [loading, setLoading] = useState(false)

    useEffect(() => {
        if (match.params.id) {
            setLoading(true)
            loadActivity(match.params.id).then((activity) => setActivity(new ActivityFormValues(activity)))
                .finally(() => setLoading(false))
        }
    }, [loadActivity, match.params.id])

    const handleFinalFormSubmit = (values: any) => {
        const dateAndTime = combineDateAndTime(values.date, values.time)
        const { date, time, ...activity } = values
        activity.date = dateAndTime
        if (activity.id) {
            editActivity(activity)
        }
        else {
            let newActivity = {
                ...activity,
                id: uuid()
            }
            createActivity(newActivity)
        }
    }

    return (
        <Grid>
            <Grid.Column width={10}>
                <Segment clearing>
                    <FinalForm validate={validate} initialValues={activity} onSubmit={handleFinalFormSubmit} render={({ handleSubmit, invalid, pristine }) => (
                        <Form onSubmit={handleSubmit} loading={loading}>
                            <Field component={TextInput} name="title" placeholder="Title" value={activity.title} />
                            <Field component={TextAreaInput} rows={3} placeholder="Description" name="description" value={activity.description} />
                            <Field component={SelectInput} options={category} placeholder="Category" name="category" value={activity.category} />
                            <Form.Group widths='equal'>
                                <Field component={DateInput} date={true} time={false} placeholder="Date" name="date" value={activity.date} />
                                <Field component={DateInput} time={true} date={false} placeholder="Time" name="time" value={activity.date} />
                            </Form.Group>
                            <Field component={TextInput} placeholder="City" name="city" value={activity.city} />
                            <Field component={TextInput} placeholder="Venue" name="venue" value={activity.venue} />
                            <Button loading={submitting} disabled={loading || invalid || pristine} positive content="Submit" type="submit" floated="right" />
                            <Button content="Cancel" disabled={loading} onClick={() => (activity.id) ? history.push(`/activities/${activity.id}`) : history.push('/activities')} floated="right" />
                        </Form>
                    )} />
                </Segment>
            </Grid.Column>
        </Grid>
    )
}

export default observer(ActivityForm)
