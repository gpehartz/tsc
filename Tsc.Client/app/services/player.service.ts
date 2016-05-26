import {Injectable, OpaqueToken} from '@angular/core';
import {Http, Headers, Response} from '@angular/http';
import {Observable} from 'rxjs/Observable';

import {Player} from '../servicemodels/player';

export interface IPlayerService {
    getUsers(): Observable<Response>;

    getUser(id: number): Observable<Response>;
}

export let PlayerServiceToken = new OpaqueToken('IPlayerService');

@Injectable()
export class PlayerService implements IPlayerService {

    constructor(private _http: Http) { }

    getUsers() {
        return this._http.get('http://localhost:8081/api/players');
    }

    getUser(id: number) {
        return this._http.get('http://localhost:8081/api/players/' + id);
    }
}