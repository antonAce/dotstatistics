import { Component, OnInit, OnDestroy } from '@angular/core';
import { ActivatedRoute } from '@angular/router';

import { Subscription } from 'rxjs';
import { KatexOptions } from 'ng-katex';

import { DatasetToRead, DatasetToProcess } from '@models/dataset';
import { LinearEquation } from '@models/analytics';

import { DatasetStorageService } from '@services/dataset-storage.service';
import { ModelAnalysisService } from '@services/model-analysis.service';

@Component({
  selector: 'analytics-page',
  templateUrl: './analytics-page.component.html',
  styleUrls: ['./analytics-page.component.scss']
})
export class AnalyticsPageComponent implements OnInit, OnDestroy {
  equation: string;
  options: KatexOptions = {
    displayMode: true,
  };

  private datasetStorage$ = new Subscription();
  private routeChange$ = new Subscription();

  constructor(private modelAnalysisService: ModelAnalysisService,
              private datasetStorage: DatasetStorageService,
              private activateRoute: ActivatedRoute) { }

  ngOnInit() {
    this.routeChange$ = this.activateRoute.params.subscribe((params) => {
      this.datasetStorage$.add(this.datasetStorage.getDatasetById(params['id']).subscribe((dataset: DatasetToRead) => {
        this.modelAnalysisService.calculateLinearEquation(<DatasetToProcess>{records:dataset.records}).subscribe(
          (poly: LinearEquation) => this.equation = this.polynomialToKatex(poly));
      }));
    });
  }

  ngOnDestroy() {
    this.datasetStorage$.unsubscribe();
    this.routeChange$.unsubscribe();
  }

  private polynomialToKatex(polymonial: LinearEquation): string {
    let transformedPolynomial = "y =";

    for (let index = 0; index < polymonial.koeficients.length; index++) {
      if (index == 0)
        transformedPolynomial += (polymonial.koeficients[index] < 0) ? "-" : " ";
      else
        transformedPolynomial += (polymonial.koeficients[index] < 0) ? "-" : "+";

      if (index == 0)
        transformedPolynomial += `${Math.abs(polymonial.koeficients[index])}`;
      else
        transformedPolynomial += `${Math.abs(polymonial.koeficients[index])}x_{${index}}`;
    }

    return transformedPolynomial;
  }
}
