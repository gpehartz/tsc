import {Team} from './team';
import {TournamentResultItem} from './tournamentResultItem';
import {Round} from './round';


export class Tournament {
    id: string;
    creationDate: Date;
    table: TournamentResultItem[];
    rounds: Round[];
    logoUrl: string;

    constructor(public name: string, public participants: Team[]) { }
}