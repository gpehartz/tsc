import {Component, OnInit, Inject} from '@angular/core';
import {Router} from '@angular/router-deprecated';

import {ITeamService, TeamServiceToken} from '../services/team.service';
import {Team} from '../servicemodels/team';

@Component({
    templateUrl: 'app/teams/teams-list.view.html',
    styleUrls: ['css/common.css']
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