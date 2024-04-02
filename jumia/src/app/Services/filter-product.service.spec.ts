import { TestBed } from '@angular/core/testing';

import { FilterProductService } from './filter-product.service';

describe('FilterProductService', () => {
  let service: FilterProductService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(FilterProductService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
