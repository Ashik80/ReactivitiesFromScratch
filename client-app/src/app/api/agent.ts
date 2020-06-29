import axios, { AxiosResponse } from 'axios'
import { IActivity } from '../models/activity'
import { history } from '../..'
import { toast } from 'react-toastify'
import { IUser, IUserFormValues } from '../models/user'
import { IProfile, IPhoto } from '../models/profile'

axios.defaults.baseURL = "http://localhost:5000/api"

axios.interceptors.request.use(config => {
    const token = window.localStorage.getItem('jwt')
    if(token) config.headers.Authorization = `Bearer ${token}`
    return config
}, error => {
    return Promise.reject(error)
})

axios.interceptors.response.use(undefined, error => {
    if(error.message === 'Network Error' && !error.response){
        toast.error("Network Error - Please try again later!")
    }
    const { config, status, data } = error.response
    if (status === 404) {
        history.push('/notfound')
    }
    if (status === 400 && config.method === 'get' && data.errors.hasOwnProperty('id')) {
        history.push('/notfound')
    }
    if (status === 500) {
        toast.error("Server error - check the terminal for more info!")
    }
    throw error.response
})

const responseBody = (response: AxiosResponse) => response.data

const sleep = (ms: number) => (response: AxiosResponse) =>
    new Promise<AxiosResponse>(resolve => setTimeout(() => resolve(response), ms))


const requests = {
    get: (url: string) => axios.get(url).then(sleep(1000)).then(responseBody),
    post: (url: string, body: {}) => axios.post(url, body).then(sleep(1000)).then(responseBody),
    put: (url: string, body: {}) => axios.put(url, body).then(sleep(1000)).then(responseBody),
    del: (url: string) => axios.delete(url).then(sleep(1000)).then(responseBody),
    postForm: (url: string, file: Blob) => {
        let formData = new FormData()
        formData.append('File', file)
        return axios.post(url, formData, {
            headers: {'Content-type': 'multipart/form-data'}
        }).then(responseBody)
    }
}

const Activities = {
    list: (): Promise<IActivity[]> => requests.get("/activity"),
    details: (id: string): Promise<IActivity> => requests.get(`/activity/${id}`),
    create: (activity: IActivity) => requests.post('/activity', activity),
    update: (activity: IActivity) => requests.put(`/activity/${activity.id}`, activity),
    delete: (id: string) => requests.del(`/activity/${id}`),
    attend: (id: string) => requests.post(`/activity/attend/${id}`, {}),
    unattend: (id: string) => requests.del(`/activity/unattend/${id}`)
}

const User = {
    current: (): Promise<IUser> => requests.get("/users"),
    login: (user: IUserFormValues): Promise<IUser> => requests.post('/users/login', user),
    register: (user: IUserFormValues): Promise<IUser> => requests.post('/users/register', user)
}

const Profiles = {
    get: (userName: string): Promise<IProfile> => requests.get(`/profiles/${userName}`),
    uploadPhoto: (photo: Blob): Promise<IPhoto> => requests.postForm('/photos', photo),
    setMainPhoto: (id: string) => requests.put(`/photos/setmain/${id}`, {}),
    deletePhoto: (id: string) => requests.del(`/photos/${id}`),
    edit: (userName: string, profile: IProfile) => requests.put(`/profiles/${userName}`, profile),
    follow: (userName: string) => requests.post(`/profiles/follow/${userName}`, {}),
    unfollow: (userName: string) =>  requests.del(`/profiles/follow/${userName}`),
    listFollowings: (userName: string, predicate: string): Promise<IProfile[]> => 
        requests.get(`/profiles/follow/${userName}?predicate=${predicate}`)
}

export default { Activities, User, Profiles }