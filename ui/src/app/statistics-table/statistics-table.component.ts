import { Component, Input, Output, EventEmitter } from '@angular/core';
import { Row } from '@models/analytics';

@Component({
  selector: 'statistics-table',
  templateUrl: './statistics-table.component.html',
  styleUrls: ['./statistics-table.component.scss']
})
export class StatisticsTableComponent {
  @Input()
  get rows() {
    return this._rows;
  }

  @Output() rowsChange = new EventEmitter();

  set rows(rows) {
    this._rows = rows;
    this.rowsChange.emit(this._rows);
  }

  private _rows: Row[];

  getHeadingFormat(): string[] {
    if (this._rows.length == 0)
      return ["#", "X0", "Y"];
    else {
      let header = new Array<string>();
      header.push("#");

      for (let i = 0; i < this._rows[0].args.length; i++) {
        header.push("X" + i);
      }

      header.push("Y");
      return header;
    }
  }
}
