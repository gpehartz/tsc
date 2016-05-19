import {Team} from '../servicemodels/team';

export class TeamWithSelection {
    constructor(public team: Team, public isSelected: boolean) { }
}