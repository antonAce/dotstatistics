import { TestBed } from '@angular/core/testing';

import { ModelAnalysisService } from './model-analysis.service';

describe('ModelAnalysisService', () => {
  beforeEach(() => TestBed.configureTestingModule({}));

  it('should be created', () => {
    const service: ModelAnalysisService = TestBed.get(ModelAnalysisService);
    expect(service).toBeTruthy();
  });
});
