import { Component, OnInit, OnDestroy } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { SchedulerService } from './scheduler.service';
import { FormGroup, FormControl } from '@angular/forms';
import { ISchedulerRow } from './scheduler.model';
import { timer, Subscription } from 'rxjs';
import { take } from 'rxjs/operators';

@Component({
	selector: 'scheduler',
	templateUrl: './scheduler.component.html'
})

export class SchedulerComponent implements OnInit {
	schedulerRows: ISchedulerRow[];
	errorMessage: string;
	timerSubscription: Subscription;

	constructor(private route: ActivatedRoute, private schedulerService: SchedulerService) { }

	ngOnInit() {
		// Subscribing timer
		this.timerSubscription = timer(0, 1000).subscribe(() => {
			this.getSchedulerRows();
		});
	}

	ngOnDestroy() {
		// Unsubscribing timer
		this.timerSubscription.unsubscribe();
	}

	getSchedulerRows() {
		// Subscribe to the scheduler service
		this.schedulerService.getSchedulerRows().subscribe(
			data => {
				this.schedulerRows = data
			},
			error => {
				this.errorMessage = error.error.ExceptionMessage;
			}
		)
	}
}