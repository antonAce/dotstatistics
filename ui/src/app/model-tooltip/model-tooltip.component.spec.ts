import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { ModelTooltipComponent } from './model-tooltip.component';

describe('ModelTooltipComponent', () => {
  let component: ModelTooltipComponent;
  let fixture: ComponentFixture<ModelTooltipComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ ModelTooltipComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(ModelTooltipComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
