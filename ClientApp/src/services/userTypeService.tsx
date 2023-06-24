import http from './httpService';
import { AxiosResponse } from 'axios';
import AppConsts from '../appconst';
import { IUserType } from '../types/UserType.type';
import { addKeyToList } from '../helper';

export async function getAllUserTypes(): Promise<IUserType[]> {
    const result: AxiosResponse = await http.get(`${AppConsts.remoteServiceBaseUrl}/api/UserType`);

    return addKeyToList(result);
}
