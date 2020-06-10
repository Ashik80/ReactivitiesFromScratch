import React, { useContext, useEffect } from 'react'
import { Grid } from 'semantic-ui-react'
import { observer } from 'mobx-react-lite'
import { RouteComponentProps } from 'react-router-dom'
import LoadingComponent from '../../../app/layout/LoadingComponent'
import ActivityDetailedHeader from './ActivityDetailedHeader'
import ActivityDetailedInfo from './ActivityDetailedInfo'
import ActivityDetailedChat from './ActivityDetailedChat'
import ActivityDetailedSidebar from './ActivityDetailedSidebar'
import { RootStoreContext } from '../../../app/stores/rootStore'

interface DetailsParam{
    id: string
}

const ActivityDetails: React.FC<RouteComponentProps<DetailsParam>> = ({match}) => {
    const rootStore = useContext(RootStoreContext)
    const { selectedActivity, loadActivity, loadingInitial } = rootStore.activityStore

    useEffect(() => {
        loadActivity(match.params.id)
    },[loadActivity, match.params.id])

    if(loadingInitial) return <LoadingComponent content="Loading Activity..." />

    else if (!selectedActivity) return <h1>Not Found</h1>

    return (
        <Grid>
            <Grid.Column width={10}>
                <ActivityDetailedHeader activity={selectedActivity} />
                <ActivityDetailedInfo activity={selectedActivity} />
                <ActivityDetailedChat />
            </Grid.Column>
            <Grid.Column width={6}>
                <ActivityDetailedSidebar />
            </Grid.Column>
        </Grid>
    )
}

export default observer(ActivityDetails)
