import { Injectable } from '@angular/core';
import { AddLiveEventRequest } from '../models/add-live-event-request.model';
import { catchError, Observable, tap, throwError } from 'rxjs';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { LiveEvent } from '../models/live-event.model';
import { environment } from '../../../../environments/environment';
import { EditLiveEventRequest } from '../models/edit-live-event.request.model';

@Injectable({
  providedIn: 'root'
})
export class LiveEventService {

  constructor(private http: HttpClient) { }

  postFile(file: File): Observable<Object> {
    const formData = new FormData()
    formData.append('file', file)
    const headers = new HttpHeaders().append('Content-Disposition', 'multipart/form-data')
    return this.http.post(`${environment.apiBaseUrl}/api/liveEvents`, formData, { headers })
  }

  addLiveEvent(model : AddLiveEventRequest) : Observable<void> {
    let data = new FormData
    data.append('title', model.title)
    data.append('description', model.description)
    data.append('date', model.date)
    data.append('time', model.time)
    data.append('place', model.place)
    data.append('category', model.category)
    data.append('maxParticipants', model.maxParticipants.toString())
    data.append('imageUrl', model.imageUrl)
    return this.http.post<void>(`${environment.apiBaseUrl}/api/liveEvents`, data)
  }

  getAllLiveEvents() : Observable<LiveEvent[]>{
    return this.http.get<LiveEvent[]>(`${environment.apiBaseUrl}/api/liveEvents`);
  }

  getLiveEventById(id : string) : Observable<LiveEvent>{
    return this.http.get<LiveEvent>(`${environment.apiBaseUrl}/api/liveEvents/${id}`);
  }

  updateLiveEventById(id: string, updatedLiveEvent: EditLiveEventRequest) : Observable<LiveEvent>{
    const headers = new HttpHeaders({
      'Content-Type': 'application/json'
    });
    return this.http.put<LiveEvent>(`${environment.apiBaseUrl}/api/liveEvents/${id}`, updatedLiveEvent, { headers })
      .pipe(tap(response => {
        if (response) { }
      }), catchError(error => {
        return throwError(error);
      }));
  }

  deleteLiveEventById(id: string): Observable<LiveEvent>{
    return this.http.delete<LiveEvent>(`${environment.apiBaseUrl}/api/liveEvents/${id}`)
  }
}
