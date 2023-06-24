import { IEntityDto } from './EntityDto.type';

export interface IUser extends IEntityDto {
    login: string;
    password: string;
    name: string;
    typeId: number;
    last_visit_date: Date | null;
    key?: number;
}