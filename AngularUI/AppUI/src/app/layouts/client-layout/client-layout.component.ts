import { Component, OnInit } from '@angular/core';
import { first } from 'rxjs/operators';
import { Router } from '@angular/router';

import { UserDetail } from '../../_models';
import { UserService } from '../../_services';
import { UserShared } from '../../_shared';
import { AppConstants } from '../../app.constant';

@Component({
    selector: 'app-client-layout',
    templateUrl: './client-layout.component.html',
    styleUrls: ['./client-layout.component.css']
})
export class ClientLayoutComponent implements OnInit {
    currentUser: UserDetail;

    constructor(private userService: UserService, private router: Router, private userShared: UserShared) { }

    ngOnInit() {
        this.loadUser();
    }

    private loadUser() {
        this.userService.getLoggedIn()
            //.pipe(first())
            .subscribe(
                user => {
                    this.currentUser = user;
                });
    }

    //deleteUser(id: number) {
    //  this.userService.delete(id).pipe(first()).subscribe(() => {
    //    this.loadAllUsers()
    //  });
    //}

    //private loadAllUsers() {
    //  this.userService.getAll().pipe(first()).subscribe(users => {
    //    this.users = users;
    //  });
    //}

    // Go To Client Login Page
    goClientLoginPage() {
        this.router.navigate(['/', AppConstants.clientLoginComponentPath]);
    }
    // Go To Home Page
    goHomePage(){
        this.router.navigate(['/', AppConstants.homeComponentPath]);
    }

}
