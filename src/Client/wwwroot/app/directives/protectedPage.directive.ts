import {Directive, OnDestroy} from 'angular2/core';
import {MockAuthenticationService} from '../services/mockauthentication.service';
import {ROUTER_DIRECTIVES, Router, Location} from "angular2/router";

@Directive({
    selector: '[protectedpage]'
})

export class ProtectedDirective implements OnDestroy {
    private sub: any = null;

    constructor(private authService: MockAuthenticationService, private router: Router, private location: Location) {
        if (!authService.isAuthenticated()) {
            this.location.replaceState('/');
            this.router.navigate(['PublicPage']);
        }

        this.sub = this.authService.subscribe((val) => {
            if (!val.authenticated) {
                this.location.replaceState('/');
                this.router.navigate(['LoggedoutPage']);
            }
        });
    }

    ngOnDestroy() {
        if (this.sub != null) {
            this.sub.unsubscribe();
        }
    }
}