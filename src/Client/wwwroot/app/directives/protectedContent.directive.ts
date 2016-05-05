import {Directive, OnDestroy, Input, TemplateRef, ViewContainerRef} from 'angular2/core';
import {MockAuthenticationService} from '../services/mockauthentication.service';
import {ROUTER_DIRECTIVES, Router} from "angular2/router";

@Directive({
    selector: '[protectedcontent]',
    providers: [MockAuthenticationService]
})

export class ProtectedContentDirective {
    //private sub:any = null;

    constructor(
        private authService: MockAuthenticationService,
        private templateRef: TemplateRef,
        private viewContainer: ViewContainerRef) {
        if (authService.isAuthenticated()) {
            this.viewContainer.createEmbeddedView(this.templateRef);
        } else {
            this.viewContainer.clear();
        }
    }
};