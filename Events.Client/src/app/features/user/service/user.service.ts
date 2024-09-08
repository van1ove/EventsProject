import { Injectable } from '@angular/core';
import { User } from '../../participant/models/user.model';
import { BehaviorSubject, catchError, tap, throwError } from 'rxjs';
import { HttpClient } from '@angular/common/http';
import { environment } from '../../../../environments/environment';
import { Router } from '@angular/router';

@Injectable({
  providedIn: 'root'
})
export class UserService {
  private _user: User | null = null;

  constructor(
    private http: HttpClient,
    private router: Router){
      const userStr = localStorage.getItem('user');

      if (userStr) {
        this._user = JSON.parse(userStr) as User;
      }
  }

  setUser(user: User | null): void {
    if (user){
      localStorage.setItem('user', JSON.stringify(user));
    }

    this._user = user;
  }

  get role(): string | null {
    return this._user ? this._user.role : '';
  }

  get user(): User | null {
    return this._user;
  }

  clearUser(): void {
    localStorage.removeItem('user');
    this.setUser(null);
  }

  public register(user: User) {
    return this.http.post(`${environment}/api/Auth/register`, user)
      .pipe(
        tap(res => {
          if (res){
            alert('Registration successful');
            this.router.navigateByUrl('login');
          }
        }), catchError(error => {
          alert('Registration faild, try again');
          this.router.navigateByUrl('registration');
          return throwError(error);
        }));
  }

  public getAllUsers() {
  }
}
