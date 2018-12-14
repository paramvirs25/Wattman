import '../polyfills';

import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { HttpClientModule, HTTP_INTERCEPTORS } from '@angular/common/http';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { FontAwesomeModule } from '@fortawesome/angular-fontawesome';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';

import { AngularMaterialModule } from './material-module';

import { AppComponent } from './app.component';
import { routing } from './app.routing';

import { BsDropdownModule } from 'ngx-bootstrap/dropdown';
import { TooltipModule } from 'ngx-bootstrap/tooltip';
import { ModalModule } from 'ngx-bootstrap/modal';
import { CompareValidatorModule } from 'angular-compare-validator';

import { AlertComponent } from './_directives';
import { SnackBarCustomComponent, SnackBarComponent } from './_directives';

import { AuthGuard, AdminGuard } from './_guards';
import { JwtInterceptor, ErrorInterceptor, ValidationCheck } from './_helpers';
import { AlertService, AuthenticationService, UserService, ContentService, RolesService, UserTypesService, UserContentService } from './_services';
import { UserShared } from './_shared';

import { HomeComponent } from './home';
import { LoginComponent } from './login';
import { RegisterComponent } from './register';
import { DashboardComponent } from './dashboard';
import { AdminLayoutComponent } from './layouts/admin-layout/admin-layout.component';
import { ClientLayoutComponent } from './layouts/client-layout/client-layout.component';
import { UserListComponent } from './admin/user-list/user-list.component';
import { UserDetailComponent } from './admin/user-detail/user-detail.component';
import { UserDetailMatComponent } from './admin/user-detail-mat/user-detail-mat.component';
import { AdminLoginLayoutComponent } from './layouts/admin-login-layout/admin-login-layout.component';
import { ClientLoginLayoutComponent } from './layouts/client-login-layout/client-login-layout.component';
import { ContentListComponent } from './admin/content-list/content-list.component';
import { ContentDetailComponent } from './admin/content-detail/content-detail.component';
import { EmbedVideo } from 'ngx-embed-video';
import { YoutubePlayerModule } from 'ngx-youtube-player';

@NgModule({
    imports: [
        BrowserModule,
        BrowserAnimationsModule,
        FormsModule,
        HttpClientModule,
        AngularMaterialModule,
        ReactiveFormsModule,
        routing,
        FontAwesomeModule,
        CompareValidatorModule,
        YoutubePlayerModule,
        BsDropdownModule.forRoot(),
        TooltipModule.forRoot(),
        ModalModule.forRoot(),
        EmbedVideo.forRoot()
    ],
    declarations: [
        AppComponent,
        AlertComponent,        
        AdminLoginLayoutComponent,
        ClientLoginLayoutComponent,
        AdminLayoutComponent,
        ClientLayoutComponent,
        HomeComponent,
        LoginComponent,
        RegisterComponent,
        DashboardComponent,
        UserListComponent,
        UserDetailComponent,
        SnackBarCustomComponent, SnackBarComponent,
        UserDetailMatComponent,
        ContentListComponent,
        ContentDetailComponent
    ],
    providers: [
        AuthGuard,
        AdminGuard,
        AlertService,
        ContentService,
        AuthenticationService,
        UserService,
        UserContentService,
        RolesService,
        UserTypesService,
        UserShared,
        ValidationCheck,
        { provide: HTTP_INTERCEPTORS, useClass: JwtInterceptor, multi: true },
        { provide: HTTP_INTERCEPTORS, useClass: ErrorInterceptor, multi: true }
    ],
    entryComponents: [UserListComponent, AlertComponent, SnackBarCustomComponent, SnackBarComponent],
    exports: [BsDropdownModule, TooltipModule, ModalModule],
    bootstrap: [AppComponent]
})

export class AppModule { }

