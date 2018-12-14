import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';

import { environment } from '../../environments/environment';
import { Roles } from '../_models';

import { Observable, of } from 'rxjs';

@Injectable()
export class RolesService {
    apiUrl: string;

    constructor(private http: HttpClient) {
        this.apiUrl = `${environment.apiUrl}/roles`;
    }

    getRoles(): Observable<Roles[]> {
        return this.http.get<Roles[]>(`${this.apiUrl}`);
    }
}
