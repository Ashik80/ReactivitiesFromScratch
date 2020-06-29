import React, { useContext, useEffect } from 'react'
import { Grid } from 'semantic-ui-react'
import ProfileHeader from './ProfileHeader'
import ProfileContent from './ProfileContent'
import { RootStoreContext } from '../../app/stores/rootStore'
import { RouteComponentProps } from 'react-router-dom'
import { observer } from 'mobx-react-lite'
import LoadingComponent from '../../app/layout/LoadingComponent'

interface IProps {
    username: string
}

const ProfilePage: React.FC<RouteComponentProps<IProps>> = ({ match }) => {
    const rootStore = useContext(RootStoreContext)
    const { 
        loadProfile, 
        loadingProfile, 
        profile, 
        follow, 
        unfollow, 
        isCurrentUser, 
        loading,
        setActiveTab
    } = rootStore.profileStore

    useEffect(() => {
        loadProfile(match.params.username)
    }, [loadProfile, match.params.username])

    if (loadingProfile) return <LoadingComponent content="Loading Profile..." />

    return (
        <Grid>
            <Grid.Column width={16}>
                <ProfileHeader
                    profile={profile}
                    follow={follow}
                    unfollow={unfollow}
                    isCurrentUser={isCurrentUser}
                    loading={loading}
                />
                <ProfileContent setActiveTab={setActiveTab} />
            </Grid.Column>
        </Grid>
    )
}

export default observer(ProfilePage)
