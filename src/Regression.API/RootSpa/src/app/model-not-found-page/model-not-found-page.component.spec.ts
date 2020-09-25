import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { ModelNotFoundPageComponent } from './model-not-found-page.component';

describe('ModelNotFoundPageComponent', () => {
  let component: ModelNotFoundPageComponent;
  let fixture: ComponentFixture<ModelNotFoundPageComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ ModelNotFoundPageComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(ModelNotFoundPageComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
