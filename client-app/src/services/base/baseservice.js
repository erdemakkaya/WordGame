import  ErrorNotification from'../../components/Notification/WordNotification'
import axios from 'axios';


const sleep = (milliseconds) => {
    return new Promise(resolve => setTimeout(resolve, milliseconds))
  }

axios.interceptors.response.use(undefined, error => {
    if (error.message === 'Network Error' && !error.response) {
        ErrorNotification('Network error - make sure API is running!')
    }
    const {status, data, config} = error.response;
    if (status === 404) {
        ErrorNotification('Not Found Any Word')
    }
    if (status === 400 && config.method === 'get' && data.errors.hasOwnProperty('id')) {
        ErrorNotification('Not Found Any Word')
    }
    if (status === 500) {
        ErrorNotification('Server error - check the terminal for more info!')
    }
    throw error.response;
})



axios.defaults.baseURL = 'http://localhost:5000/api/';
// axios.paramSerializer = (params) => {
//     return qs.stringify(params, {
//         encode: false
//       });
// }

const Requests = {
    get: (url) => axios.get(url).then(sleep(1000)).then(response=>response.data),
    post: (url, body) => axios.post(url, body,{
        headers: { 'Content-Type': 'application/json' }
    }).then(sleep(1000)).then(response=>response.data),
    postFile: (url, body) => axios.post(url, body,{
        headers: { 'Content-Type': 'multipart/form-data' }
    }).then(sleep(1000)).then(response=>response.data),
    put: (url, body) => axios.put(url, body).then(sleep(1000)).then(response=>response.data),
    del: (url) => axios.delete(url).then(sleep(1000)).then(response=>response.data) 
    
};

export default Requests;