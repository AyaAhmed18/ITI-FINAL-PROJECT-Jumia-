import { ComponentFixture, TestBed } from '@angular/core/testing';

import { DelivaryComponent } from './delivary.component';

describe('DelivaryComponent', () => {
  let component: DelivaryComponent;
  let fixture: ComponentFixture<DelivaryComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [DelivaryComponent]
    })
    .compileComponents();
    
    fixture = TestBed.createComponent(DelivaryComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
