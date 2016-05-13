import {Injectable, EventEmitter} from "angular2/core";
import {Http, Headers} from 'angular2/http'

export class MockAuthenticationService {
    private locationWatcher = new EventEmitter(); 

    public isAuthenticated() {
        return false;
    }
    public subscribe(onNext: (value: any) => void, onThrow?: (exception: any) => void, onReturn?: () => void) {
        return this.locationWatcher.subscribe(onNext, onThrow, onReturn);
    }

}