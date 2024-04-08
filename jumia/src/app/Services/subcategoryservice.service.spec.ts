import { TestBed } from '@angular/core/testing';

import { SubcategoryserviceService } from './subcategoryservice.service';

describe('SubcategoryserviceService', () => {
  let service: SubcategoryserviceService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(SubcategoryserviceService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
