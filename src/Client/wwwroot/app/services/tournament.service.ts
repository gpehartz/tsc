import {Injectable, OpaqueToken} from 'angular2/core';
import {Http, Headers, Response} from 'angular2/http'
import {Observable} from 'rxjs/Observable';

import {Tournament} from '../servicemodels/tournament';
import {Fixture} from '../servicemodels/fixture';

export interface ITournamentService {
    getTournaments(): Observable<Response>;

    getTournament(id: string): Observable<Response>;

    addTournament(tournament: Tournament, logo: File): Promise<Response>;

    setFixtureResult(tournamentId: string, fixture: Fixture): Observable<Response>;
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

    addTournament(tournament: Tournament, logo: File) {
        return new Promise((resolve, reject) => {
            var formData: any = new FormData();
            var xhr = new XMLHttpRequest();

            formData.append('tournament', JSON.stringify(tournament));
            formData.append('logo', logo, logo.name);

            xhr.onreadystatechange = function () {
                if (xhr.readyState == 4) {
                    if (xhr.status == 200) {
                        resolve(JSON.parse(xhr.response));
                    } else {
                        reject(xhr.response);
                    }
                }
            }
            xhr.open('POST', 'http://localhost:8081/api/tournaments/', true);
            xhr.send(formData);
        });
    }

    setFixtureResult(tournamentId: string, fixture: Fixture) {
        var headers = new Headers();
        headers.append('Content-Type', 'application/json');

        return this._http.put('http://localhost:8081/api/tournaments/' + tournamentId + '/fixtures/' + fixture.id, JSON.stringify(fixture), { headers: headers });
    }
}