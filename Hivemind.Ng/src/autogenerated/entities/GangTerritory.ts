import { Territory } from './Territory';

export class GangTerritory {
    public gangTerritoryId: string;
    public gangId: string;
    public territory: Territory;

    public constructor(partial: Partial<GangTerritory>) {
        if (partial.gangTerritoryId) {
            this.gangTerritoryId = partial.gangTerritoryId;
        }
        if (partial.gangId) {
            this.gangId = partial.gangId;
        }
        if (partial.territory) {
            this.territory = partial.territory;
        }
    }
}