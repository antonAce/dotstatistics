import { Component, OnInit, Input, Output, EventEmitter, HostListener, ElementRef } from '@angular/core';

@Component({
  selector: 'statistics-cell',
  templateUrl: './statistics-cell.component.html',
  styleUrls: ['./statistics-cell.component.scss']
})
export class StatisticsCellComponent implements OnInit {
  @Input() isReadonly: boolean;

  @Input()
  set value(val: number) {
    this.onValueChanged(val);
  }

  @Output() valueChange = new EventEmitter<number>();

  get value(): number {
    return this._value;
  }

  get IsInEditMode(): boolean {
    return this.isInEditMode;
  }

  private _value: number;
  private isInEditMode: boolean = false;

  constructor() { }

  ngOnInit() {
  }

  @HostListener("dblclick") onDoubleClick() {
    if (!this.isInEditMode && !this.isReadonly) this.isInEditMode = true;
  }

  onKeydown(event, cell: HTMLInputElement) {
    if (this.isInEditMode && event.key === "Enter") {
      this.onValueChanged(Number(cell.value))
    }
  }

  onBlur(cell: HTMLInputElement) {
    if (this.isInEditMode) {
      this.onValueChanged(Number(cell.value))
    }
  }

  onValueChanged(val: number) {
    this.isInEditMode = false;
    this._value = val;
    this.valueChange.emit(this._value);
  }
}
