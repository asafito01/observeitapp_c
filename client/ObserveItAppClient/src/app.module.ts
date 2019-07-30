import { BrowserModule } from '@angular/platform-browser';
import { HttpClientModule } from '@angular/common/http';
import { NgModule } from '@angular/core';
import { RouterModule } from '@angular/router';

import { AppComponent } from './app/app.component';
import { SchedulerComponent } from './app/scheduler/scheduler.component';
import { AppointmentAddComponent } from './app/appointments/appointmentadd.component';

import { SchedulerService } from './app/scheduler/scheduler.service';

import { FormsModule, ReactiveFormsModule } from '@angular/forms';

import { appRoutes } from './routes';

@NgModule({
  declarations: [
    AppComponent,
    SchedulerComponent,
    AppointmentAddComponent
  ],
  imports: [
    BrowserModule,
    HttpClientModule,
    FormsModule,
    ReactiveFormsModule,
    RouterModule.forRoot(appRoutes)
  ],
  providers: [
    SchedulerService
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }
