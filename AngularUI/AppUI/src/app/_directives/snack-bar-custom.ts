import { Component, Inject } from '@angular/core';
import { Subscription } from 'rxjs';
import { MatSnackBar } from '@angular/material';
import { AlertService } from '../_services';
import { MAT_SNACK_BAR_DATA } from '@angular/material';

/**
 * @title Snack-bar with a custom component
 */
@Component({
    selector: 'snack-bar-custom',
    template: '',
})
export class SnackBarCustomComponent {
    private subscription: Subscription;
    message: any;

    constructor(
        private alertService: AlertService,
        public snackBar: MatSnackBar) {}

    ngOnInit() {
        this.subscription = this.alertService.getMessage().subscribe(message => {
            if (message) {
                this.snackBar.openFromComponent(SnackBarComponent, {
                    data: message,
                    duration: 5000,
                    panelClass: ['snackBar-customClass']
                });

                //this.snackBar.open(
                //    message.text,
                //    message.type,
                //    {
                //        duration: 5000,
                //        panelClass: ['snackBar-customClass']
                //    });
            }
        });
    }

    ngOnDestroy() {
        this.subscription.unsubscribe();
    }
}


@Component({
    selector: 'snack-bar-component-example-snack',
    templateUrl: 'snack-bar-custom.html'
})
export class SnackBarComponent {
    constructor(@Inject(MAT_SNACK_BAR_DATA) public data: any) { }
}
