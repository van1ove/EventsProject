import { Component } from '@angular/core';
import { AuthService } from './services/auth.service';
import { RouterModule } from '@angular/router';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';

@Component({
  selector: 'app-login',
  standalone: true,
  imports: [
    RouterModule,
    CommonModule,
    FormsModule
  ],
  templateUrl: './login.component.html',
  styleUrl: './login.component.css'
})
export class LoginComponent {
  public email: string = '';
  public password: string = '';

  constructor(private authService: AuthService){}

  public login(): void {
    if (this.email == '' || this.password == '')
    {
      alert('Email and password must be not empty!');
    }
    else{
      this.authService.login(this.email, this.password);
    }
  }
}
