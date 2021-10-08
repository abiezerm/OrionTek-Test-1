import baseService from './BaseService.js';

let httpHeaders = {
    headers: {
        Authorization: `Bearer ${window.localStorage.getItem('token')}`
    }
}

const clientService = {
    getAll: () => {
        return baseService.get('Client', httpHeaders);
    },
    create: async(client) => {
        return baseService.post('Client', client, httpHeaders);
    },
    update: async(client) => {
        return baseService.put('Client', client, httpHeaders);
    },
    delete: async(id) => {
        return baseService.delete('Client/' + id, httpHeaders);
    }
}

export default clientService;