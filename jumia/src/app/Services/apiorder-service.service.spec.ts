import { TestBed } from '@angular/core/testing';

import { APIOrderServiceService } from './apiorder-service.service';

describe('APIOrderServiceService', () => {
  let service: APIOrderServiceService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(APIOrderServiceService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
