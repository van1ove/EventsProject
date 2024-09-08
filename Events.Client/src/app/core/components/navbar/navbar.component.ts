import { Component, OnDestroy } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { UserService } from '../../../features/user/service/user.service';
import { AuthService } from '../../../features/login/services/auth.service';
import { CommonModule } from '@angular/common';

@Component({
  selector: 'app-navbar',
  standalone: true,
  imports: [
    RouterModule,
    CommonModule
  ],
  templateUrl: './navbar.component.html',
  styleUrls: ['./navbar.component.css']
})
export class NavbarComponent{

  constructor(
    private userService: UserService, 
    private authService: AuthService) 
    {}

  public isAdmin(): boolean {
    return (this.userService.role != null && this.userService.role == 'Admin');
  }

  public logout(): void {
    this.authService.logout();
  }
}
