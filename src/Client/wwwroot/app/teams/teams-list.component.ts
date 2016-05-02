import {Component, OnInit, Inject} from 'angular2/core';
import {Router} from 'angular2/router';

import {ITeamService, TeamServiceToken} from '../services/team.service';
import {Team} from '../models/team';

@Component({
    templateUrl: 'app/teams/teams-list.view.html'
})
export class TeamsListComponent implements OnInit  {
    teams: Team[];

    constructor(private _router: Router,
        @Inject(TeamServiceToken) private _teamService: ITeamService) { }

    addTeam() {
        this._router.navigate(['TeamCreator']);
    }

    ngOnInit() {
        this._teamService.getTeams().subscribe(item => this.teams = item.json());
    }
}