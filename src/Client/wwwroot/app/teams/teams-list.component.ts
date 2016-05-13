import {Component, OnInit, Inject} from 'angular2/core';
import {Router} from 'angular2/router';

import {ITeamService, TeamServiceToken} from '../services/team.service';
import {Team} from '../servicemodels/team';

@Component({
    templateUrl: 'app/teams/teams-list.view.html',
    styles: [`
    .logoStyle {
        border-radius: 32px;
        height: 64px;
        width: 64px;
    }
    .myTd {
        vertical-align: middle;
    }`]
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