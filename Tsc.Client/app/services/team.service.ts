import {Injectable, OpaqueToken} from '@angular/core';
import {Http, Headers, Response} from '@angular/http';
import {Observable} from 'rxjs/Observable';

import {Team} from '../servicemodels/team';

export interface ITeamService {
    getTeams(): Observable<Response>;

    getTeam(id: number): Observable<Response>;

    addTeam(team: Team): Observable<Response>;
}

export let TeamServiceToken = new OpaqueToken('ITeamService');

@Injectable()
export class TeamService implements ITeamService {

    constructor(private _http: Http) { }

    getTeams() {
        return this._http.get('http://localhost:8081/api/teams');
    }

    getTeam(id: number) {
        return this._http.get('http://localhost:8081/api/teams/' + id);
    }

    addTeam(team: Team) {
        var headers = new Headers();
        headers.append('Content-Type', 'application/json');

        return this._http.post('http://localhost:8081/api/teams/', JSON.stringify(team), { headers: headers });
    }
}