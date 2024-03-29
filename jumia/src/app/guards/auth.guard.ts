import { inject } from '@angular/core';
import { CanActivateFn, Router } from '@angular/router';
import { ApiLoginService } from '../Services/api-login.service';

export const authGuard: CanActivateFn = (route, state) => {
 let userAuth= inject(ApiLoginService)
 let router=inject(Router)
 if(userAuth.IsLoggedIn()){
  return true
 }
 else{
 alert('you must login First')
 router.navigate(['/SignIn']);
  return false
 }
  
};
