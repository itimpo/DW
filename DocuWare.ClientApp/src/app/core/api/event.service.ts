
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { Event, EventParticipant } from '../models/event';
import { Result } from '../models/shared';
import { BaseService } from './_base.service';

@Injectable({
  providedIn: 'root'
})
export class EventService extends BaseService{

  url = {
    event: '/event',
  }

  //#region Events

  events(): Observable<Event[]> {
    return super.get<Event[]>(`${this.url.event}`);
  }

  eventGet(id: number): Observable<Event> {
    return super.get(`${this.url.event}/${id}`);
  }

  eventCreate(post: Event): Observable<Result> {
    return super.post(this.url.event, post)
  }

  eventUpdate(eventId: number, post: Event): Observable<Result> {
    return super.put(`${this.url.event}/${eventId}`, post)
  }

  //#endregion

  //#region Events

  eventParticipants(eventId: number): Observable<EventParticipant[]> {
    return super.get<EventParticipant[]>(`${this.url.event}/${eventId}/participants`);
  }

  eventParticipantCreate(eventId:number, post: Event): Observable<Result> {
    return super.post(`${this.url.event}/${eventId}/participant`, post)
  }

  //#endregion
}
