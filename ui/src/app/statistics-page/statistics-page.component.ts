import { Component, OnInit, OnDestroy } from '@angular/core';
import { ActivatedRoute } from '@angular/router';

import { Subscription } from 'rxjs';

import { Row } from '@models/analytics';
import { DatasetToRead, DatasetToSave, Record } from '@models/dataset';

import { DatasetStorageService } from '@services/dataset-storage.service';

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

  constructor(private datasetStorage: DatasetStorageService,
              private activateRoute: ActivatedRoute) { }

  ngOnInit() {
    this.datasetId = this.activateRoute.snapshot.params['id'];
    this.datasetStorage$.add(this.datasetStorage.getDatasetById(this.datasetId).subscribe((dataset: DatasetToRead) => {
      let i = 0;
      this.datasetName = dataset.name;
      this.rows = dataset.records.map(record => <Row> {
        id: ++i,
        args: record.inputs,
        result: record.output
      });
    }));
  }

  ngOnDestroy() {
    this.datasetStorage$.unsubscribe();
  }

  appendColumn() {
    this.rows.forEach(element => {
      element.args.push(0);
    });

    this.updateDataset(this.rows);
  }

  removeColumn() {
    this.rows.forEach(element => {
      element.args.pop();
    });

    this.updateDataset(this.rows);
  }

  appendRow() {
    this.rows.push({ 
      id: this.rows.length + 1,
      args: Array(this.rows[0].args.length).fill(0),
      result: 0
    });

    this.updateDataset(this.rows);
  }

  removeRow() {
    if (this.rows.length > 1)
      this.rows.pop();

    this.updateDataset(this.rows);
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
