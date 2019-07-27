import { Injectable } from '@angular/core';
import { Router,  ActivatedRouteSnapshot, RouterStateSnapshot, UrlTree ,CanActivateChild } from '@angular/router';
import { Observable } from 'rxjs';
import {ApiClientService} from './api-client.service';


@Injectable({
  providedIn: 'root'
})
export class AuthGuard implements  CanActivateChild {

  constructor(private registerapi :ApiClientService , public router: Router) {}

  canActivateChild(route: ActivatedRouteSnapshot, state: RouterStateSnapshot): boolean {
  if (!this.registerapi.checktokenstatus())   {
    this.router.navigate(['logout']);
    return false;
  }

  return true;
}

}
