export namespace AppConstants {
    export const userDetail = "userdetail";
    export const userDetailMat = "userdetailmat";
    export const contentDetail = "contentdetail";

    export const adminLoginComponentPath = "admin";
    export const clientLoginComponentPath = "login";
    export const homeComponentPath = "home";
    export const dashboardComponentPath = "dashboard";
    export const userListComponentPath = "userlist";    
    export const userDetailComponentPath = userDetail + "/:id";    
    export const userDetailMatComponentPath = userDetailMat + "/:id/:isloggedinUser";
    export const contentListComponentPath = "contentlist";
    export const contentDetailComponentPath = contentDetail + "/:id";   
}
