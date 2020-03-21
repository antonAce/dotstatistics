import { Component, OnInit, OnDestroy } from '@angular/core';
import { ActivatedRoute } from '@angular/router';

import { Subscription } from 'rxjs';

import { Row } from '@models/analytics';
import { DatasetToRead, DatasetToSave, Record } from '@models/dataset';

import { DatasetStorageService } from '@services/dataset-storage.service';
import { TooltipMediatorService } from '@services/tooltip-mediator.service';

@Component({
  selector: 'statistics-page',
  templateUrl: './statistics-page.component.html',
  styleUrls: ['./statistics-page.component.scss']
})
export class StatisticsPageComponent implements OnInit, OnDestroy {
  private datasetId: string
  private datasetName: string;
  private rows: Row[];

  private datasetStorage$ = new Subscription();
  private routeChange$ = new Subscription();
  private mediator$ = new Subscription();

  constructor(private datasetStorage: DatasetStorageService,
              private mediator: TooltipMediatorService,
              private activateRoute: ActivatedRoute) { }

  ngOnInit() {
    this.routeChange$ = this.activateRoute.params.subscribe((params) => {
      this.datasetId = params['id'];
      this.datasetStorage$.add(this.datasetStorage.getDatasetById(this.datasetId).subscribe((dataset: DatasetToRead) => {
        let i = 0;
        this.datasetName = dataset.name;
        this.rows = dataset.records.map(record => <Row> {
          id: ++i,
          args: record.inputs,
          result: record.output
        });
      }));
    });

    this.mediator$ = this.mediator.datasetSaveChanges.subscribe(() => this.updateDataset(this.rows));
  }

  ngOnDestroy() {
    this.datasetStorage$.unsubscribe();
    this.routeChange$.unsubscribe();
    this.mediator$.unsubscribe();
  }

  appendColumn() {
    this.rows.forEach(element => {
      element.args.push(0);
    });
  }

  removeColumn() {
    this.rows.forEach(element => {
      element.args.pop();
    });
  }

  appendRow() {
    this.rows.push({ 
      id: this.rows.length + 1,
      args: Array(this.rows[0].args.length).fill(0),
      result: 0
    });
  }

  removeRow() {
    if (this.rows.length > 1)
      this.rows.pop();
  }

  private updateDataset(rows: Row[]) {
    let dataset = <DatasetToSave>{
      name: this.datasetName,
      records: this.rows.map(row => <Record>{
        inputs: row.args,
        output: row.result
      })
    };

    this.datasetStorage$.add(this.datasetStorage.updateDataset(this.datasetId, dataset).subscribe());
  }
}
