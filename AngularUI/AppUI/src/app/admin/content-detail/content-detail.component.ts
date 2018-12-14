import { Component, OnInit } from '@angular/core';
import { AlertService, ContentService } from '../../_services';
import { ActivatedRoute } from '@angular/router';
import { Router } from '@angular/router';
import { FormControl, FormBuilder, FormGroup, Validators } from '@angular/forms';
import { AppConstants } from '../../app.constant';

import { ValidationCheck } from '../../_helpers';
import { ContentType, ContentBase } from '../../_models';

@Component({
    selector: 'app-content-detail',
    templateUrl: './content-detail.component.html',
    styleUrls: ['./content-detail.component.css']
})
export class ContentDetailComponent implements OnInit {

    constructor(
        private contentService: ContentService,
        private router: Router,
        private alertService: AlertService,
        private activeRoute: ActivatedRoute,
        private validationCheck: ValidationCheck,
        private formBuilder: FormBuilder) { }

    contentDetailsForm: FormGroup;
    contentTypeOptions: ContentType[];

    isLoadingResults = false;
    isSaving = false;

    contentId = 0; //Add Mode
    lblAddEditContent: string;
    isEditMode = false;

    contentname = new FormControl('', [Validators.required]);
    contenturl = new FormControl('', [Validators.required]);
    ddcontenttype = new FormControl(null);

    matcher = this.validationCheck.errorStateMatcher();

    ngOnInit() {
        this.contentId = +this.activeRoute.snapshot.paramMap.get('id'); // get id as number
        this.isEditMode = this.contentId > 0;

        //Initialise validations
        this.initValidators();

        //Add Mode
        if (!this.isEditMode) {
            this.lblAddEditContent = "Add Content";
            this.isLoadingResults = true;

            //Get Data for Create Mode
            this.getForCreate();
        }
        //Edit Mode
        else {
            this.lblAddEditContent = "Edit Content -> ContentId - " + this.contentId;
            this.isEditMode = true;
            this.isLoadingResults = true;

            //Bind Controls for Edit Mode
            this.getForEdit(this.contentId);
        }
    }

    //Bind Controls for Create Mode
    getForCreate() {
        this.contentService.getForCreate().subscribe(cc => {

            //Initialise dropdown
            this.initContentTypes(cc.contentType);
            this.isLoadingResults = false;
        },
        error => {
            this.isLoadingResults = false;
        });
    }

    //Bind Controls for Edit Mode
    getForEdit(contentId) {

        this.contentService.getForEdit(contentId).subscribe(ce => {

            this.contentCtrls.contentname.setValue(ce.content.contentName);
            this.contentCtrls.contenturl.setValue(ce.content.contentUrl);

            //Initialise dropdown
            this.initContentTypes(ce.contentType);

            //Bind dropdown
            this.contentCtrls.ddcontenttype.setValue(ce.content.contentType);

            this.isLoadingResults = false;
        },
        error => {
            this.isLoadingResults = false;
            this.router.navigate(['/', AppConstants.contentListComponentPath]);
        });
    }

    // convenience getter for easy access to form fields
    get contentCtrls() { return this.contentDetailsForm.controls; }

    //Initialise form validators
    initValidators() {
        this.contentDetailsForm = this.formBuilder.group({
            contentname: this.contentname,
            contenturl: this.contenturl,
            ddcontenttype: this.ddcontenttype
        });
    }

    // Initialise Dropdown ContentTypes
    initContentTypes(contentTypes: ContentType[]) {
        this.contentTypeOptions = contentTypes;
        this.contentCtrls.ddcontenttype.setValue(this.contentTypeOptions[0].contentTypeName);
    }

    // Go To Content List
    goContentListPage() {
        this.router.navigate(['/', AppConstants.contentListComponentPath]);
    }

    //Save Content Details
    save() {

        // make controls touched for validation to work
        this.validationCheck.makeCtrlsTouched(this.contentCtrls);

        // stop here if form is invalid
        if (this.contentDetailsForm.invalid) {
            return;
        }

        let contentBase: ContentBase = new ContentBase();
        contentBase.contentId = this.contentId;
        contentBase.contentName = this.contentCtrls.contentname.value;
        contentBase.contentType = this.contentCtrls.ddcontenttype.value;
        contentBase.contentUrl = this.contentCtrls.contenturl.value;

        this.isSaving = true;

        if (!this.isEditMode) {
            this.contentService.create(contentBase).subscribe(
                data => {
                    this.isSaving = false;
                    this.alertService.success('Content Created Successfully', true);
                    this.goContentListPage();
                },
                error => {
                    this.isSaving = false;
                });
        }
        else {
            this.contentService.update(contentBase).subscribe(
                data => {
                    this.isSaving = false;
                    this.alertService.success('Content Updated Successfully', true);
                },
                error => {
                    this.isSaving = false;
                });
        }
        
    }
}
