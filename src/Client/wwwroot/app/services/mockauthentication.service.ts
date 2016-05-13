import {Injectable, EventEmitter, Output} from "angular2/core";
import {Http} from 'angular2/http'
import 'rxjs/add/operator/map'
import {Login} from '../servicemodels/login';

@Injectable()
export class AuthenticationService {
    private _url = "http://localhost:8081/api/account";
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

   constructor(private _http: Http) {
       
   }

    public doLogin(emailAddress:string, password:string)  {

        var login = new Login(emailAddress, password);

        return this._http
            .post(this._url + '/login', JSON.stringify(login))
            .map(res => res.json());
    }

    public doLogout() {
        this.authenticated = false;
    }

    public isAuthenticated() {
        return this.authenticated;
    }

    public doRegister(emailAddress: string, password: string) {

        var login = new Login(emailAddress, password);

        return this._http
            .post(this._url + '/register', JSON.stringify(login))
            .map(res => res.json());
    }

    public getUserName() {
        return "Trolika Bogyika";
    }
}