import React, { useState, useContext } from 'react'
import { Tab, Grid, Header, Button } from 'semantic-ui-react'
import ProfileEditForm from './form/ProfileEditForm'
import { RootStoreContext } from '../../app/stores/rootStore'
import { observer } from 'mobx-react-lite'
import { IProfile } from '../../app/models/profile'

const ProfileDescription = () => {
    const rootStore = useContext(RootStoreContext)
    const { editProfile, loading, profile, isCurrentUser } = rootStore.profileStore

    const [editMode, setEditMode] = useState(false)

    const handleFormSubmit = (values: IProfile) => {
        editProfile(values).then(() => setEditMode(false))
    }

    return (
        <Tab.Pane>
            <Grid>
                <Grid.Column width={16}>
                    <Header icon='user' content={`About ${profile?.displayName}`} floated='left' />
                    {isCurrentUser && <Button basic floated='right' disabled={loading} content={editMode ? 'Cancel' : 'Edit Profile'} onClick={() => setEditMode(!editMode)} />}
                </Grid.Column>
                <Grid.Column width={16}>
                    {editMode ? (
                        <ProfileEditForm profile={profile!} editForm={handleFormSubmit} loading={loading} />
                    ) : ( profile &&
                            <p>{profile.bio}</p>
                        )}
                </Grid.Column>
            </Grid>
        </Tab.Pane>
    )
}

export default observer(ProfileDescription)
