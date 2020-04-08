import { Component, OnInit, OnDestroy } from '@angular/core';
import { ActivatedRoute } from '@angular/router';

import { Subscription, forkJoin } from 'rxjs';
import { switchMap, map } from 'rxjs/operators';

import { ComparingPairs } from '@models/analytics';

import { DatasetStorageService } from '@services/dataset-storage.service';
import { DatasetAnalysisService } from '@services/dataset-analysis.service';

@Component({
  selector: 'plot-page',
  templateUrl: './plot-page.component.html',
  styleUrls: ['./plot-page.component.scss']
})
export class PlotPageComponent implements OnInit, OnDestroy {
  comparingPairs: ComparingPairs[];

  private routeChange$ = new Subscription();

  constructor(private datasetStorageService: DatasetStorageService,
              private datasetAnalysisService: DatasetAnalysisService,
              private activateRoute: ActivatedRoute) { }

  avg = (numbers: number[]) => (numbers.reduce((a,b) => a + b, 0)) / numbers.length;

  ngOnInit() {
    this.routeChange$ = this.activateRoute.params.pipe(
      switchMap(params => forkJoin(
        this.datasetStorageService.getDatasetById(params['id']),
        this.datasetAnalysisService.calculateEstimations(params['id'])
      )),
      map(results => results[0].records.map((record, i) => {
        return {
          argument: record.inputs[0],
          discrete: results[1].discreteOutput[i],
          approximate: results[1].approximationOutputs[i]
        } as ComparingPairs;
      }))
    ).subscribe((pairs) => this.comparingPairs = pairs.sort((a, b) => a.argument < b.argument ? -1 : a.argument > b.argument ? 1 : 0));
  }

  ngOnDestroy() {
    this.routeChange$.unsubscribe();
  }
}
