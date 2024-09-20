import { CanActivateFn } from '@angular/router';
import { AuthorizationService } from '../http/authorization.service';
import { Router } from '@angular/router';
import { ActivatedRouteSnapshot, RouterStateSnapshot } from "@angular/router";
import { inject } from '@angular/core';

export const homeEnter1Guard : CanActivateFn = (route: ActivatedRouteSnapshot, state :RouterStateSnapshot) => {
  const router = inject(Router);  
  const authService = inject(AuthorizationService);  
  let result = Verification(router,authService);
  return result;
};

export function Verification(router: Router, authorizationService : AuthorizationService){
    authorizationService.Verification().subscribe({
    next: (response) => {
      return false;        
    },
    error: (err) => {
      router.navigate(["/login"]);
      return true;
    }
  });
  return true;
};
