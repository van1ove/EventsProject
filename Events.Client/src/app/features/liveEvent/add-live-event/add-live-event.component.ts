import { Component,  OnDestroy } from "@angular/core";
import { FormsModule } from "@angular/forms";
import { RouterModule } from "@angular/router";
import { AddLiveEventRequest } from "../models/add-live-event-request.model";
import { LiveEventService } from "../services/live-event.service";
import { Subscription } from "rxjs";
import { CommonModule } from "@angular/common";
import { CATEGORIES_CONST } from "../consts/categories";
import { Router } from "@angular/router"

@Component({
  selector: 'app-add-live-event',
  standalone: true,
  imports: [
    RouterModule,
    FormsModule,
    CommonModule
  ],
  templateUrl: './add-live-event.component.html',
  styleUrl: './add-live-event.component.css'
})

export class AddLiveEventComponent implements OnDestroy{

  isModelValid : boolean = true

  readonly categories : Array<string>

  model: AddLiveEventRequest
  image?: File
  data: FormData = new FormData()

  private addLiveEventSubscribtion?: Subscription

  constructor(private liveEventService : LiveEventService, private router : Router) {
    this.categories = CATEGORIES_CONST
    this.model = {
      title: 'New event',
      description: 'Event description',
      date: new Date().toJSON().slice(0,10),
      time: new Date().toJSON().slice(11,16),
      place: 'Event place',
      category: this.categories[0],
      maxParticipants: 10,
      imageUrl: 'assets/images/default.jpg'
    };
  }


  onFormSubmit() : void{
    if(this.model.date == '' || this.model.title.length < 3 || this.model.time == ''
        || this.model.place.length < 3){
      this.isModelValid = false
    }
    else {
      this.isModelValid = true
    }

    if(!this.isModelValid)
      return

    if(this.model.time.length == 5)
      this.model.time += ":00"

    this.addLiveEventSubscribtion = this.liveEventService.addLiveEvent(this.model).subscribe({
      next: (response) => {
        this.router.navigateByUrl('liveEvents')
      }
    })
  }

  onImageChange(event : any) {
    let newFile = event.files as File[]
    let file = newFile[0]
    if(file){
      this.image = file
    }

    var reader = new FileReader
    reader.onload=() => {
      if(reader.result)
        this.model.imageUrl = reader.result.toString()

    }

    reader.readAsDataURL(file)
  }

  ngOnDestroy(): void {
    this.addLiveEventSubscribtion?.unsubscribe();
  }
}

