import {bootstrap} from '@angular/platform-browser-dynamic';
import {provide} from '@angular/core';
import {ROUTER_PROVIDERS} from '@angular/router-deprecated';
import {HTTP_PROVIDERS} from '@angular/http';
import {FORM_PROVIDERS, LocationStrategy, HashLocationStrategy} from '@angular/common';

import {AppComponent} from './app.component';

bootstrap(AppComponent, [
    ROUTER_PROVIDERS,
    HTTP_PROVIDERS,
    FORM_PROVIDERS,
    provide(LocationStrategy, { useClass: HashLocationStrategy })]);
