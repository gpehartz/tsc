import {Component, OnInit, Inject} from '@angular/core';
import {Router} from '@angular/router-deprecated';
import {Observable} from 'rxjs/Rx';
import 'rxjs/add/operator/map';
import 'rxjs/add/operator/do';
import {PaginatePipe, PaginationService, PaginationControlsCmp, IPaginationInstance} from 'ng2-pagination';

import {ITeamService, TeamServiceToken} from '../services/team.service';
import {Team} from '../servicemodels/team';

export interface PagedResponse<T> {
    total: number;
    data: T[];
}

@Component({
    templateUrl: 'app/teams/teams-list.view.html',
    styleUrls: ['css/common.css'],
    providers: [PaginationService],
    directives: [PaginationControlsCmp],
    pipes: [PaginatePipe],
})
export class TeamsListComponent implements OnInit  {
    teams: Observable<Team>;
private _page: number = 1;
    private _total: number;
    
    constructor(private _router: Router,
        @Inject(TeamServiceToken) private _teamService: ITeamService) { }

    addTeam() {
        this._router.navigate(['TeamCreator']);
    }

    ngOnInit() {
        this.getPage(1);
    }    
    
    getPage(page: number) {
        this.teams = this._teamService.getTeamPage(page)
            .do((res: any) => {
                this._total = res.json().total;
                this._page = page;
            })
            .map((res: any) => res.json().data);
    }
}