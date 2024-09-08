import { Component, OnDestroy } from '@angular/core';
import { Route, Router, RouterModule } from '@angular/router';
import { AddParticipantRequest } from '../models/add-participant-request.model';
import { Subscription } from 'rxjs';
import { ParticipantService } from '../services/participant.service';
import { CommonModule, NgOptimizedImage } from '@angular/common';
import { FormsModule } from '@angular/forms';

@Component({
  selector: 'app-add-participant',
  standalone: true,
  imports: [
    RouterModule,
    CommonModule,
    FormsModule,
    NgOptimizedImage
  ],
  templateUrl: './add-participant.component.html',
  styleUrl: './add-participant.component.css'
})
export class AddParticipantComponent implements OnDestroy {

  model: AddParticipantRequest
  private addParticipantSubscription?: Subscription

  constructor(private participantService: ParticipantService, private router: Router) {
    this.model = {
      name: '',
      surname: '',
      birthDate: new Date().toJSON().slice(0,10),
      //registrationDate: new Date().toJSON().slice(0,10),
      email: ''
    }
  }
  ngOnDestroy(): void {
    this.addParticipantSubscription?.unsubscribe();
  }

  onFormSubmit() {
    this.addParticipantSubscription = this.participantService.addParticipant(this.model)
      .subscribe({
        next: (responce) => {
          this.router.navigateByUrl('/admin/participant')
        }
      })
  }
}
