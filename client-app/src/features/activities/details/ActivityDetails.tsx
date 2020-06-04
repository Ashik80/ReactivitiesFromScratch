import React, { useContext, useEffect } from 'react'
import { Card, Image, Button } from 'semantic-ui-react'
import { observer } from 'mobx-react-lite'
import ActivityStore from '../../../app/stores/activityStore'
import { RouteComponentProps, Link } from 'react-router-dom'
import LoadingComponent from '../../../app/layout/LoadingComponent'

interface DetailsParam{
    id: string
}

const ActivityDetails: React.FC<RouteComponentProps<DetailsParam>> = ({match, history}) => {
    const activityStore = useContext(ActivityStore)
    const { selectedActivity, loadActivity, loadingInitial } = activityStore

    useEffect(() => {
        loadActivity(match.params.id)
    },[loadActivity, match.params.id])

    if(loadingInitial) return <LoadingComponent content="Loading Activity..." />

    return (
        <Card fluid>
            <Image src={`/assets/Images/categoryImages/${selectedActivity?.category}.jpg`} wrapped ui={false} />
            <Card.Content>
                <Card.Header>{selectedActivity?.title}</Card.Header>
                <Card.Meta>
                    <span>{selectedActivity?.date}</span>
                </Card.Meta>
                <Card.Description>
                    {selectedActivity?.description}
                </Card.Description>
            </Card.Content>
            <Card.Content extra>
                <Button.Group fluid>
                    <Button as={Link} to={`/manage/${selectedActivity?.id}`} basic color="blue" content="Edit" />
                    <Button onClick={() => history.push('/activities')} basic color="grey" content="Cancel" />
                </Button.Group>
            </Card.Content>
        </Card>
    )
}

export default observer(ActivityDetails)
