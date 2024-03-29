import { Component, OnChanges, OnInit, SimpleChanges } from '@angular/core';
import { Router, RouterLink, RouterOutlet } from '@angular/router';
import { ApiLoginService } from '../../Services/api-login.service';

@Component({
  selector: 'app-navigiation-bar',
  standalone: true,
  imports: [RouterLink,RouterOutlet],
  templateUrl: './navigiation-bar.component.html',
  styleUrl: './navigiation-bar.component.css'
})
export class NavigiationBarComponent implements OnInit {
  loggedInUsername: string="";

  //load page and check if logged or not
  IsUserLogged:boolean=false
  ngOnInit() {
   // this.IsUserLogged= this._apiLoginService.IsLoggedIn();
  // this.loggedInUsername = this._apiLoginService.getLoggedInUsername();
    this._apiLoginService.getLoggedStatus().subscribe((stat)=>{
    this.IsUserLogged=stat
   })
   this._apiLoginService.gettName2().subscribe((stat)=>{
    this.loggedInUsername=stat
   })
   }
  constructor(private _apiLoginService : ApiLoginService , private router: Router){
    this.loggedInUsername = this._apiLoginService.getLoggedInUsername();
    if(this._apiLoginService.IsLoggedIn()){
      
    }
    else{
      this._apiLoginService.logout();
    }
    
  }
  SignInNav(){

    this.IsUserLogged= this._apiLoginService.IsLoggedIn();
  }
  SignOutNav(){
    this._apiLoginService.logout();
    this.IsUserLogged= this._apiLoginService.IsLoggedIn();
  }
}
