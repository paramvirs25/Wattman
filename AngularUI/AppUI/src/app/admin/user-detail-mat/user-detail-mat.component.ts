import { Component, OnInit } from '@angular/core';
import { AlertService, UserService } from '../../_services';
import { ActivatedRoute } from '@angular/router';
import { Router } from '@angular/router';
import { FormControl, FormBuilder, FormGroup, Validators, AbstractControl } from '@angular/forms';

import { Roles, UserTypes, UserLogin, UserDetailsBaseAdmin, UserDetailBase } from '../../_models';
import { AppConstants } from '../../app.constant';
import { UserCreateSave } from '../../_models/userModelExtensions';
import { ValidationCheck } from '../../_helpers';

@Component({
    selector: 'app-user-detail-mat',
    templateUrl: './user-detail-mat.component.html',
    styleUrls: ['./user-detail-mat.component.css']
})
export class UserDetailMatComponent implements OnInit {

    constructor(
        private userService: UserService,
        private router: Router,
        private alertService: AlertService,
        private activeRoute: ActivatedRoute,
        private validationCheck: ValidationCheck,
        private formBuilder: FormBuilder) { }

    passhide = true;
    confirmhide = true;
    loginDetailsForm: FormGroup;
    userDetailsForm: FormGroup;

    roleOptions: Roles[];
    userTypeOptions: UserTypes[];
    
    isLoadingResults = false;
    isShowLoginCtrls = false;

    isSaving = false;
    isSavingLoginDetails = false;
    isSavingUserDetails = false;

    userId = 0; //Add Mode
    lblAddEditUser: string;
    isEditMode = false;
    isloggedInUser = false;

    // Define validations and form control names
    username = new FormControl('', [Validators.required]);
    password = new FormControl('', [Validators.required, Validators.minLength(4)]);
    confirmpass = new FormControl('', [Validators.required, Validators.minLength(4)]);
    firstname = new FormControl('', [Validators.required]);
    lastname = new FormControl('', [Validators.required]);
    email = new FormControl('', [Validators.required, Validators.email]);
    ddrole = new FormControl(null);
    ddusertype = new FormControl(null);

    matcher = this.validationCheck.errorStateMatcher();

    ngOnInit() {

        this.userId = +this.activeRoute.snapshot.paramMap.get('id'); // get id as number
        this.isloggedInUser = this.activeRoute.snapshot.paramMap.get('isloggedinUser') == "true";
        this.isEditMode = this.userId > 0;

        //Reloads page when Active Route params are changed
        this.reloadonRouteChange();
                        
        // Initialise Login Details Validators
        this.initLoginValidators();

        // Initialise User Details Validators
        this.initUserDetailValidators();
                
        //Add Mode
        if (!this.isEditMode) {
            this.lblAddEditUser = "Add User";
            this.isShowLoginCtrls = true;
            this.isLoadingResults = true;

            //Get Data for Create Mode
            this.getForCreate();
        }
        //Edit Mode
        else{
            this.lblAddEditUser = "Edit User -> UserId - " + this.userId;
            this.isEditMode = true;
            this.isLoadingResults = true;

            //Bind Controls for Edit Mode
            this.getForEdit(this.userId);
        }     
    }

    //Bind Controls for Create Mode
    getForCreate() {
        this.userService.getForCreate().subscribe(userCreate => {

            //Initialise dropdowns
            this.initRoles(userCreate.roles);
            this.initUserTypes(userCreate.userTypes);
            this.isLoadingResults = false;
        },
        error => {
            this.isLoadingResults = false;
        });
    }

    //Bind Controls for Edit Mode
    getForEdit(userId) {

        this.userService.getForEdit(userId).subscribe(ue => {

            this.loginCtrls.username.setValue(ue.user.username);
            this.loginCtrls.password.setValue(ue.user.password);
            this.loginCtrls.confirmpass.setValue(ue.user.password);
            this.userDetailCtrls.firstname.setValue(ue.userDetailsBaseAdmin.userFirstName);
            this.userDetailCtrls.lastname.setValue(ue.userDetailsBaseAdmin.userLastName);
            this.userDetailCtrls.email.setValue(ue.userDetailsBaseAdmin.userEmail);

            //Initialise dropdowns
            this.initRoles(ue.roles);
            this.initUserTypes(ue.userTypes);

            //Bind Dropdowns
            this.userDetailCtrls.ddrole.setValue(ue.userDetailsBaseAdmin.roleId);
            this.userDetailCtrls.ddusertype.setValue(ue.userDetailsBaseAdmin.userTypeId);

            this.isLoadingResults = false;
        },
        error => {
            this.isLoadingResults = false;
            this.router.navigate(['/', AppConstants.userListComponentPath]);
        });
    }

    //Save Method to Create User
    saveforCreate() {

        // make all controls touched for validation to work
        this.validationCheck.makeCtrlsTouched(this.loginCtrls);
        this.validationCheck.makeCtrlsTouched(this.userDetailCtrls);
   
        // stop here if form is invalid
        if (this.loginDetailsForm.invalid || this.userDetailsForm.invalid) { 
            return;
        }

        let ucSave: UserCreateSave = new UserCreateSave();

        //user login
        ucSave.user = new UserLogin();
        ucSave.user.userId = this.userId;
        ucSave.user.username = this.loginCtrls.username.value;
        ucSave.user.password = this.loginCtrls.password.value;

        //user details
        ucSave.userDetailsBaseAdmin = new UserDetailsBaseAdmin();
        ucSave.userDetailsBaseAdmin.userId = this.userId;
        ucSave.userDetailsBaseAdmin.userFirstName = this.userDetailCtrls.firstname.value;
        ucSave.userDetailsBaseAdmin.userLastName = this.userDetailCtrls.lastname.value;
        ucSave.userDetailsBaseAdmin.userEmail = this.userDetailCtrls.email.value;
        ucSave.userDetailsBaseAdmin.roleId = this.userDetailCtrls.ddrole.value;
        ucSave.userDetailsBaseAdmin.userTypeId = this.userDetailCtrls.ddusertype.value;
        
        this.isSaving = true;
        this.userService.create(ucSave).subscribe(
            data => {
                this.isSaving = false;
                this.alertService.success('User Created Successfully', true);
                this.goUserListPage();
            },
            error => {
                //this.alertService.error(error);
                this.isSaving = false;
            });
    }    

    //Save Method for Login Details Component
    saveLoginDetails() {

        // make Login controls touched for validation to work
        this.validationCheck.makeCtrlsTouched(this.loginCtrls);

        // stop here if form is invalid
        if (this.loginDetailsForm.invalid) {
            return;
        }

        let userLogin: UserLogin = new UserLogin();
        userLogin.userId = this.userId;
        userLogin.username = this.loginCtrls.username.value;
        userLogin.password = this.loginCtrls.password.value;

        this.isSavingLoginDetails = true;
        this.userService.update(userLogin).subscribe(
            data => {
                this.isSavingLoginDetails = false;
                this.alertService.success('Login Details Saved Successfully', true);
            },
            error => {
                //this.alertService.error(error);
                this.isSavingLoginDetails = false;
            });
      
    }

    //Save Method for User Details Component
    saveUserDetails() {

        // make user controls touched for validation to work
        this.validationCheck.makeCtrlsTouched(this.userDetailCtrls);

        // stop here if form is invalid
        if (this.userDetailsForm.invalid) {
            return;
        }

        //Save any user's general details
        if (!this.isloggedInUser) {
            let userDetailsBaseAdmin: UserDetailsBaseAdmin = new UserDetailsBaseAdmin();
            userDetailsBaseAdmin.userId = this.userId;
            userDetailsBaseAdmin.userFirstName = this.userDetailCtrls.firstname.value;
            userDetailsBaseAdmin.userLastName = this.userDetailCtrls.lastname.value;
            userDetailsBaseAdmin.userEmail = this.userDetailCtrls.email.value;
            userDetailsBaseAdmin.roleId = this.userDetailCtrls.ddrole.value;
            userDetailsBaseAdmin.userTypeId = this.userDetailCtrls.ddusertype.value;

            this.isSavingUserDetails = true;
            this.userService.updateDetail(userDetailsBaseAdmin).subscribe(
                data => {
                    this.isSavingUserDetails = false;
                    this.alertService.success('User Details Saved Successfully', true);
                },
                error => {
                    //this.alertService.error(error);
                    this.isSavingUserDetails = false;
                });
        }
        //Save logged in user's general details
        else {
            let userDetailBase: UserDetailBase = new UserDetailBase();
            userDetailBase.userId = this.userId;
            userDetailBase.userFirstName = this.userDetailCtrls.firstname.value;
            userDetailBase.userLastName = this.userDetailCtrls.lastname.value;
            userDetailBase.userEmail = this.userDetailCtrls.email.value;

            this.isSavingUserDetails = true;
            this.userService.updateDetailLoggedIn(userDetailBase).subscribe(
                data => {
                    this.isSavingUserDetails = false;
                    this.alertService.success('User Details Saved Successfully', true);
                },
                error => {
                    //this.alertService.error(error);
                    this.isSavingUserDetails = false;
                });
        }
        
    }

    // convenience getter for easy access to form fields
    get loginCtrls() { return this.loginDetailsForm.controls; }
    get userDetailCtrls() { return this.userDetailsForm.controls; }

    // Reloads page when Active Route params are changed
    reloadonRouteChange() {
        this.activeRoute.params.subscribe(() => {
            this.router.routeReuseStrategy.shouldReuseRoute = function () {
                return false;
            };
        });
    }

    //Initialise login details form validators
    initLoginValidators() {
        this.loginDetailsForm = this.formBuilder.group({
            username: this.username,
            password: this.password,
            confirmpass: this.confirmpass
        }, { validator: PasswordValidation.MatchPassword });
    }

    //Initialise user details form validators
    initUserDetailValidators() {
        this.userDetailsForm = this.formBuilder.group({
            firstname: this.firstname,
            lastname: this.lastname,
            email: this.email,
            ddrole: this.ddrole,
            ddusertype: this.ddusertype
        });
    }

    // Initialise Dropdown Roles
    initRoles(roles: Roles[]) {
        this.roleOptions = roles;
        this.userDetailCtrls.ddrole.setValue(this.roleOptions[0].roleId);
    }

    // Initialise Dropdown UserTypes
    initUserTypes(userTypes: UserTypes[]) {
        this.userTypeOptions = userTypes;
        this.userDetailCtrls.ddusertype.setValue(this.userTypeOptions[0].userTypeId);
    }

    // Go To Users List
    goUserListPage() {
        this.router.navigate(['/', AppConstants.userListComponentPath]);
    }
}

// Compare password validation
export class PasswordValidation {

    static MatchPassword(ac: AbstractControl) {
        if (ac.get(ControlNames.password).value != ac.get(ControlNames.confirmpass).value) {
            ac.get(ControlNames.confirmpass).setErrors({ MatchPassword: true })
        } else {
            return null
        }
    }
}

export class ControlNames {
    static username = 'username';
    static password = 'password';
    static confirmpass = 'confirmpass';
    static firstname = 'firstname';
    static lastname = 'lastname';
    static email = 'email';
    static ddrole = 'ddrole';
    static ddusertype = 'ddusertype';
}
