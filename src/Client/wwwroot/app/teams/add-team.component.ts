﻿import {Component, OnInit, Inject} from 'angular2/core';
import {Router} from 'angular2/router';
import {FormBuilder, Validators, Control, ControlGroup} from 'angular2/common';

import {ITeamService, TeamServiceToken} from '../services/team.service';
import {IUserService, UserServiceToken} from '../services/user.service';
import {User} from '../servicemodels/user';
import {Team} from '../servicemodels/team';
import {ImgurComponent} from '../common/imgur.component';

@Component({
    templateUrl: 'app/teams/add-team.view.html',
    directives: [ImgurComponent]
})
export class AddTeamComponent implements OnInit {
    addTeamForm: ControlGroup;
    teamName: string;
    users: User[];
    selectedUsers: User[];
    isInitialize: boolean;
    imgurUrl: string;

    constructor(private _router: Router,
        private _fb: FormBuilder,
        @Inject(TeamServiceToken) private _teamService: ITeamService,
        @Inject(UserServiceToken) private _userService: IUserService) {

        this.addTeamForm = this._fb.group({
            inputTeamName: ["", Validators.required]
        });
    };

    ngOnInit() {
        this.isInitialize = true;

        this.users = new Array<User>();
        this.selectedUsers= new Array<User>();
        
        this._userService.getUsers().subscribe(item => {
            this.users = <User[]>item.json();
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

    onAddUserToTeam(user: User) {
        this.isInitialize = false;
        this.removeUserFrom(this.users, user);
        this.selectedUsers.push(user);
    }

    onRemoveUserFromTeam(user: User) {
        this.users.push(user);
        this.removeUserFrom(this.selectedUsers, user);
    }

    removeUserFrom(removeFrom: User[], userToRemove: User) {
        var index = removeFrom.indexOf(userToRemove);
        removeFrom.splice(index, 1);  
    }

    onLogoUploaded(url: string) {
        this.imgurUrl = url;
    }
}