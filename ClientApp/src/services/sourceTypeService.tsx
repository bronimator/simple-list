import http from './httpService';
import { AxiosResponse } from 'axios';
import AppConsts from '../appconst';
import { SourceType } from '../helper';

export async function updateSourceType(sourceType: SourceType): Promise<any> {
    const result: AxiosResponse = await http.put(`${AppConsts.remoteServiceBaseUrl}/api/User/source`, { sourceType });

    return result;
}

export async function getSourceType(): Promise<any> {
    return await http.get(`${AppConsts.remoteServiceBaseUrl}/api/User/source`);
}