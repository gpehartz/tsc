import {MatchResult} from './matchresult';

export class Fixture {
    constructor(public id: string, public homeTeam: string, public awayTeam: string, public hasResult: boolean, public results: MatchResult[]) { }
}