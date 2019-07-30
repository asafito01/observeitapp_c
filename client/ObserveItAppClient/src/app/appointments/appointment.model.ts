
import { IUser } from './user.model';

export interface IAppointment {
	User: IUser;
	StartHour: string;
	EndHour: string;
}

export class Appointment implements IAppointment {
	constructor(
		public User: IUser,
		public StartHour: string,
		public EndHour: string
	) { }
}