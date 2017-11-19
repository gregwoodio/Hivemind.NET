
export class Territory {
    public territoryId: number;
    public name: string;
    public description: string;
    public income: string;
    public workTerritory: any;

    public constructor(partial: Partial<Territory>) {
        if (partial.territoryId) {
            this.territoryId = partial.territoryId;
        }
        if (partial.name) {
            this.name = partial.name;
        }
        if (partial.description) {
            this.description = partial.description;
        }
        if (partial.income) {
            this.income = partial.income;
        }
        if (partial.workTerritory) {
            this.workTerritory = partial.workTerritory;
        }
    }
}