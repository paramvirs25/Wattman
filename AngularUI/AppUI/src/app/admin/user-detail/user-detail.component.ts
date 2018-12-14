import { Component, OnInit } from '@angular/core';
import { AlertService, UserService } from '../../_services';
import { ActivatedRoute } from '@angular/router';
import { Router } from '@angular/router';
import { FormControl, FormBuilder, FormGroup, Validators } from '@angular/forms';

import { Roles, UserTypes } from '../../_models';
import { AppConstants } from '../../app.constant';
import { AbstractControl } from '@angular/forms';
import { UserSave } from '../../_models/userModelExtensions';

@Component({
    selector: 'app-user-detail',
    templateUrl: './user-detail.component.html',
    styleUrls: ['./user-detail.component.css']
})
export class UserDetailComponent implements OnInit {

    constructor(
        private userService: UserService,
        private router: Router,
        private alertService: AlertService,
        private activeRoute: ActivatedRoute,
        private formBuilder: FormBuilder) { }

    roleOptions: Roles[];
    userTypeOptions: UserTypes[];
    userSave: UserSave;
    isLoadingResults = false;
    isSaving = false;

    userDetailsForm: FormGroup;
    submitted = false;

    userId = 0; //Add Mode
    lblAddEditUser: string;
    hasUserId = false;

    ngOnInit() {

        this.userId = +this.activeRoute.snapshot.paramMap.get('id');

        // Define validations and control names
        this.userDetailsForm = this.formBuilder.group({
            username: ['', Validators.required],
            password: ['', [Validators.required, Validators.minLength(4)]],
            confirmpassword: ['', [Validators.required, Validators.minLength(4)]],
            firstname: ['', Validators.required],
            lastname: ['', Validators.required],
            email: ['', [Validators.required, Validators.email]],
            ddrole: new FormControl(null),
            ddusertype: new FormControl(null)
        }, {
                validator: PasswordValidation.MatchPassword
            });

        //Add Mode
        if (this.userId == 0) {
            this.lblAddEditUser = "Add";
            this.isLoadingResults = true;

            //Get Data for Create Mode
            this.userService.getForCreate().subscribe(
                userCreate => {
                    //Initialise dropdowns
                    this.initRoles(userCreate.roles);
                    this.initUserTypes(userCreate.userTypes);

                    this.isLoadingResults = false;
                },
                error => {
                    this.isLoadingResults = false;
                    this.router.navigate(['/', AppConstants.userListComponentPath]);
                });
        } //Edit Mode
        else if (this.userId > 0) {
            this.hasUserId = true;
            this.lblAddEditUser = "Edit";
            this.isLoadingResults = true;

            this.userService.getForEdit(this.userId).subscribe(
                userEdit => {
                    console.log(userEdit);

                    this.f.username.setValue(userEdit.user.username);
                    this.f.password.setValue(userEdit.user.password);
                    this.f.confirmpassword.setValue(userEdit.user.password);

                    this.f.firstname.setValue(userEdit.userDetailsBaseAdmin.userFirstName);
                    this.f.lastname.setValue(userEdit.userDetailsBaseAdmin.userLastName);
                    this.f.email.setValue(userEdit.userDetailsBaseAdmin.userEmail);

                    //Initialise dropdowns
                    this.initRoles(userEdit.roles);
                    this.initUserTypes(userEdit.userTypes);

                    //Bind Dropdowns
                    this.f.ddrole.setValue(userEdit.userDetailsBaseAdmin.roleId);
                    this.f.ddusertype.setValue(userEdit.userDetailsBaseAdmin.userTypeId);

                    this.isLoadingResults = false;
                },
                error => {
                    this.isLoadingResults = false;
                    this.router.navigate(['/', AppConstants.userListComponentPath]);
                });
        }
    }

    // convenience getter for easy access to form fields
    get f() { return this.userDetailsForm.controls; }

    //save user
    save() {
        this.submitted = true;

        // stop here if form is invalid
        if (this.userDetailsForm.invalid) {
            return;
        }

        //Save user details
        this.userSave = new UserSave();

        this.userSave = new UserSave();
        this.userSave.userId = this.userId;
        this.userSave.username = this.f.username.value;
        this.userSave.password = this.f.password.value;

        this.userSave.userFirstName = this.f.firstname.value;
        this.userSave.userLastName = this.f.lastname.value;
        this.userSave.userEmail = this.f.email.value;
        this.userSave.roleId = this.f.ddrole.value;
        this.userSave.userTypeId = this.f.ddusertype.value;
       
        //console.log(this.userSave);

        this.isSaving = true;
        this.userService.save(this.userSave).subscribe(
            data => {
                this.alertService.success('Registration successful', true);
                //this.router.navigate(['/login']);
                this.router.navigate(['/', AppConstants.userListComponentPath]);
            },
            error => {
                //this.alertService.error(error);
                this.isSaving = false;
            });        

        //this.router.navigate(['/', AppConstants.userListComponentPath]);
    }

    // Initialise Dropdown Roles
    initRoles(roles: Roles[]) {
        this.roleOptions = roles;
        this.f.ddrole.setValue(this.roleOptions[0].roleId); // Set Default value
    }

    // Initialise Dropdown UserTypes
    initUserTypes(userTypes: UserTypes[]) {
        this.userTypeOptions = userTypes;
        this.f.ddusertype.setValue(this.userTypeOptions[0].userTypeId); // Set Default value
    }

    // Go To Add Users
    goUserListPage() {
        this.router.navigate(['/', AppConstants.userListComponentPath]);
    }
}

export class PasswordValidation {

    static MatchPassword(ac: AbstractControl) {
        if (ac.get(ControlNames.password).value != ac.get(ControlNames.confirmpassword).value) {
            ac.get(ControlNames.confirmpassword).setErrors({ MatchPassword: true })
        } else {
            return null
        }
    }
}

export class ControlNames {
    static username = 'username';
    static password = 'password';
    static confirmpassword = 'confirmpassword';
    static firstname = 'firstname';
    static lastname = 'lastname';
    static email = 'email';
    static ddrole = 'ddrole';
    static ddusertype = 'ddusertype';
}

