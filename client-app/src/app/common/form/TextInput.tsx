import React from 'react'
import { FieldRenderProps } from 'react-final-form'
import { FormFieldProps, Form, Label } from 'semantic-ui-react'

interface IProps extends FieldRenderProps<string, HTMLInputElement>, FormFieldProps {}

const TextInput: React.FC<IProps> = ({ input, width, placeholder, meta: { error, touched } }) => {
    return (
        <Form.Field error={touched && !!error} width={width}>
            <input {...input} placeholder={placeholder} />
            {(touched && error) && (
                <Label basic color='red'>
                    {error}
                </Label>
            )}
        </Form.Field>
    )
}

export default TextInput
