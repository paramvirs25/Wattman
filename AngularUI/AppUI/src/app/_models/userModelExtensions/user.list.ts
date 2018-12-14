import { UserDetail } from "../user.details";

export class UserList extends UserDetail {
    createdByName: string;
    modifiedByName: string;
    roleName: string;
    userTypeName: string;
    isAllContentWatched: boolean;
}
