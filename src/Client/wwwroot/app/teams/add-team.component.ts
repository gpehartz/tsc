import {Component, OnInit, Inject} from 'angular2/core';
import {Router} from 'angular2/router';
import {FormBuilder, Validators, Control, ControlGroup} from 'angular2/common';


import {ITeamService, TeamServiceToken} from '../services/team.service';
import {Team} from '../models/team';

@Component({
    templateUrl: 'app/teams/add-team.view.html'
})
export class AddTeamComponent implements OnInit {
    addTeamForm: ControlGroup;
    teamName: string;

    constructor(private _router: Router,
        private _fb: FormBuilder,
        @Inject(TeamServiceToken) private _teamService: ITeamService) {

        this.addTeamForm = this._fb.group({
            inputTeamName: ["", Validators.required]
        });
    };

    ngOnInit() {

    }

    cancel() {
        this._router.navigate(['TeamsCenter']);
    }

    save() {
        this._teamService
            .addTeam(new Team(this.teamName, new Array<Team>()))
            .subscribe(item => this._router.navigate(['TeamsCenter']));
    }
}