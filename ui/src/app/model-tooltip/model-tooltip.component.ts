import { Component, OnInit, OnDestroy } from '@angular/core';

import { Subscription } from 'rxjs';

import { DatasetHeader } from '@models/dataset';
import { DatasetStorageService } from '@services/dataset-storage.service';

@Component({
  selector: 'model-tooltip',
  templateUrl: './model-tooltip.component.html',
  styleUrls: ['./model-tooltip.component.scss']
})
export class ModelTooltipComponent implements OnInit, OnDestroy {
  datasetHeaders: DatasetHeader[];

  private datasetStorage$ = new Subscription();

  constructor(private datasetStorage: DatasetStorageService) {}

  ngOnInit() {
    this.datasetStorage$.add(this.datasetStorage.fetchDatasetHeaders().subscribe((dataset: DatasetHeader[]) => {
      this.datasetHeaders = dataset;
    }));
  }

  ngOnDestroy() {
    this.datasetStorage$.unsubscribe();
  }
}
