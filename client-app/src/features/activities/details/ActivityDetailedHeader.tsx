import React, { useContext } from 'react'
import { Segment, Item, Header, Image, Button } from 'semantic-ui-react'
import { IActivity } from '../../../app/models/activity';
import { observer } from 'mobx-react-lite';
import { Link } from 'react-router-dom';
import { format } from 'date-fns';
import { RootStoreContext } from '../../../app/stores/rootStore';

const activityImageStyle = {
    filter: 'brightness(30%)'
};

const activityImageTextStyle = {
    position: 'absolute',
    bottom: '5%',
    left: '5%',
    width: '100%',
    height: 'auto',
    color: 'white'
};

const ActivityDetailedHeader: React.FC<{ activity: IActivity }> = ({ activity }) => {
    const rootStore = useContext(RootStoreContext)
    const { attendActivity, cancelAttendence, loading } = rootStore.activityStore
    const host = activity.attendees.filter(a => a.isHost)[0]

    return (
        <Segment.Group>
            <Segment basic attached='top' style={{ padding: '0' }}>
                <Image style={activityImageStyle} src={`/assets/Images/categoryImages/${activity.category}.jpg`} fluid />
                <Segment basic style={activityImageTextStyle}>
                    <Item.Group>
                        <Item>
                            <Item.Content>
                                <Header
                                    size='huge'
                                    content={activity.title}
                                    style={{ color: 'white' }}
                                />
                                <p>{format(activity.date, 'eeee do MMMM')}</p>
                                <p>
                                    Hosted by <Link to={`/profile/${host.displayName}`}><strong>Bob</strong></Link>
                                </p>
                            </Item.Content>
                        </Item>
                    </Item.Group>
                </Segment>
            </Segment>
            <Segment clearing attached='bottom'>
                {!activity.isHost && !activity.isGoing && (
                    <Button loading={loading} color='teal' onClick={attendActivity} >Join Activity</Button>
                )}
                {!activity.isHost && activity.isGoing && (
                    <Button loading={loading} onClick={cancelAttendence}>Cancel attendance</Button>
                )}
                {activity.isHost && (
                    <Button color='orange' floated='right' as={Link} to={`/manage/${activity.id}`}>
                        Manage Event
                    </Button>
                )}
            </Segment>
        </Segment.Group>
    )
}

export default observer(ActivityDetailedHeader)
