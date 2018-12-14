import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';

import { environment } from '../../environments/environment';
import { UserTypes } from '../_models';

import { Observable, of } from 'rxjs';

@Injectable()
export class UserTypesService {
    apiUrl: string;

    constructor(private http: HttpClient) {
        this.apiUrl = `${environment.apiUrl}/usertypes`;
    }

    getUserTypes(): Observable<UserTypes[]> {
        return this.http.get<UserTypes[]>(`${this.apiUrl}`);
    }
}
