import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';

import { EventListComponent } from './components/event-list.component';
import { AuthGuard } from '../../shared/auth-guard';
import { EventEditComponent } from './components/event-edit.component';

const routes: Routes = [
  { path: 'list', component: EventListComponent },
  { path: 'edit/:id', canActivate: [AuthGuard], component: EventEditComponent },
  { path: 'edit', canActivate: [AuthGuard], component: EventEditComponent },
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class EventRoutingModule { }
