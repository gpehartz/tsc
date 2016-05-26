import {Component} from '@angular/core';
import {RouteConfig, ROUTER_DIRECTIVES} from '@angular/router-deprecated';

import {TeamsComponent} from './teams/teams.component';
import {TournamentsComponent} from './tournaments/tournaments.component';
import {Login} from './authentication/login.component';
import {AuthenticationService} from './services/authentication.service';

@Component({
    selector: 'main-app',
    templateUrl: 'app/app.view.html',
    directives: [ROUTER_DIRECTIVES],
    providers: [AuthenticationService]
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
export class AppComponent {
    constructor(private authService: AuthenticationService) {
    }

    get authenticated() {
        return this.authService.isAuthenticated();
    }

    get username() {
        return this.authService.getUserName();
    }
}