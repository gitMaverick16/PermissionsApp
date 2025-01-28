import axios from 'axios';

const API_URL_QUERY = 'http://localhost:5043/api/permission';
const API_URL_COMMAND = 'http://localhost:5112/api/permission';

export const getPermissions = () => axios.get(API_URL_QUERY);

export const getPermissionById = (id) => axios.get(`${API_URL_QUERY}/${id}`);

export const createPermission = (permission) => axios.post(`${API_URL_COMMAND}/create`, permission);

export const updatePermission = (id, permission) => axios.put(`${API_URL_COMMAND}/${id}`, permission);

export const deletePermission = (id) => axios.delete(`${API_URL_COMMAND}/${id}`);