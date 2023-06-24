import { IEntityDto } from "./EntityDto.type";

export interface IUserType extends IEntityDto {
    id: number;
    name: string;
    allow_edit: boolean;
    key?: number;
}