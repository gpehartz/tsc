﻿import {Component, provide} from '@angular/core';
import {RouteConfig, RouterOutlet} from '@angular/router-deprecated';

import {TournamentsListComponent} from './tournaments-list.component';
import {TournamentDetailComponent} from './tournament-detail.component';
import {AddTournamentComponent} from './add-tournament.component';
import {AddFixtureResultComponent} from './add-fixtureresult.component';
import {TeamService, TeamServiceToken} from '../services/team.service';
import {TournamentService, TournamentServiceToken} from '../services/tournament.service';

@Component({
    templateUrl: 'app/tournaments/tournaments.view.html',
    directives: [RouterOutlet],
    providers: [provide(TeamServiceToken, { useClass: TeamService }), provide(TournamentServiceToken, { useClass: TournamentService })]
})
@RouteConfig([
        { path: '/', name: 'TournamentsCenter', component: TournamentsListComponent, useAsDefault: true },
        { path: '/:id', name: 'TournamentDetail', component: TournamentDetailComponent },
        { path: '/:id/fixtures', name: 'Fixtures', component: AddFixtureResultComponent },
        { path: '/new', name: 'TournamentCreator', component: AddTournamentComponent},
])
export class TournamentsComponent { }