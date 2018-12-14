import { Component, OnInit } from '@angular/core';
import { Router, ActivatedRoute } from '@angular/router';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { first } from 'rxjs/operators';

import { AlertService, AuthenticationService } from '../_services';
import { UserShared } from '../_shared';
import { AppConstants } from '../app.constant';

@Component({ templateUrl: 'login.component.html' })
export class LoginComponent implements OnInit {
    loginForm: FormGroup;
    loading = false;
    submitted = false;
    returnUrl: string;
    isAdminMode = false;

    constructor(
        private formBuilder: FormBuilder,
        private route: ActivatedRoute,
        private router: Router,
        private authenticationService: AuthenticationService,
        private userShared: UserShared,
        private alertService: AlertService) { }

    ngOnInit() {
        this.loginForm = this.formBuilder.group({
            username: ['', Validators.required],
            password: ['', Validators.required]
        });

        // reset login status
        this.authenticationService.logout();

        this.isAdminMode = this.route.snapshot.routeConfig.path == AppConstants.adminLoginComponentPath;

        // get return url from route parameters or default to '/'
        //this.returnUrl = this.route.snapshot.queryParams['returnUrl'];
        //console.log(this.returnUrl);
        if (this.returnUrl == undefined || this.returnUrl == "/" || this.returnUrl == "") {
            this.returnUrl = this.isAdminMode ? AppConstants.userListComponentPath : AppConstants.homeComponentPath ;
        }//this.route.snapshot.queryParams['returnUrl'] || '/';
    }

    // convenience getter for easy access to form fields
    get f() { return this.loginForm.controls; }

    onSubmit() {
        this.submitted = true;

        // stop here if form is invalid
        if (this.loginForm.invalid) {
            return;
        }

        this.loading = true;
        this.authenticationService.login(this.f.username.value, this.f.password.value, this.isAdminMode)
            .pipe(first())
            .subscribe(
                data => {
                    //set true if user logged in from admin screen
                    this.userShared.setAdminLoginFlag(this.isAdminMode);

                    //console.log(this.returnUrl);
                    this.router.navigate([this.returnUrl]);
                },
                error => {
                    //this.alertService.error(error);
                    this.loading = false;
                });
    }
}
