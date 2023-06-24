import { action, makeAutoObservable } from 'mobx';
import { createEntity, deleteEntity, getAll, getEntity, getFiltered, updateEntity } from '../services/userService';
import { getAllUserTypes } from '../services/userTypeService';
import { IUser } from '../types/User.type';
import { IUserType } from '../types/UserType.type';
import { IFilters } from '../types/Filters.type';
import { SourceType } from '../helper';
import { getSourceType, updateSourceType } from '../services/sourceTypeService';

export class TaskStore {
  users: IUser[] = [];
  userTypes: IUserType[] = [];
  editUser: IUser | null = null;
  isLoading = true;
  sourceType: SourceType | null = null;

  constructor() {
    makeAutoObservable(this);
  }

  load() {
    this.isLoading = true;

    Promise
      .all([getAll(), getAllUserTypes(), getSourceType()])
      .then(action(values => {
        this.users = values[0];
        this.userTypes = values[1];
        this.sourceType = values[2]?.data;
      }))
      .catch((e: any) => {
        alert(`Ошибка: ${e.message}`)
      })
      .finally(action(() => {
        this.isLoading = false;
      }));
  }

  async updateSource(source: SourceType) {
    this.isLoading = true;

    await updateSourceType(source)
      .then(action((result: boolean) => {
        this.load();
      }))
      .catch((e: any) => {
        alert(`Ошибка: ${e.message}`)
      })
      .finally(action(() => {
        this.isLoading = false;
      }));
  }

  async getUser(id: number) {
    this.isLoading = true;

    await getEntity(id)
      .then(action(result => {
        this.editUser = result;
      }))
      .catch((e: any) => {
        alert(`Ошибка: ${e.message}`)
      })
      .finally(action(() => {
        this.isLoading = false;
      }));
  }

  async createUser(user: IUser) {
    this.isLoading = true;

    createEntity(user)
      .then(action(result => {
        this.users = result;
      }))
      .catch((e: any) => {
        alert(`Ошибка: ${e.message}`)
      })
      .finally(action(() => {
        this.isLoading = false;
      }));
  }

  async updateUser(user: IUser) {
    this.isLoading = true;

    updateEntity(user)
      .then(action(result => {
        this.users = result;
      }))
      .catch((e: any) => {
        alert(`Ошибка: ${e.message}`)
      })
      .finally(action(() => {
        this.isLoading = false;
      }));
  }

  async getFilteredUsers(filters: IFilters) {
    this.isLoading = true;

    getFiltered(filters)
      .then(action(result => {
        this.users = result;
      }))
      .catch((e: any) => {
        alert(`Ошибка: ${e.message}`)
      })
      .finally(action(() => {
        this.isLoading = false;
      }));
  }

  async deleteUser(id: number) {
    this.isLoading = true;

    deleteEntity(id)
      .then(action(result => {
        this.users = result;
      }))
      .catch((e: any) => {
        alert(`Ошибка: ${e.message}`)
      })
      .finally(action(() => {
        this.isLoading = false;
      }));
  }

  createNewUser() {
    this.editUser = {
      id: 0,
      login: '',
      name: '',
      password: '',
      typeId: 0,
      last_visit_date: null,
    }
  }
}
