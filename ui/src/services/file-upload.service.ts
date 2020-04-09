import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';

import { Observable } from 'rxjs';

import { API_SETTINGS } from '@environment/api';

@Injectable({
  providedIn: 'root'
})
export class FileUploadService {
  private readonly apiFileUploadEndpoint: string = API_SETTINGS.BASE_URL + "/api/fileUpload";

  private readonly fileUploadHeader = {
      headers: new HttpHeaders({
          'enctype':  'multipart/form-data',
      })
  };

  constructor(private http: HttpClient) {}

  uploadFile(file: any): Observable<string> {
    const formToUpload = new FormData();
    formToUpload.append('uploadFile', file);

    return this.http.post<string>(this.apiFileUploadEndpoint, formToUpload, this.fileUploadHeader);
  }
}
