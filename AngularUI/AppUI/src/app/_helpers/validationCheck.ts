import { FormControl, NgForm, FormGroupDirective } from '@angular/forms';
import { ErrorStateMatcher } from '@angular/material/core';

export class ValidationCheck {

    //Match form error state
    errorStateMatcher() {
        return new MyErrorStateMatcher();
    }

    // make Controls touched for validations
    makeCtrlsTouched(ctrlColl) {
        for (let i in ctrlColl) {
            ctrlColl[i].markAsTouched();
        }
    }
}

/** Error when invalid control is dirty, touched, or submitted. */
export class MyErrorStateMatcher implements ErrorStateMatcher {
    isErrorState(control: FormControl | null, form: FormGroupDirective | NgForm | null): boolean {
        return !!(control && control.invalid && (control.dirty || control.touched));
    }
}
