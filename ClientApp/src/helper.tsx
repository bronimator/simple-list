import { AxiosResponse } from "axios";
import { IUser } from "./types/User.type";

export enum ModalType {
    CREATE,
    EDIT
}

export enum Status {
    LOADING,
    SUCCEEDED,
    FAILED,
}

export enum SourceType {
    DATABASE,
    FILE,
}

//Each child in a list should have a unique "key" prop for antd-table.
export function addKeyToList(list: AxiosResponse) {
    return list.data.map((x: IUser) => {
        x.key = x.id;
        return x;
    });
}

export function sortDescById(list: AxiosResponse) {
    list.data.sort((a: IUser, b: IUser) => b.id - a.id)
}