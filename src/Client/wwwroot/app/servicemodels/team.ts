import {User} from './user';

export class Team {
    id: string;
    creationDate: Date;

    constructor(public name: string, public users: User[]) { }
}