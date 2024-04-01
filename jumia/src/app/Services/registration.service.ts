import { Injectable } from '@angular/core';
import { environment } from '../../environment/environment';
import { HttpClient } from '@angular/common/http';

@Injectable({
  providedIn: 'root'
})
export class RegistrationService {
  private apiUrl = environment.apiUrl;

  constructor(private httpClient: HttpClient) { }

  register(username: string, email: string, password: string, confirmpass: string,phone:string) {
    const userDetails = { username, email, password, confirmpass ,phone };
    return this.httpClient.post<any>(`${this.apiUrl}/Account/Register`, userDetails)   
  }
}
