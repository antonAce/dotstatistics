import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';

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
export class AnalyticsPageComponent implements OnInit {
  equation: string;
  options: KatexOptions = {
    displayMode: true,
  };

  constructor(private modelAnalysisService: ModelAnalysisService,
              private datasetStorage: DatasetStorageService,
              private activateRoute: ActivatedRoute) { }

  ngOnInit() {
    this.datasetStorage.getDatasetById(this.activateRoute.snapshot.params['id']).subscribe((dataset: DatasetToRead) => {
      this.modelAnalysisService.calculateLinearEquation(<DatasetToProcess>{records:dataset.records}).subscribe(
        (poly: LinearEquation) => this.equation = this.polynomialToKatex(poly),
        (error) => {});
    });
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
