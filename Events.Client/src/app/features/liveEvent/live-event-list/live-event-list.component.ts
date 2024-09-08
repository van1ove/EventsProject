import { Component, OnInit } from '@angular/core';
import { RouterModule } from '@angular/router';
import { LiveEventService } from '../services/live-event.service';
import { LiveEvent } from '../models/live-event.model';
import { CommonModule } from '@angular/common';
import { Observable } from 'rxjs';
import { UserService } from '../../user/service/user.service';

@Component({
  selector: 'app-live-event-list',
  standalone: true,
  imports: [
    RouterModule,
    CommonModule
  ],
  templateUrl: './live-event-list.component.html',
  styleUrl: './live-event-list.component.css'
})
export class LiveEventListComponent implements OnInit{

  liveEvents$? : Observable<LiveEvent[]>;

  constructor(
    private liveEventService : LiveEventService,
    private userService: UserService) {
  }

  ngOnInit(): void {
    this.liveEvents$ = this.liveEventService.getAllLiveEvents();
  }

  public isAdmin(): boolean {
    const role = this.userService.role;
    
    if (role)
    {
      return role === 'Admin';
    }

    return false;
  }
}
