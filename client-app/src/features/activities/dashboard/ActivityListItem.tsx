import React from 'react'
import { Item, Button, Segment, Icon } from 'semantic-ui-react'
import { Link } from 'react-router-dom'
import { IActivity } from '../../../app/models/activity'
import { observer } from 'mobx-react-lite'
import {format} from 'date-fns'

interface IProps {
    activity: IActivity
}

const ActivityListItem: React.FC<IProps> = ({ activity }) => {
    return (
        <Segment.Group>
            <Segment>
                <Item.Group>
                    <Item>
                        <Item.Image size='tiny' circular src='/assets/Images/user.png' />
                        <Item.Content>
                            <Item.Header as='a'>{activity.title}</Item.Header>
                            <Item.Description>
                                Hosted by Ashik
                        </Item.Description>
                        </Item.Content>
                    </Item>
                </Item.Group>
            </Segment>
            <Segment>
                <Icon name='clock' />{format(activity.date, 'h:mm a')}
                <Icon name='marker' />{activity.venue}, {activity.city}
            </Segment>
            <Segment secondary>
                Attendees will go here
            </Segment>
            <Segment clearing>
                <span>{activity.description}</span>
                <Button as={Link} to={`/activities/${activity.id}`} floated="right" color="blue" content="View" />
            </Segment>
        </Segment.Group>
    )
}

export default observer(ActivityListItem)
