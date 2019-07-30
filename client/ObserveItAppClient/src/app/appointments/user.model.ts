export interface IUser {
	Name: string;
}

export class User implements IUser {
	constructor(
		public Name: string
	) { }
}