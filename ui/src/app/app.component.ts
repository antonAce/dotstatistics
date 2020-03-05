import { Component } from '@angular/core';
import { Row } from '@models/statistics/row';

@Component({
  selector: 'root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.scss']
})
export class AppComponent {
  title = 'ASCM Regression Platform';
  rows = [
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
  ]
}
