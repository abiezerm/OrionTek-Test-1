import axios from 'axios';

const baseService = axios.create({
    baseURL: 'https://localhost:5001/api/',
});

baseService.interceptors.request.use(
    (config) => {
        let token = localStorage.getItem('token');

        if (token) {
            config.headers['Authorization'] = `Bearer ${ token }`;
        }

        return config;
    },

    (error) => {
        return Promise.reject(error);
    }
);

export default baseService;