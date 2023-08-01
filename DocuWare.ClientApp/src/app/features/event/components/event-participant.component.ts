import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { DynamicDialogConfig, DynamicDialogRef } from 'primeng/dynamicdialog';
import { EventService } from '../../../core/api/event.service';
import { EventParticipant } from '../../../core/models/event';
import { NotificationService } from '../../../core/services/notification.service';

@Component({
  templateUrl: './event-participant.component.html'
})
export class EventParticipantComponent implements OnInit {

  eventId!: number;

  form: FormGroup = this.fb.group(new EventParticipant());

  constructor(
    private fb: FormBuilder,
    private eventService: EventService,
    private ref: DynamicDialogRef,
    private config: DynamicDialogConfig,
    private notify: NotificationService
  ) {
    this.eventId = this.config.data.eventId;
  }

  ngOnInit() {
    this.form.controls["name"].addValidators([Validators.required, Validators.maxLength(100)]);
    this.form.controls["phoneNumber"].addValidators([Validators.required, Validators.maxLength(100)]);
    this.form.controls["emailAddress"].addValidators([Validators.required, Validators.email, Validators.maxLength(100)]);
  }

  cancel() {
    this.ref.close();
  }

  save() {
    this.eventService.eventParticipantCreate(this.eventId, this.form.value)
        .subscribe(r => {
          if (r.success) {
            this.notify.success('Saved')
            this.ref.close(r);
          } else {
            this.notify.error('Failed', r.error);
          }
        })
  }
}
