import { Roles } from "../roles";
import { UserTypes } from "../userTypes";
import { UserLogin } from "../user.login";
import { UserDetailsBaseAdmin } from "../user.details.base.admin";

export class UserEdit
{
    user: UserLogin;
    userDetailsBaseAdmin: UserDetailsBaseAdmin;
    roles: Roles[];
    userTypes: UserTypes[];
}

