import {Fixture} from '../servicemodels/fixture';

export class FixtureDisplay {
    constructor(public fixture: Fixture, public description: string, public isSeparator: boolean, public isHeader: boolean){ }
}