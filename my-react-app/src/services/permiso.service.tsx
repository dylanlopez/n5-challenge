import axios from 'axios';
import { Permiso } from '../dtos/permiso';

const api = axios.create({
  baseURL: 'https://localhost:7045/api'
});

export const requestPermission = (permiso: Permiso) => {
  console.log('requestPermission');
  console.log(' permiso: ', permiso);
  return api.post<Permiso>('/Permiso', permiso);
};

export const modifyPermission = (id: number, permiso: Permiso) => {
  return api.put<Permiso>(`/Permiso/${id}`, permiso);
};

export const getPermissions = () => {
  console.log('getPermissions');
  const response = api.get<Permiso[]>('/Permiso');
  console.log(' response: ', response);
  return response;
};
