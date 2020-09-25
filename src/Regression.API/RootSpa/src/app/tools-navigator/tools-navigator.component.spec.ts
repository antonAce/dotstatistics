import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { ToolsNavigatorComponent } from './tools-navigator.component';

describe('ToolsNavigatorComponent', () => {
  let component: ToolsNavigatorComponent;
  let fixture: ComponentFixture<ToolsNavigatorComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ ToolsNavigatorComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(ToolsNavigatorComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
