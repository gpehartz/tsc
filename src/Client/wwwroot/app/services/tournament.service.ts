import {Injectable, OpaqueToken} from 'angular2/core';
import {Http, Headers, Response} from 'angular2/http'
import {Observable} from 'rxjs/Observable';

import {Tournament} from '../servicemodels/tournament';

export interface ITournamentService {
    getTournaments(): Observable<Response>;

    getTournament(id: string): Observable<Response>;

    addTournament(tournament: Tournament): Observable<Response>;
}

export let TournamentServiceToken = new OpaqueToken('ITournamentService');

@Injectable()
export class TournamentService implements ITournamentService {

    constructor(private _http: Http) { }

    getTournaments() {
        return this._http.get('http://localhost:8081/api/tournaments');
    }

    getTournament(id: string) {
        return this._http.get('http://localhost:8081/api/tournaments/' + id);
    }

    addTournament(tournament: Tournament) {
        var headers = new Headers();
        headers.append('Content-Type', 'application/json');

        return this._http.post('http://localhost:8081/api/tournaments/', JSON.stringify(tournament), { headers: headers });
    }
}