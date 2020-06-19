import React, { Fragment } from 'react'
import { Form, Button } from 'semantic-ui-react'
import { Form as FinalForm, Field } from 'react-final-form'
import TextInput from '../../../app/common/form/TextInput'
import TextAreaInput from '../../../app/common/form/TextAreaInput'
import { IProfile } from '../../../app/models/profile'
import { observer } from 'mobx-react-lite'
import { isRequired, combineValidators } from 'revalidate'

const validate = combineValidators({
    displayName: isRequired('Display name')
})

interface IProps {
    editForm: (profile: IProfile) => void,
    loading: boolean,
    profile: IProfile
}

const ProfileEditForm: React.FC<IProps> = ({editForm, loading, profile}) => {
    

    return (
        <Fragment>
            <FinalForm onSubmit={editForm} validate={validate} initialValues={profile}
                render={({handleSubmit, pristine}) => (
                    <Form onSubmit={handleSubmit}>
                        <Field name="displayName" placeholder='Display Name' component={TextInput} />
                        <Field rows={3} name="bio" placeholder='Display Name' component={TextAreaInput} />
                        <Button loading={loading} disabled={pristine} positive floated='right' content='Update Profile' type="submit" />
                    </Form>
                )}
            />
        </Fragment>
    )
}

export default observer(ProfileEditForm)
