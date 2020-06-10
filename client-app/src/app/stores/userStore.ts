import { observable, computed, action, runInAction } from "mobx";
import { IUser, IUserFormValues } from "../models/user";
import agent from "../api/agent";
import { RootStore } from "./rootStore";
import { history } from "../..";

export default class UserStore {
    rootStore: RootStore
    constructor(rootStore: RootStore){
        this.rootStore = rootStore
    }

    @observable user: IUser | null = null;

    @computed get isLoggedIn() {
        return !!this.user
    }

    @action login = async (user: IUserFormValues) => {
        try{
            const myuser = await agent.User.login(user)
            runInAction(() => {
                this.user = myuser
                this.rootStore.commonStore.setToken(this.user.token)
                this.rootStore.modalStore.closeModal()
            })
            history.push('/activities')
        }
        catch(error){
            throw error
        }
    } 

    @action register = async (user: IUserFormValues) => {
        try{
            const myuser =  await agent.User.register(user)
            runInAction(() => {
                this.user = myuser
                this.rootStore.commonStore.setToken(this.user.token)
                this.rootStore.modalStore.closeModal()
            })
            history.push('/activities')
        }
        catch(error){
            throw error
        }
    }

    @action logout = () => {
        this.rootStore.commonStore.setToken(null)
        this.user = null
        history.push('/')
    }

    @action getUser = async () => {
        try{
            const user = await agent.User.current()
            runInAction(() => {
                this.user = user
            })
        }
        catch(error){
            console.log(error)
        }
    }
}