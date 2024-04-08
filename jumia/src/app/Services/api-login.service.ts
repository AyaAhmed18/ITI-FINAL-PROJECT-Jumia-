import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { environment } from '../../environment/environment';
import { IUserLogin } from '../Models/iuser-login';
import { BehaviorSubject, Observable } from 'rxjs';
@Injectable({
  providedIn: 'root'
})
export class ApiLoginService {

  private apiUrl = environment.apiUrl;
  loggedInUsername: string ="Aya";
  loggedInUsername2!:BehaviorSubject<string>
  IsLoggedStatus!:BehaviorSubject<boolean>
  constructor(private httpClient:HttpClient) {
    this.IsLoggedStatus=new BehaviorSubject<boolean>(this.IsLoggedIn())
    this.loggedInUsername2=new BehaviorSubject<string>(this.getLoggedInUsername())

   }
  
  //Login
  login(username: string, password: string): Observable<any> {
    return this.httpClient.post<any>(`${this.apiUrl}/Account/Login`, { username, password });
  }
//LogOut
  logout(){
    localStorage.removeItem('token'); 
    localStorage.removeItem('userName'); 
    localStorage.removeItem('userId');
    this.loggedInUsername2.next("Account")
    this.IsLoggedStatus.next(false)
  }
  //Check
  IsLoggedIn():boolean{
    return localStorage.getItem('token')?true:false
  }

  getLoggedInUsername(): string {
    var user=localStorage.getItem('userName')
    if( user != null ){
      return user
  }else{
    return "Account"
  }
    }
    
   gettName2():BehaviorSubject<string>{
    return this.loggedInUsername2;
   } 
   getLoggedStatus():BehaviorSubject<boolean>{
    return this.IsLoggedStatus
   } 
}
