import { Injectable } from '@angular/core';
import { ActivatedRouteSnapshot, CanActivate, CanActivateFn, Router, RouterStateSnapshot } from '@angular/router';
@Injectable()
export class authGuard implements CanActivate {
   constructor(private router:Router){

  }
 canActivate(route:ActivatedRouteSnapshot, state:RouterStateSnapshot):boolean{
  const isAuthenticated = localStorage.getItem("token")?true:false;
  if(!isAuthenticated)
  {
    this.router.navigateByUrl("/login");
    return false;
  } 
  return true;
 }
};
