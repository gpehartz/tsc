import {Component, OnInit, Inject} from 'angular2/core';
import {Router} from 'angular2/router';

import {ITournamentService, TournamentServiceToken} from '../services/tournament.service';
import {Tournament} from '../servicemodels/tournament';

@Component({
    templateUrl: 'app/tournaments/tournaments-list.view.html',
    styles: [`
    .logoStyle {
        border-radius: 32px;
        height: 64px;
        width: 64px;
    }
    .myTd {
        vertical-align: middle;
    }`]
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

    onSelect(tournament: Tournament) {
        this._router.navigate(['TournamentDetail', { id: tournament.id }]);
    }
}