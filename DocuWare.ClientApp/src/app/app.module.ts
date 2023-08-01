import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { RouterModule } from '@angular/router';

import { ButtonModule } from 'primeng/button';
import { MenubarModule } from 'primeng/menubar';
import { AppComponent } from './app.component';
import { EventModule } from './features/event/event.module';
import { HomeComponent } from './features/home/components/home.component';
import { HomeModule } from './features/home/home.module';
import { NavMenuComponent } from './features/nav-menu/components/nav-menu.component';
import { UserModule } from './features/user/user.module';
import { HTTP_INTERCEPTORS, HttpClientModule } from '@angular/common/http';
import { MessageService } from 'primeng/api';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { AuthInterceptor } from './shared/auth-interceptor';

@NgModule({
  declarations: [AppComponent, NavMenuComponent],
  imports: [
    BrowserAnimationsModule,
    HttpClientModule,
    ButtonModule,
    MenubarModule,

    HomeModule,
    EventModule,
    UserModule,

    RouterModule.forRoot([
      { path: '', component: HomeComponent, pathMatch: 'full' },
      { path: 'user', loadChildren: () => import('./features/user/user.module').then(m => m.UserModule) },
      { path: 'event', loadChildren: () => import('./features/event/event.module').then(m => m.EventModule) },
    ]),
  ],
  providers: [
    { provide: HTTP_INTERCEPTORS, useClass: AuthInterceptor, multi: true },
    MessageService],
  bootstrap: [AppComponent]
})
export class AppModule { }


