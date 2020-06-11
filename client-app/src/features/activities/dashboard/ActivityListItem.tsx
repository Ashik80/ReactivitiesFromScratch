import React from 'react'
import { Item, Button, Segment, Icon, Label } from 'semantic-ui-react'
import { Link } from 'react-router-dom'
import { IActivity } from '../../../app/models/activity'
import { observer } from 'mobx-react-lite'
import { format } from 'date-fns'
import ActivityListItemAttendees from './ActivityListItemAttendees'
import { addHours } from 'date-fns/esm'

interface IProps {
    activity: IActivity
}

const ActivityListItem: React.FC<IProps> = ({ activity }) => {
    const host = activity.attendees.filter(a => a.isHost)[0]

    return (
        <Segment.Group>
            <Segment>
                <Item.Group>
                    <Item>
                        <Item.Image size='tiny' circular src={host.image || '/assets/Images/user.png'} />
                        <Item.Content>
                            <Item.Header as={Link} to={`/activities/${activity.id}`}>{activity.title}</Item.Header>
                            <Item.Description>
                                Hosted by {host.displayName}
                            </Item.Description>
                            {activity.isHost && (
                                <Item.Description>
                                    <Label
                                        basic
                                        color='orange'
                                        content='You are hosting this activity'
                                    />
                                </Item.Description>
                            )}
                            {activity.isGoing && !activity.isHost && (
                                <Item.Description>
                                    <Label
                                        basic
                                        color='green'
                                        content='You are going to this activity'
                                    />
                                </Item.Description>
                            )}
                        </Item.Content>
                    </Item>
                </Item.Group>
            </Segment>
            <Segment>
                <Icon name='clock' />{format(addHours(activity.date, 6), 'h:mm a')}
                <Icon name='marker' />{activity.venue}, {activity.city}
            </Segment>
            <Segment secondary>
                <ActivityListItemAttendees attendees={activity.attendees} />
            </Segment>
            <Segment clearing>
                <span>{activity.description}</span>
                <Button as={Link} to={`/activities/${activity.id}`} floated="right" color="blue" content="View" />
            </Segment>
        </Segment.Group>
    )
}

export default observer(ActivityListItem)
