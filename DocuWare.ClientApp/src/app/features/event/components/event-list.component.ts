import { Component, OnInit } from '@angular/core';
import { DialogService, DynamicDialogRef } from 'primeng/dynamicdialog';
import { EventService } from '../../../core/api/event.service';
import { Event } from '../../../core/models/event';
import { UserStore } from '../../../core/state/user.store';
import { Router } from '@angular/router';
import { EventParticipantComponent } from './event-participant.component';
import { MessageService } from 'primeng/api';
import { Result } from '../../../core/models/shared';

@Component({
  templateUrl: './event-list.component.html',
  providers: [DialogService, MessageService]
})
export class EventListComponent implements OnInit {

  events: Event[] = [];
  participantDialog!: DynamicDialogRef;
  participateIds: number[] = [];

  constructor(
    private dialogService: DialogService,
    private eventService: EventService,
    public userStore: UserStore,
    public router: Router
  ) {
      
  }

  ngOnInit() {
    this.refreshList();
    this.userStore.participateIds$.subscribe(q => this.participateIds = q);
  }

  refreshList() {
    this.eventService.events()
      .subscribe((response: Event[]) => this.events = response);
  }

  addParticipant(eventId:number) {
    this.participantDialog = this.dialogService.open(EventParticipantComponent, {
      data: { eventId: eventId },
      header: 'Event Participant',
      width: '500px'
    });

    this.participantDialog.onClose
      .subscribe((result: Result) => {
        if (result && result.success) {
          this.refreshList();
          this.userStore.setParticipateIds([eventId]);
        }
      });
  }
}
