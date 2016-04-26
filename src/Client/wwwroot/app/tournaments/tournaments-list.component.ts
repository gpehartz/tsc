import {Component, OnInit, Inject} from 'angular2/core';
import {Router} from 'angular2/router';

import {ITournamentService, TournamentServiceToken} from '../services/tournament.service';
import {Tournament} from '../models/tournament';

@Component({
    templateUrl: 'app/tournaments/tournaments-list.view.html'
})
export class TournamentsListComponent implements OnInit {
    tournaments: Tournament[];

    constructor(private _router: Router,
        @Inject(TournamentServiceToken) private _tournamentService: ITournamentService) { }

    addTournament() {
        this._router.navigate(['TournamentCreator']);
    }

    ngOnInit() {
        this._tournamentService.getTournaments().subscribe(item => this.tournaments = item.json());
    }
}