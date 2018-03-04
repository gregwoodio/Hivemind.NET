/*
 * This file is autogenerated. Please see README.md for instructions on editing.
 */


export class TerritoryIncomeReport {
    public description: string;
    public income: number;

    public constructor(partial: Partial<TerritoryIncomeReport>) {
        if (partial.description) {
            this.description = partial.description;
        }
        if (partial.income) {
            this.income = partial.income;
        }
    }
}