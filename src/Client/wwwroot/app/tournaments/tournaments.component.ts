import {Component} from 'angular2/core';
import {RouteConfig, RouterOutlet} from 'angular2/router';

import {TournamentsListComponent} from './tournaments-list.component';

@Component({
    templateUrl: 'app/tournaments/tournaments.view.html',
    directives: [RouterOutlet]
})
@RouteConfig([
    { path: '/', name: 'TournamentsCenter', component: TournamentsListComponent, useAsDefault: true }
])
export class TournamentsComponent { }