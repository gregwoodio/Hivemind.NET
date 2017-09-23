import { InjuryReport } from './InjuryReport';
import { GangLevelUpReport } from './GangLevelUpReport';
import { IncomeReport } from './IncomeReport';

export class PostGameReport {
    public injuries: InjuryReport;
    public experience: GangLevelUpReport;
    public income: IncomeReport;

    public constructor(partial: Partial<PostGameReport>) {
        if (partial.injuries) {
            this.injuries = partial.injuries;
        }
        if (partial.experience) {
            this.experience = partial.experience;
        }
        if (partial.income) {
            this.income = partial.income;
        }
    }
}