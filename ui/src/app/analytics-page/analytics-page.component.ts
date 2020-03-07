import { Component, OnInit } from '@angular/core';
import { KatexOptions } from 'ng-katex';

import { Polymonial } from '@models/analytics/polynomial';
import { ModelSerializationService } from '@services/model-serialization.service';

@Component({
  selector: 'app-analytics-page',
  templateUrl: './analytics-page.component.html',
  styleUrls: ['./analytics-page.component.scss']
})
export class AnalyticsPageComponent implements OnInit {
  equation: string = 'y = 0.2 + 0.4 x + 0.6 x^2';
  options: KatexOptions = {
    displayMode: true,
  };

  constructor(private modelSerializer: ModelSerializationService) { }

  ngOnInit() {
    let poly = new Polymonial();

    poly.constants = [
      {
        power: -1,
        value: 2
      },
      {
        power: 0,
        value: 5
      },
      {
        power: 2,
        value: 12
      }
    ];

    this.equation = this.polynomialToKatex(poly);
  }

  private polynomialToKatex(polymonial: Polymonial): string {
    let transformedPolynomial = "y =";

    for (let index = 0; index < polymonial.constants.length; index++) {
      if (index == 0)
        transformedPolynomial += (polymonial.constants[index].value < 0) ? "-" : " ";
      else
        transformedPolynomial += (polymonial.constants[index].value < 0) ? "-" : "+";

      if (polymonial.constants[index].power == 0)
        transformedPolynomial += `${polymonial.constants[index].value}x`;
      else
        transformedPolynomial += `${polymonial.constants[index].value}x^{${polymonial.constants[index].power}}`;
    }

    return transformedPolynomial;
  }
}
