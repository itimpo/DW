import { Component, OnInit } from '@angular/core';
import { Event, EventParticipant } from '../../../core/models/event';
import { EventService } from '../../../core/api/event.service';
import { ActivatedRoute, Router } from '@angular/router';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { map } from 'rxjs';
import { NotificationService } from '../../../core/services/notification.service';

@Component({
  selector: 'event-edit-component',
  templateUrl: './event-edit.component.html'
})
export class EventEditComponent implements OnInit {

  eventId?: number;

  form: FormGroup = this.fb.group(new Event());;

  participants: EventParticipant[] = [];

  constructor(
    private fb: FormBuilder,
    private eventService: EventService,
    private route: ActivatedRoute,
    private notify: NotificationService,
    public router: Router,
  ) {
  }

  ngOnInit() {

     this.route.params
      .pipe(map(p => p['id'] as number))
      .subscribe(id => {
        this.eventId = id;

        if (!this.eventId) {
          this.form = this.fb.group(new Event());
          this.addValidators();
        } else {
          this.eventService
            .eventGet(this.eventId)
            .subscribe(r => {
              r.startTime = new Date(r.startTime);
              if (r.endTime) {
                r.endTime = new Date(r.endTime);
              }
              this.form = this.fb.group(r);
              this.addValidators();
            });

          this.eventService.eventParticipants(this.eventId)
            .subscribe(r => this.participants = r);
        }
      });
  }

  addValidators() {
    this.form.controls["name"].addValidators([Validators.required, Validators.maxLength(50)]);
    this.form.controls["description"].addValidators([Validators.required, Validators.maxLength(250)]);
    this.form.controls["location"].addValidators([Validators.required, Validators.maxLength(100)]);
    this.form.controls["startTime"].addValidators([Validators.required]);
  }

  save() {
    if (this.eventId) {
      this.eventService.eventUpdate(this.eventId, this.form.value)
        .subscribe(r => {
          if (r.success) {
            this.notify.success('Saved')
            this.router.navigate(['/event/list'])
          } else {
            this.notify.error('Failed', r.error);
          }
        });
    } else {
      this.eventService.eventCreate(this.form.value)
        .subscribe(r => {
          if (r.success) {
            this.notify.success('Saved')
            this.router.navigate(['/event/list'])
          } else {
            this.notify.error('Failed', r.error);
          }
        })
    }

  }
}
