import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'statistics-page',
  templateUrl: './statistics-page.component.html',
  styleUrls: ['./statistics-page.component.scss']
})
export class StatisticsPageComponent implements OnInit {
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

  constructor() { }

  ngOnInit() {
  }

}
