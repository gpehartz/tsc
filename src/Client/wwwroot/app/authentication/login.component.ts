import {Component} from "angular2/core";
import {ROUTER_DIRECTIVES, Router} from "angular2/router";
import {Location} from "angular2/router";
import {MockAuthenticationService} from '../services/mockauthentication.service';

@Component({
    selector: 'login',
    directives: [ROUTER_DIRECTIVES],
    providers: [MockAuthenticationService],
    pipes: [],
    templateUrl: 'app/authentication/login.view.html'
})

export class Login {
    constructor(private location: Location, private router: Router, private authService: MockAuthenticationService) {
    }

    get authenticated() {
        return this.authService.isAuthenticated();
    }

    //doLogin() {
    //    this.authService.doLogin();
    //}

    //doLogout() {
    //    this.authService.doLogout();
    //}

    //get userName() {
    //    return this.authService.getUserName();
    //}

    get page() {
        return this.location.path().split('/')[1];
    }
}