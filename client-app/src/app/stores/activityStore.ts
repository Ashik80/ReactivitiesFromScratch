import { observable, action, computed, configure, runInAction } from 'mobx'
import { SyntheticEvent } from 'react'
import { IActivity } from '../models/activity'
import agent from '../api/agent'
import { format, addHours } from 'date-fns'
import { history } from '../..'
import { toast } from 'react-toastify'
import { RootStore } from './rootStore'
import { setActivityProps, createAttendee } from '../common/util/util'

configure({ enforceActions: 'always' })

export default class ActivityStore {
    rootStore: RootStore;
    constructor(rootStore: RootStore) {
        this.rootStore = rootStore
    }

    @observable activityRegistry = new Map()
    @observable selectedActivity: IActivity | null = null
    @observable loadingInitial = false
    @observable submitting = false
    @observable target = ""
    @observable loading = false

    @computed get activitiesByDate() {
        return this.groupActivitiesByDate(Array.from(this.activityRegistry.values()))
    }

    groupActivitiesByDate = (activities: IActivity[]) => {
        const sortedActivities = activities.sort(
            (a, b) => a.date.getTime() - b.date.getTime()
        )
        return Object.entries(sortedActivities.reduce((activities, activity) => {
            const date = format(activity.date, 'do MMMM, yyyy')
            activities[date] = activities[date] ? [...activities[date], activity] : [activity]
            return activities
        }, {} as { [key: string]: IActivity[] }))
    }

    @action loadActivities = async () => {
        this.loadingInitial = true
        try {
            const activities = await agent.Activities.list()
            runInAction('loading activities', () => {
                activities.forEach(activity => {
                    setActivityProps(activity, this.rootStore.userStore.user!)
                    this.activityRegistry.set(activity.id, activity)
                })
                this.loadingInitial = false
            })
        }
        catch (error) {
            console.log(error)
            runInAction('loading activities error', () => this.loadingInitial = false)

        }
    }

    @action loadActivity = async (id: string) => {
        let activity = this.getActivity(id)
        if (activity) {
            this.selectedActivity = activity
            return activity
        }
        else {
            this.loadingInitial = true
            try {
                activity = await agent.Activities.details(id)
                runInAction(() => {
                    setActivityProps(activity, this.rootStore.userStore.user!)
                    this.selectedActivity = activity
                    this.activityRegistry.set(activity.id, activity)
                    this.loadingInitial = false
                })
                return activity
            } catch (error) {
                runInAction("Activity details error", () => {
                    this.loadingInitial = false
                })
                console.log(error)
            }
        }
    }

    @action clearActivity = () => {
        this.selectedActivity = null
    }

    getActivity = (id: string): IActivity => {
        return this.activityRegistry.get(id)
    }

    @action createActivity = async (activity: IActivity) => {
        this.submitting = true
        try {
            await agent.Activities.create(activity)
            const attendee = createAttendee(this.rootStore.userStore.user!)
            attendee.isHost = true
            let attendees = []
            attendees.push(attendee)
            activity.attendees = attendees
            activity.date = addHours(activity.date, 6)
            activity.isHost = true
            runInAction('Creating activity', () => {
                this.activityRegistry.set(activity.id, activity)
                this.selectedActivity = activity
                this.submitting = false
            })
            history.push(`/activities/${activity.id}`)
        }
        catch (error) {
            console.log(error.response)
            runInAction('Create activity error', () => this.submitting = false)
            toast.error("Problem submitting data")
        }
    }

    @action editActivity = async (activity: IActivity) => {
        this.submitting = true
        try {
            await agent.Activities.update(activity)
            activity.date = addHours(activity.date, 6)
            runInAction('Editing activity', () => {
                this.activityRegistry.set(activity.id, activity)
                this.selectedActivity = activity
                this.submitting = false
            })
            history.push(`/activities/${activity.id}`)
        }
        catch (error) {
            console.log(error)
            runInAction('Edit activity error', () => {
                this.submitting = false
            })

        }
    }

    @action deleteActivity = async (event: SyntheticEvent<HTMLButtonElement>, id: string) => {
        this.submitting = true
        this.target = event.currentTarget.name
        try {
            await agent.Activities.delete(id)
            runInAction("Deleting activity", () => {
                this.activityRegistry.delete(id)
                this.submitting = false
            })
        }
        catch (error) {
            console.log(error)
            runInAction('Delete activity error', () => {
                this.submitting = false
                this.target = ''
            })
        }
    }

    @action attendActivity = async () => {
        const attendee = createAttendee(this.rootStore.userStore.user!)
        this.loading = true
        try {
            await agent.Activities.attend(this.selectedActivity!.id)
            runInAction(() => {
                if (this.selectedActivity) {
                    this.selectedActivity.attendees.push(attendee)
                    this.selectedActivity.isGoing = true
                    this.activityRegistry.set(this.selectedActivity.id, this.selectedActivity)
                    this.loading = false
                }
            })
        }
        catch (error) {
            runInAction(() => {
                this.loading = false
            })
            toast.error("Problem signing up to activity")
        }
    }

    @action cancelAttendence = async () => {
        this.loading = true
        try {
            await agent.Activities.unattend(this.selectedActivity!.id)
            runInAction(() => {
                if (this.selectedActivity) {
                    this.selectedActivity.attendees = this.selectedActivity.attendees.filter(a => a.userName !== this.rootStore.userStore.user!.userName)
                    this.selectedActivity.isGoing = false
                    this.loading = false
                }
            })
        }
        catch (error) {
            toast.error("Problem cancelling attendance")
            runInAction(() => {
                this.loading = false
            })
        }
    }
}