import { Injectable, OnDestroy } from '@angular/core';
import {
  ActivatedRouteSnapshot,
  CanActivate,
  CanActivateChild,
  GuardResult,
  MaybeAsync,
  Router,
  RouterStateSnapshot,
} from '@angular/router';
import { AuthService } from './auth.service';
import { PUBLIC_URL_CONST } from '../../auth/consts/publicUrls';
import { UserService } from '../../user/service/user.service';

@Injectable({providedIn: 'root'})
export class RoleGuard implements CanActivate, CanActivateChild {
    public publicUrls: string[] = PUBLIC_URL_CONST;
    private previousUrl: string = '';

    constructor(
        private authService: AuthService,
        private userService: UserService,
        private router: Router
    ) {}

    canActivateChild(childRoute: ActivatedRouteSnapshot, state: RouterStateSnapshot): MaybeAsync<GuardResult> {
        return this.canActivate(childRoute, state);
    }
    
    canActivate(route: ActivatedRouteSnapshot, state: RouterStateSnapshot): MaybeAsync<GuardResult> {
        const currentUrl = route.url.join('/');
        
        if (this.publicUrls.includes(currentUrl))
        {
            localStorage.setItem('returnUrl', currentUrl);
            return true;
        }

        if (!this.userService.user)
        {
            alert('Please, log in');
            this.authService.logout();
            return false;
        }

        const isRoleBasedUrl = route.data['role'] != undefined;

        if (isRoleBasedUrl && this.userService.user.role != route.data['role'])
        {
            alert('Access denied!');
            this.router.navigateByUrl(this.returnUrl());
            return false;
        }

        localStorage.setItem('returnUrl', currentUrl);
        return true;
    }

    private returnUrl(): string {
        return localStorage.getItem('returnUrl') ?? '';
    }
}