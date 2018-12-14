import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { map } from 'rxjs/operators';
import { UserShared } from '../_shared';

import { environment } from '../../environments/environment';

@Injectable()
export class AuthenticationService {
    constructor(private http: HttpClient, private userShared: UserShared) { }

    login(username: string, password: string, checkAdminRole: boolean) {
        return this.http.post<any>(`${environment.apiUrl}/users/authenticate`, { username: username, password: password, checkAdminRole: checkAdminRole })
            .pipe(map(user => {
                // login successful if there's a jwt token in the response
                if (user && user.token) {
                    // store user details and jwt token to keep user logged in between page refreshes
                    this.userShared.setLoggedInUser(user);
                }

                return user;
            }));
    }

    logout() {
        // remove user from storage to log user out
        this.userShared.removeLoggedInUser();
    }
}
