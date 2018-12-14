import { Injectable } from '@angular/core';
import { Router, CanActivate, ActivatedRouteSnapshot, RouterStateSnapshot } from '@angular/router';
import { UserShared } from '../_shared';

@Injectable()
export class AuthGuard implements CanActivate {

    constructor(private router: Router, private userShared: UserShared) { }

    canActivate(route: ActivatedRouteSnapshot, state: RouterStateSnapshot) {        
        //console.log("AuthGuard=" + this.userShared.isUserLoggedIn());
        if (this.userShared.isUserLoggedIn()) { // logged in so return true
            return true;
        } else { // not logged in so redirect to login page with the return url
            //, { queryParams: { returnUrl: state.url } }
            this.router.navigate(['/login']);
            return false;
        }
    }
}
