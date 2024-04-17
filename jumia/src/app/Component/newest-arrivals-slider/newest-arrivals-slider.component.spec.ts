import { ComponentFixture, TestBed } from '@angular/core/testing';

import { NewestArrivalsSliderComponent } from './newest-arrivals-slider.component';

describe('NewestArrivalsSliderComponent', () => {
  let component: NewestArrivalsSliderComponent;
  let fixture: ComponentFixture<NewestArrivalsSliderComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [NewestArrivalsSliderComponent]
    })
    .compileComponents();
    
    fixture = TestBed.createComponent(NewestArrivalsSliderComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
