import http from './httpService';
import { AxiosResponse } from 'axios';
import AppConsts from '../appconst';
import { IUser } from '../types/User.type';
import { IFilters } from '../types/Filters.type';
import { addKeyToList, sortDescById } from '../helper';

export async function getAll(): Promise<IUser[]> {
    const result: AxiosResponse = await http.get(`${AppConsts.remoteServiceBaseUrl}/api/User`);

    addKeyToList(result);
    sortDescById(result);

    return result.data;
}

export async function getFiltered(filters: IFilters): Promise<IUser[]> {
    const result: AxiosResponse = await http.post(`${AppConsts.remoteServiceBaseUrl}/api/User/filter`, filters);

    addKeyToList(result);
    sortDescById(result);

    return result.data;
}

export async function getEntity(id: number): Promise<IUser> {
    const result: AxiosResponse = await http.get(`${AppConsts.remoteServiceBaseUrl}/api/User/${id}`);

    return result.data;
}

export async function createEntity(entityDto: IUser): Promise<IUser[]> {
    const result: AxiosResponse = await http.post(`${AppConsts.remoteServiceBaseUrl}/api/User`, entityDto);

    addKeyToList(result);
    sortDescById(result);

    return result.data;
}

export async function updateEntity(entityDto: IUser): Promise<IUser[]> {
    const result: AxiosResponse = await http.put(`${AppConsts.remoteServiceBaseUrl}/api/User`, entityDto);

    addKeyToList(result);
    sortDescById(result);

    return result.data;
}

export async function deleteEntity(id: number): Promise<IUser[]> {
    const result: AxiosResponse = await http.delete(`${AppConsts.remoteServiceBaseUrl}/api/User/${id}`);

    addKeyToList(result);
    sortDescById(result);

    return result.data;
}