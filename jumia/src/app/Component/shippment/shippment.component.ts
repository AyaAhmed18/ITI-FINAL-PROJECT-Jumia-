import { CommonModule } from '@angular/common';
import { HttpClientModule } from '@angular/common/http';
import { Component } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { RouterLink, RouterOutlet } from '@angular/router';
import { IShippment } from '../../Models/ishippment';

@Component({
  selector: 'app-shippment',
  standalone: true,
  imports: [RouterLink,RouterOutlet,HttpClientModule,FormsModule,CommonModule],
  templateUrl: './shippment.component.html',
  styleUrl: './shippment.component.css'
})
export class ShippmentComponent {
  shippment!:IShippment
}
