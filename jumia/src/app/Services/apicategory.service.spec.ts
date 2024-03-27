import { TestBed } from '@angular/core/testing';

import { APICategoryService } from './apicategory.service';

describe('APICategoryService', () => {
  let service: APICategoryService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(APICategoryService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
