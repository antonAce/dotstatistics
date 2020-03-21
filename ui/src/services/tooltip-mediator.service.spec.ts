import { TestBed } from '@angular/core/testing';

import { TooltipMediatorService } from './tooltip-mediator.service';

describe('TooltipMediatorService', () => {
  beforeEach(() => TestBed.configureTestingModule({}));

  it('should be created', () => {
    const service: TooltipMediatorService = TestBed.get(TooltipMediatorService);
    expect(service).toBeTruthy();
  });
});
