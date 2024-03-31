import { ComponentFixture, TestBed } from '@angular/core/testing';

import { NavigiationBarComponent } from './navigiation-bar.component';

describe('NavigiationBarComponent', () => {
  let component: NavigiationBarComponent;
  let fixture: ComponentFixture<NavigiationBarComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [NavigiationBarComponent]
    })
    .compileComponents();
    
    fixture = TestBed.createComponent(NavigiationBarComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
