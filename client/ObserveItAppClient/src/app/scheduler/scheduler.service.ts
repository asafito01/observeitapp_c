import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Observable, throwError } from 'rxjs';
import { retry, catchError } from 'rxjs/operators';
import { ISchedulerRow } from './scheduler.model';
import { IAppointment, Appointment } from '../appointments/appointment.model';

@Injectable({
        providedIn: 'root'
})
export class SchedulerService {
        private apiUrl = 'http://localhost:4444/api/v1';

        constructor(private http: HttpClient) { }

        // Http Options
        httpOptions = {
                headers: new HttpHeaders({
                        'Content-Type': 'application/json'
                })
        }

        // Gets the scheduler appointment rows
        getSchedulerRows(): Observable<ISchedulerRow[]> {
                return this.http.get<ISchedulerRow[]>(this.apiUrl + "/scheduler")
                        .pipe(
                                retry(1)
                        );
        }

        // Creates a new appointment using the Web API
        addAppointment(appointment: IAppointment): Observable<IAppointment> {
                return this.http.post<IAppointment>(this.apiUrl + "/scheduler", appointment)
                        .pipe(
                                retry(1)
                        );
        }
}