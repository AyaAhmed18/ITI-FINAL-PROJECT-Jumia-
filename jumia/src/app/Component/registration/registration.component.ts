import { CommonModule } from '@angular/common';
import { Component, OnInit } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { RegistrationService } from '../../Services/registration.service';
import { RegisterDto } from '../../ViewModels/register-dto';
import { TranslateModule, TranslateService } from '@ngx-translate/core';
import { RouterLink } from '@angular/router';

@Component({
  selector: 'app-registration',
  standalone: true,
  imports: [FormsModule,CommonModule ,TranslateModule,RouterLink],
  templateUrl: './registration.component.html',
  styleUrl: './registration.component.css'
})
export class RegistrationComponent  implements OnInit{
  user:RegisterDto={} as RegisterDto
  isArabic: boolean = false;
  constructor( private _registrationService :RegistrationService ,private  translate: TranslateService )
  {

  }
  ngOnInit(): void {
    this.translate.onLangChange.subscribe((Event)=>{
      this.isArabic = Event.lang === 'ar'
    })
  }
  changeLanguage(lang: string) {
    if (lang == 'en') {
      localStorage.setItem('lang', 'en')
    }
    else {
      localStorage.setItem('lang', 'ar')
    }

    window.location.reload();

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
