import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Router } from '@angular/router';
import { environment } from '../../../../environments/environment';
import { AuthResponse } from '../models/auth-response';
import { catchError, Observable, tap, throwError } from 'rxjs';
import { UserService } from '../../user/service/user.service';

@Injectable({providedIn: 'root'})
export class AuthService {
    constructor(private http: HttpClient, 
        private router: Router, 
        private userService: UserService) { }
    
    public login(email: string, password: string) {
        let url = `${environment.apiBaseUrl}/api/Auth/login`;

        this.http.post<AuthResponse>(url, { email, password })
            .subscribe((res) => {
                if (res){
                    this.saveAuth(res);
                    this.router.navigateByUrl('');
                }
            });
    }

    public isLogged(): boolean {
        return this.userService.user != null;
    }

    public logout(): void {
        localStorage.removeItem('accessToken');
        localStorage.removeItem('refreshToken');
        localStorage.removeItem('returnUrl');
        this.userService.clearUser();
        this.router.navigateByUrl('login');
    }

    public getAccessToken(): string {
        return localStorage.getItem("accessToken") || '';
    }

    public getRefreshToken(): string {
        return localStorage.getItem("refreshToken") || '';
    }
    
    public saveAuth(auth: AuthResponse) {
        localStorage.setItem('accessToken', auth.accessToken);
        localStorage.setItem('refreshToken', auth.refreshToken);
        this.userService.setUser(auth.user);
    }

    public generateRefreshToken(): Observable<AuthResponse> {
        if (!this.userService.user)
        {
            alert('Please, log in');
            this.logout();
            return throwError('');
        }
        else{
            const dto = {
                userId: this.userService.user.id,
                accessToken: this.getAccessToken(),
                refreshToken: this.getRefreshToken(),
            };
    
            return this.http.post<AuthResponse>(`${environment.apiBaseUrl}/api/Auth/refresh-token`, dto).pipe(
              tap(response => {
                localStorage.setItem('accessToken', response.accessToken);
                localStorage.setItem('refreshToken', response.refreshToken);
              }),
              catchError(error => {
                console.error('Refresh token failed', error);
                this.logout();
                return throwError(error);
              })
            );
        }
    }
}