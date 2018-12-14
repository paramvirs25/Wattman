import { Component, OnInit, OnDestroy } from '@angular/core';
import { Subscription } from 'rxjs';
import { AlertService } from '../_services';
import { MatSnackBar } from '@angular/material';

@Component({
    selector: 'alert',
    templateUrl: 'alert.component.html'
})

export class AlertComponent implements OnInit, OnDestroy {
    private subscription: Subscription;
    message: any;

    constructor(
        private alertService: AlertService,
        public snackBar: MatSnackBar) { }

    ngOnInit() {

        //this.subscription = this.alertService.getMessage().subscribe(message => {
        //  this.message = message;
        //  setTimeout(() => this.message = null, 3000);
        //});

        //this.subscription = this.alertService.getMessage().subscribe(message => {
        //    if (message) {
        //        this.snackBar.open(
        //            message.text,
        //            message.type,
        //            {
        //                duration: 5000,
        //                panelClass: ['snackBar-customClass']
        //            });
        //    }
        //});
    }

    ngOnDestroy() {
        //this.subscription.unsubscribe();
    }
}
