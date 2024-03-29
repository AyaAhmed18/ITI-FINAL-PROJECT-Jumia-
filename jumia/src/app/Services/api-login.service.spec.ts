import { TestBed } from '@angular/core/testing';

import { ApiLoginService } from './api-login.service';

describe('ApiLoginService', () => {
  let service: ApiLoginService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(ApiLoginService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
