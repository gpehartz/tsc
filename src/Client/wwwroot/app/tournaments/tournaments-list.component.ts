import {Component} from 'angular2/core';
import {Router} from 'angular2/router';

@Component({
    templateUrl: 'app/tournaments/tournaments-list.view.html'
})
export class TournamentsListComponent {

    constructor(private _router: Router) {        
    }

    addTournament() {
        this._router.navigate(['TournamentCreator']);
    }
}