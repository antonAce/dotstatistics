import { TestBed } from '@angular/core/testing';

import { ModelSerializationService } from './model-serialization.service';

describe('ModelSerializationService', () => {
  beforeEach(() => TestBed.configureTestingModule({}));

  it('should be created', () => {
    const service: ModelSerializationService = TestBed.get(ModelSerializationService);
    expect(service).toBeTruthy();
  });
});
