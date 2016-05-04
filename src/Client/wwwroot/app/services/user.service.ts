import {Injectable, OpaqueToken} from 'angular2/core';
import {Http, Headers, Response} from 'angular2/http';
import {Observable} from 'rxjs/Observable';

import {User} from '../models/user';

export interface IUserService {
    getUsers(): Observable<Response>;

    getUser(id: number): Observable<Response>;
}

export let UserServiceToken = new OpaqueToken('IUserService');

@Injectable()
export class UserService implements IUserService {

    constructor(private _http: Http) { }

    getUsers() {
        return this._http.get('http://localhost:8081/api/users');
    }

    getUser(id: number) {
        return this._http.get('http://localhost:8081/api/users/' + id);
    }
}