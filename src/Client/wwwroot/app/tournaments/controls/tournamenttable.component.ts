import {Component, Input, Output, EventEmitter} from 'angular2/core';

import {Tournament} from '../../servicemodels/tournament';

@Component({
    selector: 'tournament-table',
    styleUrls: ['css/common.css'],
    templateUrl: 'app/tournaments/controls/tournamenttable.view.html'
})
export class TournamentTableComponent {
    @Input() tournament: Tournament;
}