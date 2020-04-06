import { Component, OnInit, OnDestroy } from '@angular/core';
import { ActivatedRoute } from '@angular/router';

import { Subscription } from 'rxjs';
import { switchMap } from 'rxjs/operators';

import { EquationEstimations, OutputPairs, ComparingPairs } from '@models/analytics';
import { DatasetAnalysisService } from '@services/dataset-analysis.service';

@Component({
  selector: 'plot-page',
  templateUrl: './plot-page.component.html',
  styleUrls: ['./plot-page.component.scss']
})
export class PlotPageComponent implements OnInit, OnDestroy {
  pairs: OutputPairs[];

  comparingPairs: ComparingPairs[] = [{
    argument: 0,
    discrete: 1,
    approximate: 2
  },
  {
    argument: 1,
    discrete: 2,
    approximate: 3
  },
  {
    argument: 2,
    discrete: 3,
    approximate: 4
  }];

  private routeChange$ = new Subscription();

  constructor(private datasetAnalysisService: DatasetAnalysisService,
              private activateRoute: ActivatedRoute) { }

  ngOnInit() {
    this.routeChange$.add(this.activateRoute.params.pipe(
      switchMap(params => this.datasetAnalysisService.calculateEstimations(params['id']))
    ).subscribe(
      (estimations: EquationEstimations) => {
        this.pairs = estimations.discreteOutput.map((y, i) => {
          return {
            discrete: y,
            approximate: estimations.approximationOutputs[i]
          } as OutputPairs
        });
    }));
  }

  ngOnDestroy() {
    this.routeChange$.unsubscribe();
  }
}
