import React, { useContext } from 'react'
import { Form as FinalForm, Field } from 'react-final-form'
import { Form, Button, Header } from 'semantic-ui-react'
import TextInput from '../../app/common/form/TextInput'
import { RootStoreContext } from '../../app/stores/rootStore'
import { IUserFormValues } from '../../app/models/user'
import { FORM_ERROR } from 'final-form'
import { combineValidators, isRequired } from 'revalidate'
import ErrorMessage from '../../app/common/form/ErrorMessage'

const validate = combineValidators({
    userName: isRequired('User Name'),
    displayName: isRequired('Display Name'),
    email: isRequired('Email'),
    password: isRequired('Password')
})

const RegisterForm = () => {
    const rootStore = useContext(RootStoreContext)
    const { register } = rootStore.userStore

    return (
        <FinalForm validate={validate} onSubmit={((values: IUserFormValues) => register(values).catch((error) => ({
            [FORM_ERROR]: error
        })))} render={({ handleSubmit, submitting, submitError, invalid, pristine, dirtySinceLastSubmit }) => (
            <Form onSubmit={handleSubmit} error>
                <Header as='h2' content='Signup to Reactivities' color='teal' textAlign='center' />
                <Field name='userName' component={TextInput} placeholder='User Name' />
                <Field name='displayName' component={TextInput} placeholder='DisplayName' />
                <Field name='email' component={TextInput} placeholder='Email' />
                <Field name='password' component={TextInput} placeholder='Password' type='password' />
                {submitError && !dirtySinceLastSubmit && <ErrorMessage error={submitError} />}
                <Button fluid disabled={(invalid && !dirtySinceLastSubmit) || pristine} loading={submitting} color='teal' content="Register" type='submit' />
            </Form>
        )} />
    )
}

export default RegisterForm
