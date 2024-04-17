import { TestBed } from '@angular/core/testing';

import { ApiSpecficationsService } from './api-specfications.service';

describe('ApiSpecficationsService', () => {
  let service: ApiSpecficationsService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(ApiSpecficationsService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
