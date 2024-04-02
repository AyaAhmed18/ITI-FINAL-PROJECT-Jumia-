import { TestBed } from '@angular/core/testing';

import { BrandServiceService } from './brand-service.service';

describe('BrandServiceService', () => {
  let service: BrandServiceService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(BrandServiceService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
