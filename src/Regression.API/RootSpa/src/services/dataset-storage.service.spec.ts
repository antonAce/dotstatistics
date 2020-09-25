import { TestBed } from '@angular/core/testing';

import { DatasetStorageService } from './dataset-storage.service';

describe('DatasetStorageService', () => {
  beforeEach(() => TestBed.configureTestingModule({}));

  it('should be created', () => {
    const service: DatasetStorageService = TestBed.get(DatasetStorageService);
    expect(service).toBeTruthy();
  });
});
