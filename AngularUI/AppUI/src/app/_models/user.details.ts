import { UserDetailsBaseAdmin } from './user.details.base.admin';

export class UserDetail extends UserDetailsBaseAdmin {
    isDeleted: boolean;
    createdDate: string;
    createdBy: number;
    modifiedDate: string;
    modifiedBy: number;
}
