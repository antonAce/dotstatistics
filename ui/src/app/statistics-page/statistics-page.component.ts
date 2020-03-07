import { Component, OnInit } from '@angular/core';

import { Row } from '@models/statistics/row';
import { ModelSerializationService } from '@services/model-serialization.service';

@Component({
  selector: 'statistics-page',
  templateUrl: './statistics-page.component.html',
  styleUrls: ['./statistics-page.component.scss']
})
export class StatisticsPageComponent implements OnInit {
  private rows: Row[];

  constructor(private modelSerializer: ModelSerializationService) { }

  ngOnInit() {
    this.rows = this.modelSerializer.getModel().rows;
  }

  appendColumn(): void {
    this.rows.forEach(element => {
      element.args.push(0);
    });
  }

  removeColumn(): void {
    this.rows.forEach(element => {
      element.args.pop();
    });
  }

  appendRow(): void {
    this.rows.push({ 
      id: this.rows.length + 1,
      args: Array(this.rows[0].args.length).fill(0),
      result: 0
    });
  }

  removeRow(): void {
    if (this.rows.length > 1)
      this.rows.pop();
  }

  saveModel(): void {
    this.modelSerializer.storeModelByRows(this.rows);
  }
}
