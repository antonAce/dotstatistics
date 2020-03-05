import { Component, OnInit, Input } from '@angular/core';
import { Row } from '@models/statistics/row';

@Component({
  selector: 'statistics-table',
  templateUrl: './statistics-table.component.html',
  styleUrls: ['./statistics-table.component.scss']
})
export class StatisticsTableComponent implements OnInit {
  @Input() rows: Row[];

  constructor() { }

  ngOnInit() {
  }

  getHeadingFormat(): string[] {
    if (this.rows.length == 0)
      return ["#", "X0", "Y"];
    else {
      let header = new Array<string>();
      header.push("#");

      for (let i = 0; i < this.rows[0].args.length; i++) {
        header.push("X" + i);
      }

      header.push("Y");
      return header;
    }
  }
}
