import {Router, RouteParams} from 'angular2/router';
import {Component, OnInit, Inject} from 'angular2/core';

import {Tournament} from '../servicemodels/tournament';
import {ITournamentService, TournamentServiceToken} from '../services/tournament.service';

@Component({
    templateUrl: 'app/tournaments/tournament-detail.view.html'
})
export class TournamentDetailComponent implements OnInit {
    tournament: Tournament;

    constructor(private _router: Router,
        private _routeParams: RouteParams,
        @Inject(TournamentServiceToken) private _tournamentService: ITournamentService) { }

    ngOnInit() {
        let id = this._routeParams.get('id');

        this._tournamentService.getTournament(id).subscribe(item => this.tournament = item.json());
    }

    back() {
        this._router.navigate(['TournamentsCenter']);
    }
}