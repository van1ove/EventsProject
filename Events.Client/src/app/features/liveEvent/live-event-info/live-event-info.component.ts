import { CommonModule } from '@angular/common';
import { Component, OnDestroy, OnInit } from '@angular/core';
import { RouterModule, ActivatedRoute } from '@angular/router';
import { Subscription } from 'rxjs';
import { LiveEvent } from '../models/live-event.model';
import { LiveEventService } from '../services/live-event.service';
import { UserService } from '../../user/service/user.service';
@Component({
  selector: 'app-live-event-info',
  standalone: true,
  imports: [
    RouterModule,
    CommonModule
  ],
  templateUrl: './live-event-info.component.html',
  styleUrl: './live-event-info.component.css'
})
export class LiveEventInfoComponent implements OnInit, OnDestroy{

  liveEvent? : LiveEvent
  id: string | null = null

  paramsSunscription?: Subscription;

  constructor(
    private route : ActivatedRoute, 
    private liveEventService : LiveEventService,
    private userService: UserService) {

  }

  ngOnInit(): void {

    this.paramsSunscription = this.route.paramMap.subscribe({
      next: (params) => {
        this.id = params.get('id')

        if(this.id){
          this.liveEventService.getLiveEventById(this.id).subscribe({
            next : (responce) => {
              this.liveEvent = responce;
            }
          });
        }
      }
    });
  }

  ngOnDestroy(): void {
    this.paramsSunscription?.unsubscribe();
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
