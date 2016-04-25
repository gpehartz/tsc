import {Component, OnInit, Inject} from 'angular2/core';
import {Router} from 'angular2/router';
import {FormBuilder, Validators, Control, ControlGroup} from 'angular2/common';


import {ITeamService, TeamServiceToken} from '../services/team.service';
import {TeamWithSelection} from '../models/teamWithSelection';
import {Team} from '../models/team';


@Component({
    templateUrl: 'app/tournaments/add-tournament.view.html'
})
export class AddTournamentComponent implements OnInit {
    addTournamentForm: ControlGroup;
    name: string;
    teamsWithSelection: TeamWithSelection[];

    constructor(private _router: Router,
        private _fb: FormBuilder,
        @Inject(TeamServiceToken) private _service: ITeamService) {

        this.addTournamentForm = this._fb.group({
            inputTournamentName: ["", Validators.required],
            teamGroup: this._fb.group({}, { validator: AddTournamentComponent.validateTeamSelection })
        });
    };

    ngOnInit() {
        this._service.getTeams().subscribe(item => {
            var teams = <Team[]>item.json();
            this.teamsWithSelection = new Array();

            var teamControlGroup = <ControlGroup>this.addTournamentForm.find('teamGroup');

            teams.forEach(item => {
                teamControlGroup.addControl(item.id.toString(), new Control(false));
                this.teamsWithSelection.push(new TeamWithSelection(item, false));
            });
        });
    }

    cancel() {
        this._router.navigate(['TournamentsCenter']);
    }

    save() {
        var selectedTeams = this.teamsWithSelection.filter(item => item.isSelected).map(item => item.team);


    }

    static validateTeamSelection(group: ControlGroup) {
        for (var controlKey in group.controls) {
            var control = group.controls[controlKey];

            if (control.value) {
                return null;
            }
        }

        return { noTeamSelected: true };
    }
}