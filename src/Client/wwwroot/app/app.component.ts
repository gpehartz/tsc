import {Component} from 'angular2/core';
import {RouteConfig, ROUTER_DIRECTIVES} from 'angular2/router';

import {TeamsComponent} from './teams/teams.component';
import {TournamentsComponent} from './tournaments/tournaments.component';

@Component({
    selector: 'main-app',
    templateUrl: 'app/app.view.html',
    directives: [ROUTER_DIRECTIVES]
})
@RouteConfig([
    {
        path: '/teams/...',
        name: 'Teams',
        component: TeamsComponent
    },
    {
        path: '/tournaments/...',
        name: 'Tournaments',
        component: TournamentsComponent
    }
])
export class AppComponent {}