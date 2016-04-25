import {Component, provide} from 'angular2/core';
import {RouteConfig, RouterOutlet} from 'angular2/router';

import {TournamentsListComponent} from './tournaments-list.component';
import {AddTournamentComponent} from './add-tournament.component';
import {TeamService, TeamServiceToken} from '../services/team.service';

@Component({
    templateUrl: 'app/tournaments/tournaments.view.html',
    directives: [RouterOutlet],
    providers: [provide(TeamServiceToken, { useClass: TeamService })]
})
@RouteConfig([
        { path: '/', name: 'TournamentsCenter', component: TournamentsListComponent, useAsDefault: true },
        { path: '/new', name: 'TournamentCreator', component: AddTournamentComponent},
])
export class TournamentsComponent { }