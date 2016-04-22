import {Component} from 'angular2/core';
import {Router} from 'angular2/router';
import {FormBuilder, Validators, Control, ControlGroup} from 'angular2/common';

@Component({
    templateUrl: 'app/tournaments/add-tournament.view.html'
})
export class AddTournamentComponent {
    addTournamentForm: ControlGroup;
    name: string;

    constructor(private _router: Router,
        fb: FormBuilder) {
        this.addTournamentForm = fb.group({
            inputTournamentName: ["", Validators.required]
        });
    }

    cancel() {
        this._router.navigate(['TournamentsCenter']);
    }
}