import { Component, OnInit, OnDestroy } from '@angular/core';

import { Subscription } from 'rxjs';
import { switchMap } from 'rxjs/operators';

import { DatasetHeader } from '@models/dataset';

import { DatasetStorageService } from '@services/dataset-storage.service';
import { TooltipMediatorService } from '@services/tooltip-mediator.service';

@Component({
  selector: 'model-tooltip',
  templateUrl: './model-tooltip.component.html',
  styleUrls: ['./model-tooltip.component.scss']
})
export class ModelTooltipComponent implements OnInit, OnDestroy {
  datasetHeaders: DatasetHeader[];

  private mediator$ = new Subscription();

  constructor(private datasetStorage: DatasetStorageService,
              private mediator: TooltipMediatorService) {}

  ngOnInit() {
    this.datasetStorage.fetchDatasetHeaders().toPromise().then((dataset: DatasetHeader[]) => {
      this.datasetHeaders = dataset;
    });

    this.mediator$ = this.mediator$.add(this.mediator.datasetCreation.pipe(
      switchMap((name) => this.datasetStorage.storeEmptyDataset(name)),
      switchMap(() => this.datasetStorage.fetchDatasetHeaders())
    ).subscribe((dataset: DatasetHeader[]) => {
      this.datasetHeaders = dataset;
    }));

    this.mediator$ = this.mediator$.add(this.mediator.fileUploaded.pipe(
      switchMap(() => this.datasetStorage.fetchDatasetHeaders())
    ).subscribe((dataset: DatasetHeader[]) => {
      this.datasetHeaders = dataset;
    }));
  }

  ngOnDestroy() {
    this.mediator$.unsubscribe();
  }
}
