import React, { useState, FormEvent, useContext, useEffect } from 'react'
import { Segment, Form, Button, Grid } from 'semantic-ui-react'
import { IActivity } from '../../../app/models/activity'
import { v4 as uuid } from 'uuid'
import ActivityStore from '../../../app/stores/activityStore'
import { observer } from 'mobx-react-lite'
import { RouteComponentProps } from 'react-router-dom'

interface DetailsParam {
    id: string
}

const ActivityForm: React.FC<RouteComponentProps<DetailsParam>> = ({ match, history }) => {
    const activityStore = useContext(ActivityStore)
    const { createActivity, editActivity, submitting, selectedActivity, loadActivity, clearActivity } = activityStore

    const [activity, setActivity] = useState<IActivity>({
        id: '',
        title: '',
        description: '',
        category: '',
        date: '',
        city: '',
        venue: ''
    })

    useEffect(() => {
        if (match.params.id && (activity.id.length === 0)) {
            loadActivity(match.params.id).then(() => selectedActivity && setActivity(selectedActivity))
        }

        return () => {
            clearActivity()
        }
    }, [loadActivity, match.params.id, selectedActivity, clearActivity, activity.id.length])

    const handleChange = (event: FormEvent<HTMLInputElement> | FormEvent<HTMLTextAreaElement>) => {
        const { name, value } = event.currentTarget
        setActivity({ ...activity, [name]: value })
    }

    const handleSubmit = () => {
        if (activity.id.length === 0) {
            let newActivity = {
                ...activity,
                id: uuid()
            }
            createActivity(newActivity).then(() => history.push(`/activities/${newActivity.id}`))
        }
        else {
            editActivity(activity).then(() => history.push(`/activities/${activity.id}`))
        }
    }

    return (
        <Grid>
            <Grid.Column width={10}>
                <Segment clearing>
                    <Form onSubmit={handleSubmit}>
                        <Form.Input onChange={handleChange} name="title" placeholder="Title" value={activity?.title} />
                        <Form.TextArea onChange={handleChange} rows={2} placeholder="Description" name="description" value={activity?.description} />
                        <Form.Input onChange={handleChange} placeholder="Category" name="category" value={activity?.category} />
                        <Form.Input onChange={handleChange} type="datetime-local" placeholder="Date" name="date" value={activity?.date} />
                        <Form.Input onChange={handleChange} placeholder="City" name="city" value={activity?.city} />
                        <Form.Input onChange={handleChange} placeholder="Venue" name="venue" value={activity?.venue} />
                        <Button loading={submitting} positive content="Submit" type="submit" floated="right" />
                        <Button content="Cancel" onClick={() => (activity.id.length !== 0) ? history.push(`/activities/${activity.id}`) : history.push('/activities')} floated="right" />
                    </Form>
                </Segment>
            </Grid.Column>
        </Grid>
    )
}

export default observer(ActivityForm)
