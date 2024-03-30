import { Component } from '@angular/core';
import { RouterLink, RouterOutlet } from '@angular/router';
import { ApiProductsService } from '../../Services/api-products.service';


@Component({
  selector: 'app-navigiation-bar',
  standalone: true,
  imports: [RouterLink,RouterOutlet],
  templateUrl: './navigiation-bar.component.html',
  styleUrl: './navigiation-bar.component.css'
})
export class NavigiationBarComponent {
  

  constructor() { }

  
  
}
