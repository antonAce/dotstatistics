import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'model-tooltip',
  templateUrl: './model-tooltip.component.html',
  styleUrls: ['./model-tooltip.component.scss']
})
export class ModelTooltipComponent implements OnInit {
  models: string[];

  constructor() {
    this.models = ["Test Regression", "datafile.dat", "example.dat"];
  }

  ngOnInit() {
  }

}
