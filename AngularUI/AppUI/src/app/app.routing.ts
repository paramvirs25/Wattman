import { Routes, RouterModule } from '@angular/router';
import { AppConstants } from './app.constant';

import { HomeComponent } from './home';
import { LoginComponent } from './login';
import { RegisterComponent } from './register';
import { AuthGuard, AdminGuard } from './_guards';
import { DashboardComponent } from './dashboard';

import { ClientLayoutComponent } from './layouts/client-layout/client-layout.component';
import { AdminLayoutComponent } from './layouts/admin-layout/admin-layout.component';

import { ClientLoginLayoutComponent } from './layouts/client-login-layout/client-login-layout.component';
import { AdminLoginLayoutComponent } from './layouts/admin-login-layout/admin-login-layout.component';

import { UserListComponent } from './admin/user-list/user-list.component';
import { UserDetailComponent } from './admin/user-detail/user-detail.component';
import { UserDetailMatComponent } from './admin/user-detail-mat/user-detail-mat.component';
import { ContentListComponent } from './admin/content-list/content-list.component';
import { ContentDetailComponent } from './admin/content-detail/content-detail.component';

const appRoutes: Routes = [
    { path: '', redirectTo: ('/' + AppConstants.clientLoginComponentPath), pathMatch: 'full' },
    {
        path: '',
        component: AdminLayoutComponent,
        canActivate: [AuthGuard, AdminGuard],
        children: [
            { path: AppConstants.userListComponentPath, component: UserListComponent },
            { path: AppConstants.userDetailComponentPath, component: UserDetailComponent },
            { path: AppConstants.userDetailMatComponentPath, component: UserDetailMatComponent },
            { path: AppConstants.contentListComponentPath, component: ContentListComponent },
            { path: AppConstants.contentDetailComponentPath, component: ContentDetailComponent }
        ]
    },
    {
        path: '',
        component: ClientLayoutComponent,
        canActivate: [AuthGuard],
        children: [
            { path: AppConstants.homeComponentPath, component: HomeComponent },
            { path: AppConstants.dashboardComponentPath, component: DashboardComponent }
        ]
    },
    {
        path: '',
        component: ClientLoginLayoutComponent,
        children: [
            { path: AppConstants.clientLoginComponentPath, component: LoginComponent }
            //,{ path: 'register', component: RegisterComponent }
        ]
    },
    {
        path: '',
        component: AdminLoginLayoutComponent,
        children: [
            { path: AppConstants.adminLoginComponentPath, component: LoginComponent }
        ]
    },
    
    // otherwise redirect to home
    { path: '**', redirectTo: '' }
    //{ path: '', pathMatch: 'full', redirectTo: 'login' }
];

export const routing = RouterModule.forRoot(appRoutes, {enableTracing: false});
