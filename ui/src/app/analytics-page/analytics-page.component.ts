import { Component, OnInit } from '@angular/core';
import { KatexOptions } from 'ng-katex';

import { Polymonial } from '@models/analytics/polynomial';

import { ModelSerializationService } from '@services/model-serialization.service';
import { ModelAnalysisService } from '@services/model-analysis.service';

@Component({
  selector: 'app-analytics-page',
  templateUrl: './analytics-page.component.html',
  styleUrls: ['./analytics-page.component.scss']
})
export class AnalyticsPageComponent implements OnInit {
  equation: string;
  options: KatexOptions = {
    displayMode: true,
  };

  constructor(private modelSerializer: ModelSerializationService,
              private modelAnalysisService: ModelAnalysisService) { }

  ngOnInit() {
    this.modelAnalysisService.calculatePurePolynomial(this.modelSerializer.getModel()).subscribe(
      (poly) => this.equation = this.polynomialToKatex(poly),
      (error) => {});
  }

  private polynomialToKatex(polymonial: Polymonial): string {
    let transformedPolynomial = "y =";

    for (let index = 0; index < polymonial.constants.length; index++) {
      if (index == 0)
        transformedPolynomial += (polymonial.constants[index].value < 0) ? "-" : " ";
      else
        transformedPolynomial += (polymonial.constants[index].value < 0) ? "-" : "+";

      if (polymonial.constants[index].power == 0)
        transformedPolynomial += `${Math.abs(polymonial.constants[index].value)}`;
      else
        transformedPolynomial += `${Math.abs(polymonial.constants[index].value)}x_{${polymonial.constants[index].power - 1}}`;
    }

    return transformedPolynomial;
  }
}
