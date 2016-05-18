import {Component, OnInit, Inject} from 'angular2/core';
import {Router} from 'angular2/router';
import {FormBuilder, Validators, Control, ControlGroup} from 'angular2/common';


import {ITeamService, TeamServiceToken} from '../services/team.service';
import {ITournamentService, TournamentServiceToken} from '../services/tournament.service';
import {TeamWithSelection} from '../models/teamwithselection';
import {Team} from '../servicemodels/team';
import {Tournament} from '../servicemodels/tournament';
import {ImgurComponent} from '../common/imgur.component';


@Component({
    templateUrl: 'app/tournaments/add-tournament.view.html',
    directives: [ImgurComponent]
})
export class AddTournamentComponent implements OnInit {
    addTournamentForm: ControlGroup;
    tournamentName: string;
    teamsWithSelection: TeamWithSelection[];
    imgurUrl: string;

    constructor(private _router: Router,
        private _fb: FormBuilder,
        @Inject(TeamServiceToken) private _teamService: ITeamService,
        @Inject(TournamentServiceToken) private _tournamentService: ITournamentService) {

        this.addTournamentForm = this._fb.group({
            inputTournamentName: ["", Validators.required],
            teamGroup: this._fb.group({}, { validator: AddTournamentComponent.validateTeamSelection })
        });
    };

    ngOnInit() {
        this._teamService.getTeams().subscribe(item => {
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

        var tournament = new Tournament(this.tournamentName, selectedTeams)
        tournament.logoUrl = this.imgurUrl;

        this._tournamentService.addTournament(tournament)
            .subscribe(item => this._router.navigate(['TournamentsCenter']));
    }

    onLogoUploaded(url: string) {
        this.imgurUrl = url;
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