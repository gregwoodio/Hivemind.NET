import { TerritoryIncomeReport } from './TerritoryIncomeReport';

export class IncomeReport {
    public gross: TerritoryIncomeReport[];
    public giantKillerBonus: number;
    public income: number;

    public constructor(partial: Partial<IncomeReport>) {
        if (partial.gross) {
            this.gross = partial.gross;
        }
        if (partial.giantKillerBonus) {
            this.giantKillerBonus = partial.giantKillerBonus;
        }
        if (partial.income) {
            this.income = partial.income;
        }
    }
}