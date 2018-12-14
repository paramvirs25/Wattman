import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';

import { environment } from '../../environments/environment';
import { UserLogin, UserDetail, UserDetailsBaseAdmin, UserDetailBase } from '../_models';
import { UserList, UserCreateGet, UserEdit, UserSave, UserCreateSave } from '../_models/userModelExtensions';

import { Observable} from 'rxjs';

@Injectable()
export class UserService {
    apiUrl: string;

    constructor(private http: HttpClient) {
        this.apiUrl = `${environment.apiUrl}/users`;
    }

    //Gets a list of users
    getList(): Observable<UserList[]> {
        return this.http.get<UserList[]>(`${this.apiUrl}/list`);
    }

    //Gets user by Id
    getById(id: number): Observable<UserDetail> {
      return this.http.get<UserDetail>(`${this.apiUrl}/` + id);
    }

    //Get logged in user
    getLoggedIn(): Observable<UserDetail> {
        return this.http.get<UserDetail>(`${this.apiUrl}/getLoggedIn/`);
    }

    //Gets data for 'Create' user screen
    getForCreate(): Observable<UserCreateGet> {
        return this.http.get<UserCreateGet>(`${this.apiUrl}/getForCreate/`);
    }

    //Gets user's detail for editing
    getForEdit(id: number): Observable<UserEdit> {
        return this.http.get<UserEdit>(`${this.apiUrl}/getForEdit/` + id);
    }

    //Get logged in user's details for editing
    getForEditLoggedIn(): Observable<UserEdit> {
        return this.http.get<UserEdit>(`${this.apiUrl}/getForEditLoggedIn/`);
    }

    //Creates a user if it already doesnot exits
    create(userCreateSave: UserCreateSave) {
        return this.http.post(`${this.apiUrl}/create`, userCreateSave);
    }

    //Updates any user's login details
    update(userLogin: UserLogin) {
        return this.http.post(`${this.apiUrl}/update`, userLogin);
    }

    //Updates any user's general details
    updateDetail(userDetailsBaseAdmin: UserDetailsBaseAdmin) {
        return this.http.post(`${this.apiUrl}/updateDetail`, userDetailsBaseAdmin);
    }

    //Updates logged in user login details
    updateLoggedIn(userLogin: UserLogin) {
        return this.http.post(`${this.apiUrl}/updateLoggedIn`, userLogin);
    }

    //Updates logged in user's general details
    updateDetailLoggedIn(userDetailBase: UserDetailBase) {
        return this.http.post(`${this.apiUrl}/updateDetailLoggedIn`, userDetailBase);
    }

    //Delete User by Id
    delete(id: number) {
        return this.http.delete(`${this.apiUrl}/` + id);
    }

    addEdit(userLogin: UserLogin, userDetails: UserDetail) {
        return this.http.post(`${this.apiUrl}/addEdit`, { user: userLogin, userDetail: userDetails });
    }
    save(userSave: UserSave) {
        return this.http.post(`${this.apiUrl}/save`, userSave);
    }
}
