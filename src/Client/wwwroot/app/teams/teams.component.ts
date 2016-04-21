import {Component} from 'angular2/core';
import {RouteConfig, RouterOutlet} from 'angular2/router';

import {TeamsListComponent} from './teams-list.component';

@Component({
    templateUrl: 'app/teams/teams.view.html',
    directives: [RouterOutlet]
})
@RouteConfig([
    { path: '/', name: 'TeamsCenter', component: TeamsListComponent, useAsDefault: true }
])
export class TeamsComponent { }