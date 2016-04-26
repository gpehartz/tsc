import {Team} from './team';

export class Tournament {
    id: string;
    creationDate: Date;

    constructor(public name: string, public participants: Team[]) { }
}