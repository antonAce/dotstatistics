import { Component, OnInit, Input, Output, EventEmitter } from '@angular/core';

@Component({
  selector: 'statistics-cell',
  templateUrl: './statistics-cell.component.html',
  styleUrls: ['./statistics-cell.component.scss']
})
export class StatisticsCellComponent implements OnInit {
  @Input() isReadonly: boolean;

  @Input()
  get value() {
    return this._value;
  }

  @Output() valueChange = new EventEmitter();

  set value(val) {
    this._value = val;
    this.valueChange.emit(this._value);
  }

  private _value: number;

  constructor() { }

  ngOnInit() {
  }

}
