import { Component, OnInit, ViewChild } from '@angular/core';
import { MatPaginator, MatSort, MatTableDataSource } from '@angular/material';
import { Router } from '@angular/router';

import { ContentList } from '../../_models/contentModelExtensions';
import { ContentService, AlertService } from '../../_services';
import { AppConstants } from '../../app.constant';

@Component({
    selector: 'app-content-list',
    templateUrl: './content-list.component.html',
    styleUrls: ['./content-list.component.css']
})
export class ContentListComponent implements OnInit {

    displayedColumns: string[] = ['contentId', 'contentName', 'contentUrl', 'contentType', 'actions'];
    gridDataSource: MatTableDataSource<ContentList>;
    contentIdToDelete = 0;
    showModal = false;
    lblmodalContent = "";

    isLoadingResults = true;

    @ViewChild(MatPaginator) paginator: MatPaginator;
    @ViewChild(MatSort) sort: MatSort;

    constructor(private contentService: ContentService,
        private alertService: AlertService,
        private router: Router) { }

    ngOnInit() {
        this.bindContentList();
    }

    //Bind Grid
    bindContentList() {
        this.contentService.getList().subscribe(contentlist => {
            this.gridDataSource = new MatTableDataSource(contentlist);
            this.isLoadingResults = false;

            this.gridDataSource.sort = this.sort;
            this.gridDataSource.paginator = this.paginator;
        });
    }   

    // Search Content
    applyFilter(filterValue: string) {
        this.gridDataSource.filter = filterValue.trim().toLowerCase();
    }

    // Go To Add Content
    goAddContentDetails() {
        this.router.navigate(['/' + AppConstants.contentDetail, 0]);
    }

    //Delete Content Modal
    onDeleteModal(content: ContentList): void {
        this.showModal = true;
        this.lblmodalContent = "Are you sure you want to delete <b>" + content.contentName + "</b> with <b> ContentId - " + content.contentId + "</b> ?";
        this.contentIdToDelete = content.contentId;
    }

    //Delete content
    deleteContent() {
        this.contentService.delete(this.contentIdToDelete).subscribe(() => {
            this.closeModal();
            this.alertService.success('Content Id - ' + this.contentIdToDelete + ' Deleted Successfully', true);
            this.bindContentList();
        }); 
    }

    closeModal() {
        this.showModal = false;
    }
}
