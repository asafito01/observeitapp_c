export interface ISchedulerRow {
	StartHour: string;
	EndHour: string;     
	Participants: string[];
	ParticipantsCount: number;
}

export class SchedulerRow implements ISchedulerRow {
	constructor(
			public StartHour: string,
			public EndHour: string,
			public Participants: string[],
			public ParticipantsCount: number
	){}
}