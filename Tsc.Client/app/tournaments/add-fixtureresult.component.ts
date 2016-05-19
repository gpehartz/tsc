import {Router, RouteParams} from '@angular/router-deprecated';
import {Component, OnInit, Inject} from '@angular/core';
import {FormBuilder, Validators, Control, ControlGroup} from '@angular/common';

import {Tournament} from '../servicemodels/tournament';
import {Round} from '../servicemodels/round';
import {ITournamentService, TournamentServiceToken} from '../services/tournament.service';
import {FixtureDisplay} from '../models/fixturedisplay';
import {Fixture} from '../servicemodels/fixture';
import {MatchResult} from '../servicemodels/matchresult';

@Component({
    templateUrl: 'app/tournaments/add-fixtureresult.view.html'
})
export class AddFixtureResultComponent implements OnInit {
    tournament: Tournament;
    addFixtureResultForm: ControlGroup;
    fixtures: FixtureDisplay[] = new Array();
    selectedFixture: FixtureDisplay;
    match1Home: string;
    match1Away: string;
    match2Home: string;
    match2Away: string;
    match3Home: string;
    match3Away: string;

    constructor(private _router: Router,
        private _routeParams: RouteParams,
        private _fb: FormBuilder,
        @Inject(TournamentServiceToken) private _tournamentService: ITournamentService) {

        this.addFixtureResultForm = this._fb.group({
            result1Group: this._fb.group({
                inputMatch1Home: [""],
                inputMatch1Away: [""]
            }, { validator: AddFixtureResultComponent.validateResultGroup }),
            result2Group: this._fb.group({
                inputMatch2Home: [""],
                inputMatch2Away: [""]
            }, { validator: AddFixtureResultComponent.validateResultGroup }),
            result3Group: this._fb.group({
                inputMatch3Home: [""],
                inputMatch3Away: [""]
            }, { validator: AddFixtureResultComponent.validateResultGroup })
        });
    }

    ngOnInit() {
        let id = this._routeParams.get('id');

        this._tournamentService.getTournament(id).subscribe(item => {
            this.tournament = item.json();

            for (var round of this.tournament.rounds) {
                this.fixtures.push(new FixtureDisplay(null, round.number.toString() + '. forduló', false, true));

                for (var fixture of round.fixtures) {
                    if (!fixture.hasResult) {
                        this.fixtures.push(new FixtureDisplay(fixture, fixture.homeTeam + ' vs ' + fixture.awayTeam, false, false));
                    }
                }

                this.fixtures.push(new FixtureDisplay(null, null, true, false));
            }
        });
    }

    selectMatch(fixtureDisplay: FixtureDisplay) {
        this.selectedFixture = fixtureDisplay;
    }

    save() {
        var fixture = this.selectedFixture.fixture;
        fixture.results = new Array();
        fixture.results.push(new MatchResult(+this.match1Home, +this.match1Away));
        fixture.results.push(new MatchResult(+this.match2Home, +this.match2Away));
        fixture.results.push(new MatchResult(+this.match3Home, +this.match3Away));

        this._tournamentService.setFixtureResult(this.tournament.id, fixture).subscribe(item => this._router.navigate(['TournamentDetail', { id: this.tournament.id }]));
    }

    back() {
        this._router.navigate(['TournamentDetail', { id: this.tournament.id }]);
    }

    static validateResultGroup(group: ControlGroup) {
        var control1: Control;
        var control2: Control;

        var index = 0;
        for (var controlKey in group.controls) {
            if (index == 0) {
                control1 = <Control>group.controls[controlKey];
            }
            if (index == 1) {
                control2 = <Control>group.controls[controlKey];
            }
            index++;
        }

        var result = null;

        if (control1.value == null || control1.value == '' || control2.value == null || control2.value == '') {
            result = { notFilled: true };
        }
        if (!result && ((control1.value && control1.value.match(/.*[^0-9].*/)) || (control2.value && control2.value.match(/.*[^0-9].*/)))) {
            result = { notNumber: true };
        }
        if (!result && ((+control1.value) + (+control2.value) != 10)) {
            result = { invalidMatchResult: true };
        }

        if (result) {
            return result;
        }

        return null;
    }
}