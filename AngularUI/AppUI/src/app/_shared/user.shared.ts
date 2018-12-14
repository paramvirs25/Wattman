import { UserAuthenticated } from '../_models';

export class UserShared {
    private static readonly CURRENT_USER_KEY = 'currentUser';
    private static readonly ADMIN_LOGIN_KEY = 'al';

    private static readonly ADMIN_LOGIN_VAL = '*&we45jevTYQWeXkY';
    private static readonly NON_ADMIN_LOGIN_VAL = '89EdfThs#%@17eDt';

    isUserLoggedIn(): Boolean {
        return this.getLoggedInUser() != null;
    }

    getLoggedInUser(): UserAuthenticated {
        let currentUser = sessionStorage.getItem(UserShared.CURRENT_USER_KEY);
        //localStorage.getItem(UserShared.CURRENT_USER);
        if (currentUser) {
            return JSON.parse(currentUser);
        }

        return null;
    }

    setLoggedInUser(user: UserAuthenticated) {
        // store user details and jwt token in local storage to keep user logged in between page refreshes
        sessionStorage.setItem(UserShared.CURRENT_USER_KEY, JSON.stringify(user));
    }

    removeLoggedInUser() {
        // remove user from local storage to log user out
        sessionStorage.removeItem(UserShared.CURRENT_USER_KEY);
        sessionStorage.removeItem(UserShared.ADMIN_LOGIN_KEY);
    }

    isAdminLogIn(): Boolean {
        var adminLogin = sessionStorage.getItem(UserShared.ADMIN_LOGIN_KEY);
        return (adminLogin && adminLogin === UserShared.ADMIN_LOGIN_VAL);
    }

    setAdminLoginFlag(isAdmin: Boolean) {
        sessionStorage.setItem(UserShared.ADMIN_LOGIN_KEY, isAdmin ? UserShared.ADMIN_LOGIN_VAL : UserShared.NON_ADMIN_LOGIN_VAL);
    }
}
