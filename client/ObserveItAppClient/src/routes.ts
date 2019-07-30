import { Routes } from '@angular/router';

import { SchedulerComponent } from './app/scheduler/scheduler.component';
import { AppointmentAddComponent } from './app/appointments/appointmentadd.component';

export const appRoutes: Routes = [
		{ path: 'scheduler', component: SchedulerComponent },
		{ path: 'scheduler/appointmentadd', component: AppointmentAddComponent},
        { path: '', redirectTo: 'scheduler', pathMatch: 'full' }
];  