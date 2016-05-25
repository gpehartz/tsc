import {Component, provide} from '@angular/core';
import {RouteConfig, RouterOutlet} from '@angular/router-deprecated';

import {TeamsListComponent} from './teams-list.component';
import {TeamService, TeamServiceToken} from '../services/team.service';
import {UserService, UserServiceToken} from '../services/user.service';
import {AddTeamComponent} from './add-team.component';

@Component({
    templateUrl: 'app/teams/teams.view.html',
    directives: [RouterOutlet],
    providers: [provide(TeamServiceToken, { useClass: TeamService }),
        provide(UserServiceToken, { useClass: UserService })]
})
@RouteConfig([
        { path: '/', name: 'TeamsCenter', component: TeamsListComponent, useAsDefault: true },
        { path: '/new', name: 'TeamCreator', component: AddTeamComponent },
])
export class TeamsComponent { }