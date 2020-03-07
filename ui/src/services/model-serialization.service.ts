import { Injectable } from '@angular/core';

import { RegressionModel } from '@models/statistics/regression-model';
import { Row } from '@models/statistics/row';

@Injectable({
  providedIn: 'root'
})
export class ModelSerializationService {
  private currentModel: RegressionModel;

  constructor() { 
    this.currentModel = new RegressionModel();
    this.currentModel.rows = [
      {
        id: 1,
        args: [10.0, 23.0, 12.0],
        result: 5.0
      },
      {
        id: 2,
        args: [22.0, 6.0, 12.0],
        result: 1.0
      },
      {
        id: 3,
        args: [15.0, 12.0, 3.0],
        result: 3.0
      },
      {
        id: 4,
        args: [22.0, 12.0, 5.0],
        result: 2.0
      },
      {
        id: 5,
        args: [7.0, 15.0, 10.0],
        result: 7.0
      },
      {
        id: 6,
        args: [14.0, 20.0, 5.0],
        result: 4.0
      },
      {
        id: 7,
        args: [20.0, 3.0, 18.0],
        result: 1.0
      }
    ];
  }

  empty(args: number, rows: number): RegressionModel {
    let model = new RegressionModel();

    for (let index = 0; index < rows; index++)
      model.rows.push({
        id: index + 1,
        args: Array(args).fill(0),
        result: 0
      });

    return model;
  }

  storeModel(model: RegressionModel): void {
    this.currentModel = model;
  }

  storeModelByRows(rows: Row[]): void {
    this.currentModel.rows = rows;
  }

  getModel(): RegressionModel {
    return this.currentModel;
  }
}
