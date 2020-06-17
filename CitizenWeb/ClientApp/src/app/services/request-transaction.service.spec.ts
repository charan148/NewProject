import { TestBed } from '@angular/core/testing';

import { RequestTransactionService } from './request-transaction.service';

describe('RequestTransactionService', () => {
  let service: RequestTransactionService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(RequestTransactionService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
