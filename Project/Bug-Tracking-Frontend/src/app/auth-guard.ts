import { Injectable } from '@angular/core';
import { ActivatedRouteSnapshot, CanActivate, CanActivateFn, Router, RouterStateSnapshot } from '@angular/router';
import { authService } from './core/Services/authService';
@Injectable()
export class authGuard implements CanActivate {
   constructor(private router:Router,private authService:authService){

  }
 canActivate(route:ActivatedRouteSnapshot, state:RouterStateSnapshot):boolean{
  const isAuthenticated = document.cookie.includes("access_token");
  console.log(isAuthenticated)
  if(!isAuthenticated)
  {
    this.router.navigateByUrl("/login");
    return false;
  } 
   const allowedRoles = route.data['roles'] as Array<string>;
    const userRole = this.authService.role();

    if (allowedRoles && !allowedRoles.includes(userRole)) {
      console.warn(`Access denied for role: ${userRole}`);
      this.router.navigateByUrl('/login');
      return false;
    }

  return true;
 }
};
