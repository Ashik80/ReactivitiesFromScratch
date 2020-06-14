import { RootStore } from "./rootStore";
import { observable, action, runInAction, computed } from "mobx";
import { IProfile, IPhoto } from '../models/profile'
import agent from "../api/agent";
import { toast } from "react-toastify";

export default class ProfileStore {
    rootStore: RootStore
    constructor(rootStore: RootStore) {
        this.rootStore = rootStore
    }

    @observable profile: IProfile | null = null
    @observable loadingProfile = false
    @observable uploadingPhoto = false
    @observable loading = false

    @computed get isCurrentUser() {
        if (this.profile && this.rootStore.userStore.user) {
            return this.profile.userName === this.rootStore.userStore.user.userName
        }
        else {
            return false
        }
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
        try{
            await agent.Profiles.deletePhoto(photo.id)
            runInAction(() => {
                if(this.profile){
                    this.profile.photos = this.profile.photos.filter(p => p.id !== photo.id)
                    this.loading = false
                }
            })
        }
        catch(error){
            console.log(error)
            toast.error("Problem deleting photo")
            runInAction(() => {
                this.loading = false
            })
        }
    }
}