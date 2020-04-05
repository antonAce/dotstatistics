import { TestBed } from '@angular/core/testing';

import { DatasetAnalysisService } from './dataset-analysis.service';

describe('DatasetAnalysisService', () => {
  beforeEach(() => TestBed.configureTestingModule({}));

  it('should be created', () => {
    const service: DatasetAnalysisService = TestBed.get(DatasetAnalysisService);
    expect(service).toBeTruthy();
  });
});
