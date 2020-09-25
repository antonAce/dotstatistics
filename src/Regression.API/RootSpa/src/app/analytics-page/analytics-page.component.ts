import { Component, OnInit, OnDestroy } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { HttpErrorResponse } from '@angular/common/http';

import { Subscription, forkJoin } from 'rxjs';
import { switchMap, map } from 'rxjs/operators';

import { KatexOptions } from 'ng-katex';

import { LinearEquation, EquationEstimations, OutputPairs } from '@models/analytics';
import { DatasetAnalysisService } from '@services/dataset-analysis.service';

@Component({
  selector: 'analytics-page',
  templateUrl: './analytics-page.component.html',
  styleUrls: ['./analytics-page.component.scss']
})
export class AnalyticsPageComponent implements OnInit, OnDestroy {
  equation: string;
  estimations: EquationEstimations;
  pairs: OutputPairs[];

  options: KatexOptions = {
    displayMode: true,
  };

  errorMessage: string = null;

  private routeChange$ = new Subscription();

  constructor(private datasetAnalysisService: DatasetAnalysisService,
              private activateRoute: ActivatedRoute) { }

  ngOnInit() {
    this.routeChange$ = this.activateRoute.params.pipe(
      switchMap(params => forkJoin(
        this.datasetAnalysisService.calculateEquation(params['id']),
        this.datasetAnalysisService.calculateEstimations(params['id'])
      )),
      map(results => {
        return {
          equation: this.polynomialToKatex(results[0]),
          estimations: results[1],
          pairs: this.getPairsFromEstimations(results[1])
        };
      })
      ).subscribe(
        (data) => {
          this.equation = data.equation;
          this.estimations = data.estimations;
          this.pairs = data.pairs;
        },
        (error: HttpErrorResponse) => this.handleError(error)
      );
  }

  ngOnDestroy() {
    this.routeChange$.unsubscribe();
  }

  private handleError(error: HttpErrorResponse) {
    if (error.status == 400) {
      this.errorMessage = `Cannot calculate estimations: ${error.error}`
    } else {
      this.errorMessage = "Oops! Something went wrong on server side!"
    }
  }

  private getPairsFromEstimations(estimations: EquationEstimations): OutputPairs[] {
    return estimations.discreteOutput.map((y, i) => {
      return {
        discrete: y,
        approximate: estimations.approximationOutputs[i]
      } as OutputPairs
    })
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
        transformedPolynomial += `${Math.abs(polymonial.koeficients[index])}x_{${index - 1}}`;
    }

    return transformedPolynomial;
  }
}
