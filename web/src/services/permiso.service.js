import axios from 'axios';
// import https from 'https';

const api = axios.create({
  baseURL: 'https://localhost:7045/api',
  httpsAgent: new (require('https').Agent)({ rejectUnauthorized: false }),
});

export const requestPermission = (permission) => {
  return api.post('/permissions', permission);
};

export const modifyPermission = (id, permission) => {
  return api.put(`/permissions/${id}`, permission);
};

export const getPermissions = () => {
  return api.get('/permissions');
};
