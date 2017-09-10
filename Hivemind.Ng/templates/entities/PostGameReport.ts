import { InjuryReport } from './InjuryReport';
import { GangLevelUpReport } from './GangLevelUpReport';
import { IncomeReport } from './IncomeReport';

export class PostGameReport {
    public injuries: InjuryReport;
    public experience: GangLevelUpReport;
    public income: IncomeReport;
}