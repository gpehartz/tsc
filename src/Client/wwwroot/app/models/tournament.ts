import {Team} from './team';

export class Tournament {
    constructor(public id: number, public name: string, public teams: Team[]) { }
}