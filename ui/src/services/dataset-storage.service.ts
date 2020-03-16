import { Injectable } from '@angular/core';
import { HttpClient, HttpParams } from '@angular/common/http';

import { Observable } from 'rxjs';

import { DatasetToRead, DatasetToSave, DatasetHeader } from '@models/dataset';

import { API_SETTINGS } from '@environment/api';

@Injectable({
  providedIn: 'root'
})
export class DatasetStorageService {
  private readonly apiStorageEndpoint: string = API_SETTINGS.BASE_URL + "/api/dataset";

  private readonly requestTextOptions: Object = {
    responseType: 'text'
  }

  constructor(private http: HttpClient) {}

  fetchDatasets(limit?: number, offset?: number): Observable<DatasetToRead[]> {
    let params = new HttpParams();
    this.buildQueryParamsDataFetch(params, limit, offset);

    return this.http.get<DatasetToRead[]>(this.apiStorageEndpoint, { params });
  }

  fetchDatasetHeaders(limit?: number, offset?: number): Observable<DatasetHeader[]> {
    let params = new HttpParams();
    params.set('headersOnly', 'true');
    this.buildQueryParamsDataFetch(params, limit, offset);

    return this.http.get<DatasetHeader[]>(this.apiStorageEndpoint, { params });
  }

  getDatasetById(id: string): Observable<DatasetToRead> {
    return this.http.get<DatasetToRead>(this.apiStorageEndpoint + "/" + id);
  }

  getDatasetOutputs(id: string): Observable<DatasetToRead> {
    return this.http.get<DatasetToRead>(this.apiStorageEndpoint + "/" + id + "?outputsOnly=true");
  }

  storeDataset(dataset: DatasetToSave): Observable<string> {
    return this.http.post<string>(this.apiStorageEndpoint, dataset, this.requestTextOptions);
  }

  updateDataset(id: string, dataset: DatasetToSave): Observable<string> {
    return this.http.put<string>(this.apiStorageEndpoint + "/" + id, dataset, this.requestTextOptions);
  }

  dropDataset(id: string): Observable<string> {
    return this.http.delete<string>(this.apiStorageEndpoint + "/" + id, this.requestTextOptions);
  }

  private buildQueryParamsDataFetch(params: HttpParams, limit?: number, offset?: number) {
    if (limit !== null) {
      params.set('limit', String(limit));
    }

    if (offset !== null) {
      params.set('offset', String(offset));
    }
  }
}
