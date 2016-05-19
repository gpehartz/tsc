import {Directive, OnDestroy, Input, TemplateRef, ViewContainerRef} from '@angular/core';
import {ROUTER_DIRECTIVES, Router} from "@angular/router-deprecated";

import {AuthenticationService} from '../services/authentication.service';

@Directive({
    selector: '[protectedcontent]',
    providers: [AuthenticationService]
})

export class ProtectedContentDirective {
    //private sub:any = null;

    constructor(
        private authService: AuthenticationService,
        private templateRef: TemplateRef<any>,
        private viewContainer: ViewContainerRef) {
        if (authService.isAuthenticated()) {
            this.viewContainer.createEmbeddedView(this.templateRef);
        } else {
            this.viewContainer.clear();
        }
    }
};