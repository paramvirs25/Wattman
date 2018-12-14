import { Injectable } from '@angular/core';
import { Router, CanActivate, ActivatedRouteSnapshot, RouterStateSnapshot } from '@angular/router';
import { UserShared } from '../_shared';

@Injectable()
export class AdminGuard implements CanActivate {

    constructor(private router: Router, private userShared: UserShared) { }

    canActivate(route: ActivatedRouteSnapshot, state: RouterStateSnapshot) {
        //console.log("AdminGuard=" + this.userShared.isAdminLogIn())
        if (this.userShared.isAdminLogIn()) { // if logged in user is an admin so return true
            return true;
        } else {
            return false;
        }
    }
}
