import axios, { AxiosResponse } from 'axios'
import { IActivity } from '../models/activity'
import { history } from '../..'
import { toast } from 'react-toastify'

axios.defaults.baseURL = "http://localhost:5000/api"

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
})

const responseBody = (response: AxiosResponse) => response.data

const sleep = (ms: number) => (response: AxiosResponse) =>
    new Promise<AxiosResponse>(resolve => setTimeout(() => resolve(response), ms))


const requests = {
    get: (url: string) => axios.get(url).then(sleep(1000)).then(responseBody),
    post: (url: string, body: {}) => axios.post(url, body).then(sleep(1000)).then(responseBody),
    put: (url: string, body: {}) => axios.put(url, body).then(sleep(1000)).then(responseBody),
    del: (url: string) => axios.delete(url).then(sleep(1000)).then(responseBody)
}

const Activities = {
    list: (): Promise<IActivity[]> => requests.get("/activity"),
    details: (id: string) => requests.get(`/activity/${id}`),
    create: (activity: IActivity) => requests.post('/activity', activity),
    update: (activity: IActivity) => requests.put(`/activity/${activity.id}`, activity),
    delete: (id: string) => requests.del(`/activity/${id}`)
}

export default { Activities }