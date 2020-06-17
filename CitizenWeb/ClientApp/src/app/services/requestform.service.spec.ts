import { TestBed } from '@angular/core/testing';

import { RequestFormService } from './requestform.service';

describe('RequestFormService', () => {
  beforeEach(() => TestBed.configureTestingModule({}));

  it('should be created', () => {
      const service: RequestFormService = TestBed.get(RequestFormService);
    expect(service).toBeTruthy();
  });
});
