import { HttpEvent, HttpHandler, HttpInterceptor, HttpRequest } from '@angular/common/http';
import { Injectable, Injector } from '@angular/core';
import { catchError, Observable, throwError, BehaviorSubject, switchMap, tap, take } from 'rxjs';
import { AuthService } from './auth.service'
import { AuthResponse } from '../models/auth-response';

@Injectable({
  providedIn: 'root'
})
export class AuthInterceptor implements HttpInterceptor {
    private readonly publicUrls: string[] = ['api/Auth/login', 'api/Auth/register'];

    constructor(private authService: AuthService) { }

    public intercept(request: HttpRequest<any>, next: HttpHandler): Observable<HttpEvent<any>> {
      const token = localStorage.getItem('accessToken');

      const isPublicUrl = this.publicUrls.some(url => request.url.includes(url));

      if (token && !isPublicUrl) {
        request = request.clone({
          setHeaders: {
            Authorization: `Bearer ${token}`,
          },
        });
      }

      return next.handle(request).pipe(
        catchError(err => {
          if (err.status === 401 && !isPublicUrl) {
            return this.authService.generateRefreshToken().pipe(
              switchMap((response: AuthResponse) => {
                request = this.addTokenheader(request, response.accessToken);
                return next.handle(request);
              }),
              catchError(error => {
                this.authService.logout();
                return throwError(error);
              })
            );
          }
          return throwError(err);
        })
      );
    }

    private addTokenheader(request: HttpRequest<any>, token: any) {
      return request.clone({ headers: request.headers.set('Authorization', 'bearer ' + token) });
    }
}