import {Component, OnInit, Inject} from '@angular/core';
import {Router} from '@angular/router-deprecated';
import {FormBuilder, Validators, Control, ControlGroup} from '@angular/common';

import {ITeamService, TeamServiceToken} from '../services/team.service';
import {IUserService, UserServiceToken} from '../services/user.service';
import {User} from '../servicemodels/user';
import {Team} from '../servicemodels/team';

@Component({
    templateUrl: 'app/teams/add-team.view.html',
    styleUrls: ['css/common.css']
})
export class AddTeamComponent implements OnInit {
    addTeamForm: ControlGroup;
    teamName: string; 
    users: User[];
    selectedUsers: User[];
    isInitialize: boolean;
    logoUrl: string;
    logoReposense: string;

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
            .addTeam(new Team(this.teamName, new Array<Team>(), this.logoUrl))
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

    upload($event) {
        console.log($event);
    }
    
    fileChangeEvent(fileInput: any) {

        var fileToUpload = fileInput.target.files[0];

        var formData = new FormData();
        formData.append("image", fileToUpload); 

        var xhr = new XMLHttpRequest(); 
        xhr.open("POST", "https://api.imgur.com/3/image");

        xhr.onload = () => {
            if (xhr.readyState === 4) {
                if (xhr.status === 200) {
                    this.logoUrl = JSON.parse(xhr.responseText).data.link;
                } else {
                    this.logoReposense = xhr.responseText;
                }
            }
        }

        xhr.setRequestHeader('Authorization', 'Client-ID cfc1c4d88be16f5'); 
        
        xhr.send(formData);
    }
}