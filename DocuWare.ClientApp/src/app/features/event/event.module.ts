import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { ButtonModule } from 'primeng/button';
import { TableModule } from 'primeng/table';

import { EventListComponent } from './components/event-list.component';
import { EventRoutingModule } from './event-routing.module';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { EventEditComponent } from './components/event-edit.component';
import { InputTextModule } from 'primeng/inputtext';
import { CalendarModule } from 'primeng/calendar';
import { EventParticipantComponent } from './components/event-participant.component';

@NgModule({
  declarations: [
    EventListComponent,
    EventEditComponent,
    EventParticipantComponent
  ],
  imports: [
    CommonModule,
    FormsModule,
    ReactiveFormsModule,
    EventRoutingModule,
    ButtonModule,
    TableModule,
    InputTextModule,
    CalendarModule
  ]
})
export class EventModule {}

