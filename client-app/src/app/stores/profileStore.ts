import { RootStore } from "./rootStore";
import { observable, action, runInAction, computed, reaction } from "mobx";
import { IProfile, IPhoto, IUserActivity } from '../models/profile'
import agent from "../api/agent";
import { toast } from "react-toastify";

export default class ProfileStore {
    rootStore: RootStore
    constructor(rootStore: RootStore) {
        this.rootStore = rootStore

        reaction(() => this.activeTab,
            activeTab => {
                if (activeTab === 3 || activeTab === 4) {
                    const predicate = activeTab === 3 ? "followers" : "followings"
                    this.loadFollowings(predicate)
                }
                else this.followings = []
            }
        )
    }

    @observable profile: IProfile | null = null
    @observable loadingProfile = false
    @observable uploadingPhoto = false
    @observable loading = false
    @observable followings: IProfile[] = []
    @observable activeTab: number = 0
    @observable userActivities: IUserActivity[] = []
    @observable loadingActivities = false

    @computed get isCurrentUser() {
        if (this.profile && this.rootStore.userStore.user) {
            return this.profile.userName === this.rootStore.userStore.user.userName
        }
        else {
            return false
        }
    }

    @action loadUserActivities = async (userName: string, predicate?: string) => {
        this.loadingActivities = true
        try {
            const activities = await agent.Profiles.listActivities(userName,predicate)
            runInAction(() => {
                this.userActivities = activities
            })
        } catch (error) {
            toast.error('Problem loading activities')
        } finally {
            runInAction(() => {
                this.loadingActivities = false
            })
        }
    }

    @action setActiveTab = (activeIndex: number) => {
        this.activeTab = activeIndex
    }

    @action loadProfile = async (userName: string) => {
        this.loadingProfile = true
        try {
            let profile = await agent.Profiles.get(userName)
            runInAction(() => {
                this.profile = profile
                this.loadingProfile = false
            })
        }
        catch (error) {
            console.log(error)
            runInAction(() => {
                this.loadingProfile = false
            })
        }
    }

    @action uploadPhoto = async (file: Blob) => {
        this.uploadingPhoto = true
        try {
            const photo = await agent.Profiles.uploadPhoto(file)
            runInAction(() => {
                if (this.profile) {
                    this.profile.photos.push(photo)
                    if (photo.isMain && this.rootStore.userStore.user) {
                        this.profile.image = photo.url
                        this.rootStore.userStore.user.image = photo.url
                    }
                }
                this.uploadingPhoto = false
            })
        }
        catch (error) {
            console.log(error)
            toast.error("Problem uploading photo")
            runInAction(() => {
                this.uploadingPhoto = false
            })
        }
    }

    @action setMainPhoto = async (photo: IPhoto) => {
        this.loading = true
        try {
            await agent.Profiles.setMainPhoto(photo.id)
            runInAction(() => {
                if (this.rootStore.userStore.user && this.profile) {
                    this.rootStore.userStore.user.image = photo.url
                    this.profile.photos.find(p => p.isMain)!.isMain = false
                    this.profile.photos.find(p => p.id === photo.id)!.isMain = true
                    this.profile.image = photo.url
                    this.loading = false
                }
            })
        }
        catch (error) {
            console.log(error)
            toast.error("Problem setting photo as main")
            runInAction(() => {
                this.loading = false
            })
        }
    }

    @action deletePhoto = async (photo: IPhoto) => {
        this.loading = true
        try {
            await agent.Profiles.deletePhoto(photo.id)
            runInAction(() => {
                if (this.profile) {
                    this.profile.photos = this.profile.photos.filter(p => p.id !== photo.id)
                    this.loading = false
                }
            })
        }
        catch (error) {
            console.log(error)
            toast.error("Problem deleting photo")
            runInAction(() => {
                this.loading = false
            })
        }
    }

    @action editProfile = async (profile: IProfile) => {
        this.loading = true
        try {
            await agent.Profiles.edit(this.rootStore.userStore.user!.userName, profile)
            runInAction(() => {
                if (this.profile && this.rootStore.userStore.user) {
                    this.profile.displayName = profile.displayName
                    this.rootStore.userStore.user.displayName = profile.displayName
                    this.profile.bio = profile.bio
                    this.loading = false
                }
            })
        }
        catch (error) {
            console.log(error)
            toast.error("Problem updating profile")
            runInAction(() => {
                this.loading = false
            })
        }
    }

    @action follow = async (userName: string) => {
        this.loading = true
        try {
            await agent.Profiles.follow(userName)
            runInAction(() => {
                if (this.profile) {
                    this.profile.following = true
                    this.profile.followersCount++
                }
            })
        } catch (error) {
            toast.error("Problem following user")
            console.log(error)
        } finally {
            runInAction(() => {
                this.loading = false
            })
        }
    }

    @action unfollow = async (userName: string) => {
        this.loading = true
        try {
            await agent.Profiles.unfollow(userName)
            runInAction(() => {
                if (this.profile) {
                    this.profile.following = false
                    this.profile.followersCount--
                }
            })
        } catch (error) {
            toast.error("Problem unfollowing user")
            console.log(error)
        } finally {
            runInAction(() => {
                this.loading = false
            })
        }
    }

    @action loadFollowings = async (predicate: string) => {
        this.loading = true
        try {
            const profiles = await agent.Profiles.listFollowings(this.profile!.userName, predicate)
            runInAction(() => {
                this.followings = profiles
            })
        } catch (error) {
            toast.error("Problem loading followers")
        } finally {
            runInAction(() => {
                this.loading = false
            })
        }
    }
}