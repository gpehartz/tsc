import {Directive, OnDestroy} from '@angular/core';
import {ROUTER_DIRECTIVES, Router} from "@angular/router-deprecated";
import {Location} from "@angular/common";

import {AuthenticationService} from '../services/authentication.service';

@Directive({
    selector: '[protectedpage]'
})

export class ProtectedDirective implements OnDestroy {
    private sub: any = null;

    constructor(private authService: AuthenticationService, private router: Router, private location: Location) {
        if (!authService.isAuthenticated()) {
            this.location.replaceState('/');
            this.router.navigate(['PublicPage']);
        }

        // this.sub = this.authService.subscriber((val) => {
        //     if (!val.authenticated) {
        //         this.location.replaceState('/');
        //         this.router.navigate(['LoggedoutPage']);
        //     }
        // });
    }

    ngOnDestroy() {
        if (this.sub != null) {
            this.sub.unsubscribe();
        }
    }
}