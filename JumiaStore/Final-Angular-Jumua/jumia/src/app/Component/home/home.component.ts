import { IMAGE_CONFIG } from '@angular/common';
import { Component } from '@angular/core';
// const imgesUrl=require("../../../assets/ProductImg/ApplianceEN.png");
@Component({
  selector: 'app-home',
  standalone: true,
  imports: [],
  templateUrl: './home.component.html',
  styleUrl: './home.component.css',

  providers: [
    {
      provide: IMAGE_CONFIG,
      useValue: {
        disableImageSizeWarning: true, 
        disableImageLazyLoadWarning: true
      }
    }
  ],
})
export class HomeComponent {

}
