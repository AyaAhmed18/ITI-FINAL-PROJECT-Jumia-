import { Component } from '@angular/core';
import { RouterLink, RouterOutlet } from '@angular/router';

@Component({
  selector: 'app-navigiation-bar',
  standalone: true,
  imports: [RouterLink,RouterOutlet],
  templateUrl: './navigiation-bar.component.html',
  styleUrl: './navigiation-bar.component.css'
})
export class NavigiationBarComponent {

}
