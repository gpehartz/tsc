import {Injectable, EventEmitter, Output} from "angular2/core";
import {Http, Headers} from 'angular2/http'
import {Login} from '../servicemodels/login';

export class MockAuthenticationService {
    private oAuthCallbackUrl: string;
    private oAuthTokenUrl: string;
    private oAuthUserUrl: string;
    private oAuthUserNameField: string;
    private authenticated: boolean = false;
    private token: string;
    private expires: any = 0;
    private userInfo: any = {};
    private windowHandle: any = null;
    private intervalId: any = null;
    private expiresTimerId: any = null;
    private loopCount = 600;
    private intervalLength = 100;

   constructor(private _http: Http) { }

    @Output() change = new EventEmitter();  // @TODO: switch to RxJS Subject instead of EventEmitter
    private subscription;

    public doLogin(emailAddress:string, password:string)  {

        var headers = new Headers();
        headers.append('Content-Type', 'application/json');
        var login = new Login(emailAddress, password)

        return this._http.post('http://localhost:8081/api/teams/', JSON.stringify(login), { headers: headers });
        
    }

    public doLogout() {
        this.authenticated = false;
    }

    public isAuthenticated() {
        return this.authenticated;
    }
    public subscribe(onNext: (value: any) => void, onThrow?: (exception: any) => void, onReturn?: () => void) {
        return this.change.subscribe(onNext, onThrow, onReturn);
    }

    public getUserName() {
        return "SanyikaLOL99000";
    }

    private emitAuthStatus(success: boolean) {
        this.change.emit({ success: success, authenticated: this.authenticated, token: this.token, expires: this.expires });
    }
        

}