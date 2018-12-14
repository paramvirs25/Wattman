import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';

import { environment } from '../../environments/environment';
import { Observable } from 'rxjs';
import { ContentList, ContentCreateGet, ContentEditGet } from '../_models/contentModelExtensions';
import { ContentModel, ContentBase } from '../_models';

@Injectable()
export class ContentService {
    apiUrl: string;

    constructor(private http: HttpClient) {
        this.apiUrl = `${environment.apiUrl}/content`;
    }

    //Gets a list of content
    getList(): Observable<ContentList[]> {
        return this.http.get<ContentList[]>(`${this.apiUrl}/list`);
    }

    //Gets content by Id
    getById(id: number): Observable<ContentModel> {
        return this.http.get<ContentModel>(`${this.apiUrl}/` + id);
    }

    //Gets data for 'Create' user screen
    getForCreate(): Observable<ContentCreateGet> {
        return this.http.get<ContentCreateGet>(`${this.apiUrl}/getForCreate/`);
    }

    //Gets user's detail for editing
    getForEdit(id: number): Observable<ContentEditGet> {
        return this.http.get<ContentEditGet>(`${this.apiUrl}/getForEdit/` + id);
    }

    //Creates a content if it already doesnot exits
    create(contentBase: ContentBase) {
        return this.http.post(`${this.apiUrl}/create`, contentBase);
    }

    //Updates content if already exists
    update(contentBase: ContentBase) {
        return this.http.post(`${this.apiUrl}/update`, contentBase);
    }

    //Delete Content by Id
    delete(id: number) {
        return this.http.delete(`${this.apiUrl}/` + id);
    }
}
