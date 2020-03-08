import { Component, OnInit, Input, Output, EventEmitter, HostListener, ElementRef } from '@angular/core';

@Component({
  selector: 'statistics-cell',
  templateUrl: './statistics-cell.component.html',
  styleUrls: ['./statistics-cell.component.scss']
})
export class StatisticsCellComponent implements OnInit {
  @Input() isReadonly: boolean;

  @Input()
  get value(): number {
    return this._value;
  }

  @Output() valueChange = new EventEmitter<number>();

  set value(val: number) {
    this.onValueChanged(val);
  }

  private _value: number;
  private IsInEditMode: boolean = false;

  constructor() { }

  ngOnInit() {
  }

  @HostListener("dblclick") onDoubleClick() {
    if (!this.IsInEditMode && !this.isReadonly) this.IsInEditMode = true;
  }

  onKeydown(event, cell: HTMLInputElement) {
    if (this.IsInEditMode && event.key === "Enter") {
      this.IsInEditMode = false;
      this.onValueChanged(Number(cell.value))
    }
  }

  onValueChanged(val: number) {
    this._value = val;
    this.valueChange.emit(this._value);
  }
}
