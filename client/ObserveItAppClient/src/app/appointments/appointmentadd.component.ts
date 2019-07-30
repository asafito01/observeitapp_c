import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { SchedulerService } from '../scheduler/scheduler.service';
import { FormGroup, FormControl } from '@angular/forms';
import { IAppointment } from './appointment.model';
import { IUser } from './user.model';

@Component({
	selector: 'appointmentadd',
	templateUrl: './appointmentadd.component.html'
})

export class AppointmentAddComponent implements OnInit {
	errorMessage: string;
	foundError: boolean;
	insertForm: FormGroup;
	startHour: Date = new Date(0, 0, 0, 8, 0, 0);
	endHour: Date = new Date(0, 0, 0, 9, 0, 0);

	constructor(private route: ActivatedRoute, private router: Router, private schedulerService: SchedulerService) { }

	ngOnInit() {
		this.foundError = false;

		// Init insert form
		this.insertForm = new FormGroup({
			Name: new FormControl(''),
			StartHour: new FormControl(this.formatTime(this.startHour)),
			EndHour: new FormControl(this.formatTime(this.endHour))
		});

		// Execute a validation event each time the form changes
		this.insertForm.valueChanges.subscribe(val => {
			this.validateForm(val);
		});
	}

	// Formats the time to a formatted time string
	formatTime(time: Date) {
		return (
			((time.getHours() < 10) ? '0' + time.getHours() : time.getHours()) + ':' + ((time.getMinutes() < 10) ? '0' + time.getMinutes() : time.getMinutes())
		);
	}

	// Validation
	validateForm(formValues) {
		this.foundError = false;
		this.errorMessage = "";
		if (formValues.StartHour >= formValues.EndHour) {
			this.foundError = true;
			this.errorMessage += "- The start hour is later than the end hour<br/>";
		}

		if (!formValues.Name) {
			this.foundError = true;
			this.errorMessage += "- Please insert a user name<br/>";
		}
	}

	// Inserts a new appointment with the insert form values
	addAppointmentSubmit() {
		// Validate data
		if (this.insertForm.valid) {
			let appointment: IAppointment = {
				User: { Name: this.insertForm.value.Name },
				StartHour: this.insertForm.value.StartHour,
				EndHour: this.insertForm.value.EndHour
			}

			this.addAppointment(appointment);
		}
	}

	addAppointment(appointment: IAppointment) {
		this.schedulerService.addAppointment(appointment).subscribe(
			data => {
				this.router.navigateByUrl('/scheduler');
			},
			error => {
				this.errorMessage = error.error.ExceptionMessage;
				console.log("Add appointment error", error)
			}
		);
	}

}