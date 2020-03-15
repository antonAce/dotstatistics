import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';

import { Observable } from 'rxjs';

import { DatasetToProcess } from '@models/dataset';
import { LinearEquation } from '@models/analytics/linear-equation';

import { API_SETTINGS } from '@environment/api';

@Injectable({
  providedIn: 'root'
})
export class ModelAnalysisService {

  constructor(private http: HttpClient) {}

  calculateLinearEquation(dataset: DatasetToProcess): Observable<LinearEquation> {
    return this.http.post<LinearEquation>(API_SETTINGS.BASE_URL + '/api/processing?digits=2', dataset);
  }
}
