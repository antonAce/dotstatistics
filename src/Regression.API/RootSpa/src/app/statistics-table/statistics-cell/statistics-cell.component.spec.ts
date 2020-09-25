import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { StatisticsCellComponent } from './statistics-cell.component';

describe('StatisticsCellComponent', () => {
  let component: StatisticsCellComponent;
  let fixture: ComponentFixture<StatisticsCellComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ StatisticsCellComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(StatisticsCellComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
