import {Component} from 'angular2/core';
import {RouteConfig, ROUTER_DIRECTIVES} from 'angular2/router';

import {TeamsComponent} from './teams/teams.component';
import {TournamentsComponent} from './tournaments/tournaments.component';
import {Login} from './authentication/login.component';

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
    },
    {
        path: '/authentication/',
        name: 'Login',
        component: Login
    }
])
export class AppComponent {}