import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { PlotElementComponent } from './plot-element.component';

describe('PlotElementComponent', () => {
  let component: PlotElementComponent;
  let fixture: ComponentFixture<PlotElementComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ PlotElementComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(PlotElementComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
