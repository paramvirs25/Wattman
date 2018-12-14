import { Injectable } from '@angular/core';
import { HttpRequest, HttpHandler, HttpEvent, HttpInterceptor } from '@angular/common/http';
import { Observable, throwError } from 'rxjs';
import { catchError} from 'rxjs/operators';

import { AuthenticationService, AlertService } from '../_services';

@Injectable()
export class ErrorInterceptor implements HttpInterceptor {
    constructor(
        private authenticationService: AuthenticationService,
        private alertService: AlertService
    ) { }

    /*
     * Following error objects can be expected
     * HttpErrorResponse{name: "HttpErrorResponse", ok: false, status: 0, statusText: "Unknown Error", message}
     * HttpErrorResponse{name: "HttpErrorResponse", ok: false, status: 404, statusText: "Not Found", message: "Http failure response for {URL} 404 Not Found"}
    **/
    intercept(request: HttpRequest<any>, next: HttpHandler): Observable<HttpEvent<any>> {

        return next.handle(request).pipe(catchError(err => {
            //switch (err.status) { //"Unknown Error"
            //    case 0:
            //    case 404:
            //    this.alertService.error(`${err.name} - ${err.message}`);
            //}

            console.log(err);
            //if (err.status === 401) {
            //    // auto logout if 401 response returned from api
            //    this.authenticationService.logout();
            //    location.reload(true);
            //}

            if (err.status == "400" //"Bad Request" - Some validation failed on server
                || err.status == "404" //"Not Found" - Record not found on server
            ) 
            {
                let msg: string[] = this.formatErrorMsg(err.error);
                if (msg == null) {
                    msg.push(err.statusText);
                }

                this.alertService.errorArr(msg);
            }
            //else if (err.error && err.error.message != undefined) {
            //    error = err.error.message;
            //    this.alertService.error(`${error}`);
            //}
            //else {
            //    error = err.message || err.statusText;
            //}

            return throwError(err);
        }));
    }

    /*
     * Expected object format is:
     * {Key1: ArrayValue(1), Key2: ArrayValue(1)}
     */
    formatErrorMsg(error: object) : string[] {
        let errMsgs: string[] = new Array();

        if (error != null) {
            for (let key in error) {
                let value = error[key];
                if (value instanceof Array) { //if key's value is array
                    errMsgs = errMsgs.concat(value);
                }
                else { //if key's value is a string
                    errMsgs.push(value);
                }
            }

            return errMsgs;  //.join("<br/>");
        }

        return null;
    }
}
