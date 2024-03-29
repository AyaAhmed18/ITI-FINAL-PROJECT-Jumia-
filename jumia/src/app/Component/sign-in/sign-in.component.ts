import { Component, OnInit } from '@angular/core';
import { ApiLoginService } from '../../Services/api-login.service';
import { Router } from '@angular/router';
import { FormsModule } from '@angular/forms';
import { CommonModule } from '@angular/common';
import { IUserLogin } from '../../Models/iuser-login';
import { BehaviorSubject } from 'rxjs';

@Component({
  selector: 'app-sign-in',
  standalone: true,
  imports: [FormsModule,CommonModule],
  templateUrl: './sign-in.component.html',
  styleUrl: './sign-in.component.css'
})
export class SignInComponent {
  isLogged:boolean=false
  loginRes:any
  loggedInUsername: string ="";
 // IsLoggedStatus!:BehaviorSubject<string>
  newUser:IUserLogin={} as IUserLogin
  constructor(private _apiLoginService : ApiLoginService , private router: Router){  }

 
//signIn Function Button
  signIn(username:string,password:string){
    this._apiLoginService.login(username,password).subscribe({next:(res)=>{
      if (res && res.stringtaken) {
        localStorage.setItem('token', res.stringtaken);
        this.loggedInUsername =username ; 
        this.router.navigate(['/Home']);
      } else {
        localStorage.removeItem('token');
      }
      localStorage.setItem('userName',this.loggedInUsername);
      console.log(res);
      
    },
    error:(err)=>{
      console.log(err);
      
    }});
    this.isLogged= this._apiLoginService.IsLoggedIn();
  }

  

  signOut(){
    this._apiLoginService.logout()
    this.isLogged= this._apiLoginService.IsLoggedIn();
    //this.router.navigate(['/Home']);
  }
}
