import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { AddParticipantRequest } from '../models/add-participant-request.model';
import { environment } from '../../../../environments/environment';
import { Participant } from '../models/participant.mode';

@Injectable({
  providedIn: 'root'
})
export class ParticipantService {

  constructor(private http: HttpClient) {

  }

  addParticipant(model: AddParticipantRequest): Observable<void> {
    return this.http.post<void>(`${environment.apiBaseUrl}/api/participant`, model);
  }

  getAllParticipants() : Observable<Participant[]> {
    return this.http.get<Participant[]>(`${environment.apiBaseUrl}/api/participant`);
  }
}
