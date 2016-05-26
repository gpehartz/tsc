import {Component, OnInit, Inject} from '@angular/core';
import {Router} from '@angular/router-deprecated';
import {FormBuilder, Validators, Control, ControlGroup} from '@angular/common';

import {ITeamService, TeamServiceToken} from '../services/team.service';
import {IPlayerService, PlayerServiceToken} from '../services/player.service';
import {Player} from '../servicemodels/player';
import {Team} from '../servicemodels/team';
import {ImgurComponent} from '../common/imgur.component';

@Component({
    templateUrl: 'app/teams/add-team.view.html',
    styleUrls: ['css/common.css'],
    directives: [ImgurComponent]
})
export class AddTeamComponent implements OnInit {
    addTeamForm: ControlGroup;
    teamName: string; 
    players: Player[];
    selectedUsers: Player[];
    isInitialize: boolean;
    imgurUrl: string;

    constructor(private _router: Router,
        private _fb: FormBuilder,
        @Inject(TeamServiceToken) private _teamService: ITeamService,
        @Inject(PlayerServiceToken) private _userService: IPlayerService) {

        this.addTeamForm = this._fb.group({
            inputTeamName: ["", Validators.required]
        });
    };

    ngOnInit() {
        this.isInitialize = true;

        this.players = new Array<Player>();
        this.selectedUsers= new Array<Player>();
        
        this._userService.getUsers().subscribe(item => {
            this.players = <Player[]>item.json();
        });
    }

    cancel() {
        this._router.navigate(['TeamsCenter']);
    }

    save() {
        this._teamService
            .addTeam(new Team(this.teamName, new Array<Team>(), this.imgurUrl))
            .subscribe(item => this._router.navigate(['TeamsCenter']));
    }

    onAddPlayerToTeam(player: Player) {
        this.isInitialize = false;
        this.removePlayerFrom(this.players, player);
        this.selectedUsers.push(player);
    }

    onRemovePlayerFromTeam(player: Player) {
        this.players.push(player);
        this.removePlayerFrom(this.selectedUsers, player);
    }

    removePlayerFrom(removeFrom: Player[], playerToRemove:Player) {
        var index = removeFrom.indexOf(playerToRemove);
        removeFrom.splice(index, 1);  
    }

    onLogoUploaded(url: string) {
        this.imgurUrl = url;
    }
}