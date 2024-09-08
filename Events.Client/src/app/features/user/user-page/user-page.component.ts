import { Component, OnDestroy, OnInit } from '@angular/core';
import { UserService } from '../service/user.service';
import { User } from '../../participant/models/user.model';

@Component({
  selector: 'app-user-page',
  standalone: true,
  imports: [],
  templateUrl: './user-page.component.html',
  styleUrl: './user-page.component.css'
})
export class UserPageComponent implements OnInit {

  public user: User | null = null;

  constructor(
    private userService: UserService) 
  {
    this.user = userService.user;
  }

  ngOnInit(): void {
    
  }
}
