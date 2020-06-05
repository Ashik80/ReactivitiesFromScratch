import { observable, action, computed, configure, runInAction } from 'mobx'
import { createContext, SyntheticEvent } from 'react'
import { IActivity } from '../models/activity'
import agent from '../api/agent'

configure({ enforceActions: 'always' })

class ActivityStore {
    @observable activityRegistry = new Map()
    @observable selectedActivity: IActivity | undefined
    @observable loadingInitial = false
    @observable submitting = false
    @observable target = ""

    @computed get activitiesByDate() {
        return this.groupActivitiesByDate(Array.from(this.activityRegistry.values()))
    }

    groupActivitiesByDate = (activities: IActivity[]) => {
        const sortedActivities = activities.sort(
            (a, b) => Date.parse(a.date) - Date.parse(b.date)
        )
        return Object.entries(sortedActivities.reduce((activities, activity) => {
            const date = activity.date.split('T')[0]
            activities[date] = activities[date] ? [...activities[date], activity] : [activity]
            return activities
        }, {} as {[key: string]: IActivity[]}))
    }

    @action loadActivities = async () => {
        this.loadingInitial = true
        try {
            const activities = await agent.Activities.list()
            runInAction('loading activities', () => {
                activities.forEach(activity => {
                    activity.date = activity.date.split(".")[0]
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
        }
        else {
            this.loadingInitial = true
            try {
                activity = await agent.Activities.details(id)
                runInAction('Activity details', () => {
                    this.selectedActivity = activity
                    this.loadingInitial = false
                })
            } catch(error) {
                runInAction("Activity details error", () => {
                    this.loadingInitial = false
                })
                console.log(error)
            }
        }
    }

    @action clearActivity = () => {
        this.selectedActivity = undefined
    }

    getActivity = (id: string) => {
        return this.activityRegistry.get(id)
    }

    @action createActivity = async (activity: IActivity) => {
        this.submitting = true
        try {
            await agent.Activities.create(activity)
            runInAction('Creating activity', () => {
                this.activityRegistry.set(activity.id, activity)
                this.selectedActivity = activity
                this.submitting = false
            })
        }
        catch (error) {
            console.log(error)
            runInAction('Create activity error', () => this.submitting = false)

        }
    }

    @action editActivity = async (activity: IActivity) => {
        this.submitting = true
        try {
            await agent.Activities.update(activity)
            runInAction('Editing activity', () => {
                this.activityRegistry.set(activity.id, activity)
                this.selectedActivity = activity
                this.submitting = false
            })
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
}

export default createContext(new ActivityStore())