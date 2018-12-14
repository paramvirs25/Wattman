import { Component, OnInit } from '@angular/core';
import { first } from 'rxjs/operators';
import { Router } from '@angular/router';

import { UserDetail } from '../../_models';
import { UserService } from '../../_services';
import { UserShared } from '../../_shared';
import { AppConstants } from '../../app.constant';

@Component({
    selector: 'app-admin-layout',
    templateUrl: './admin-layout.component.html',
    styleUrls: ['./admin-layout.component.css']
})
export class AdminLayoutComponent implements OnInit {

    currentUser: UserDetail;
    passloggedinUser = true;

    constructor(private userService: UserService, private router: Router, private userShared: UserShared) { }
    
    ngOnInit() {
        this.loadUser();
    }

    private loadUser() {
        this.userService.getLoggedIn()
            .pipe(first())
            .subscribe(
                user => {
                    this.currentUser = user;
                });
    }

    // Go To Admin Login Page
    goAdminLoginPage() {
        this.router.navigate(['/', AppConstants.adminLoginComponentPath]);
    }    
}
