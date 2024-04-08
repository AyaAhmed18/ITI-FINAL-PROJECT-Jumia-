import { TestBed } from '@angular/core/testing';

import { ApiShippmentService } from './api-shippment.service';

describe('ApiShippmentService', () => {
  let service: ApiShippmentService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(ApiShippmentService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
