import {Component, Input, Output} from 'angular2/core';

import {Tournament} from '../../servicemodels/tournament';

@Component({
    selector: 'tournament-rounds',
    templateUrl: 'app/tournaments/controls/tournamentrounds.view.html'
})
export class TournamentRoundsComponent {
    @Input() tournament: Tournament;
}