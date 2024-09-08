import { Component, OnDestroy, OnInit } from '@angular/core';
import { ActivatedRoute, Router, RouterModule } from '@angular/router';
import { catchError, Subscription, throwError } from 'rxjs';
import { LiveEventService } from '../services/live-event.service';
import { LiveEvent } from '../models/live-event.model';
import { FormsModule } from '@angular/forms';
import { CommonModule } from '@angular/common';
import { CATEGORIES_CONST } from '../consts/categories';
import { EditLiveEventRequest } from '../models/edit-live-event.request.model';

@Component({
  selector: 'app-edit-live-event',
  standalone: true,
  imports: [
    RouterModule,
    FormsModule,
    CommonModule
  ],
  templateUrl: './edit-live-event.component.html',
  styleUrl: './edit-live-event.component.css'
})
export class EditLiveEventComponent implements OnInit, OnDestroy{

  isModelValid : boolean = true

  readonly categories : Array<string>

  liveEvent? : LiveEvent
  id: string | null = null
  image?: File

  paramsSunscription?: Subscription;
  editLiveEventSubscription? : Subscription

  constructor(private route : ActivatedRoute, private liveEventService : LiveEventService,
    private router : Router) {
    this.categories = CATEGORIES_CONST
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
    this.editLiveEventSubscription?.unsubscribe();
  }

  onFormSubmit() : void {
    if(this.liveEvent){
      if(this.liveEvent.date == '' || this.liveEvent.title.length < 3 || this.liveEvent.time == '' || this.liveEvent.place.length < 3){
        this.isModelValid = false
      }
      else {
        this.isModelValid = true
      }
    }

    if(!this.isModelValid)
      return;

    const updatedLiveEvent : EditLiveEventRequest = {
      title: this.liveEvent?.title ?? '',
      description: this.liveEvent?.description ?? '',
      date: this.liveEvent?.date ?? new Date().toJSON().slice(0,10),
      time: this.liveEvent?.time   ?? new Date().toJSON().slice(11,16),
      place: this.liveEvent?.place ?? '',
      category: this.liveEvent?.category ?? '',
      maxParticipants: this.liveEvent?.maxParticipants ?? 10,
      imageUrl: this.liveEvent?.title ?? ''
    }

    if(this.id){
      this.editLiveEventSubscription = this.liveEventService.updateLiveEventById(this.id, updatedLiveEvent)
      .subscribe(response => {
        this.router.navigateByUrl('liveEvents');
      });
    }

  }

  onImageChange(event : any) {
    let newFile = event.files as File[]
    let file = newFile[0]
    if(file){
      this.image = file
    }

    var reader = new FileReader
    reader.onload=() => {
      if(reader.result && this.liveEvent)
        this.liveEvent.imageUrl = reader.result.toString()

    }

    reader.readAsDataURL(file)
  }

  onDelete() : void {
    if(this.id){
      this.liveEventService.deleteLiveEventById(this.id)
      .subscribe({
        next: (response) => {
          this.router.navigateByUrl('liveEvents')
        }
      });
    }
  }
}
