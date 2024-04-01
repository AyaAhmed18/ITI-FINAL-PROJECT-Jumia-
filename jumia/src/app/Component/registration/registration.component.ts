import { CommonModule } from '@angular/common';
import { Component } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { RegistrationService } from '../../Services/registration.service';
import { RegisterDto } from '../../ViewModels/register-dto';

@Component({
  selector: 'app-registration',
  standalone: true,
  imports: [FormsModule,CommonModule],
  templateUrl: './registration.component.html',
  styleUrl: './registration.component.css'
})
export class RegistrationComponent {
  user:RegisterDto={} as RegisterDto
  constructor( private _registrationService :RegistrationService )
  {

  }
  register(username: string, email: string, password: string, confirmpass: string ,phonenumber :string) {
    if (password !== confirmpass) {
   
      alert("Password and Confirm Password do not match.");
      return; 
    }
    this._registrationService.register(username, email, password, confirmpass,phonenumber).subscribe({
      next: (res) => {
        console.log(res)
        alert("Successfully registered!");
      },
      error: (err) => {
        console.error(err);
        alert("Error occurred while registering. Please try again.");
      }
    });



}}
