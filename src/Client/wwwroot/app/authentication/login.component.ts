import {Component} from "angular2/core";
import {ROUTER_DIRECTIVES, Router} from "angular2/router";
import {Location} from "angular2/router";
import {AuthenticationService} from '../services/mockauthentication.service';

@Component({
    selector: 'login',
    directives: [ROUTER_DIRECTIVES],
    providers: [AuthenticationService],
    pipes: [],
    templateUrl: 'app/authentication/login.view.html'
})

export class Login {

    public loginEmail: string;
    public loginPassword: string;
    public errorMessage: string;

    constructor(private location: Location, private router: Router, private authService: AuthenticationService) {

    }

    onLoginClick() {
        var isAuthenticated = this.authService.doLogin(this.loginEmail, this.loginPassword).subscribe(res => this.router.navigate(['LoggedoutPage']));

        if (!isAuthenticated) {
            this.errorMessage = "Hát ez a jelszó itten fos!"
        } else {
            //TODO:GO to my page
        }
    }

    onRegisterClick() {
        var isAuthenticated = this.authService.doRegister(this.loginEmail, this.loginPassword).subscribe(res => this.router.navigate(['LoggedoutPage']));

        if (!isAuthenticated) {
            this.errorMessage = "Sikertelen regisztáció!"
        } else {
            //TODO:GO to my page
        }
    }

    get authenticated() {
        return this.authService.isAuthenticated();
    }

    get userName() {
        return this.authService.getUserName();
    }

    get page() {
        return this.location.path().split('/')[1];
    }
}