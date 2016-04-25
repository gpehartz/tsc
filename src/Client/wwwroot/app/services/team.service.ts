import {Injectable, OpaqueToken} from 'angular2/core';
import {Http, Headers, Response} from 'angular2/http'
import {Observable} from 'rxjs/Observable';

export interface ITeamService {
    getTeams(): Observable<Response>;

    getTeam(id: number): Observable<Response>;
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
}