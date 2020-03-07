import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';

import { Observable } from 'rxjs';
import { map } from 'rxjs/operators';

import { Polymonial } from '@models/analytics/polynomial';
import { RegressionModel } from '@models/statistics/regression-model';
import { RegressionPolynomial } from '@models/statistics/regression-polynomial';

import { API_SETTINGS } from '@environment/api';

@Injectable({
  providedIn: 'root'
})
export class ModelAnalysisService {

  constructor(private http: HttpClient) {}

  calculateRegressionPolynomial(model: RegressionModel): Observable<RegressionPolynomial> {
    return this.http.post<RegressionPolynomial>(API_SETTINGS.BASE_URL + '/api/processing', model);
  }

  calculatePurePolynomial(model: RegressionModel): Observable<Polymonial> {
    return this.calculateRegressionPolynomial(model).pipe(
      map(poly => {
        let polynomial = new Polymonial();
        polynomial.constants = [];

        for (let index = 0; index < poly.constants.length; index++) {
          polynomial.constants.push({
            power: index,
            value: poly.constants[index]
          });
        }

        return polynomial;
      })
    );
  }
}
