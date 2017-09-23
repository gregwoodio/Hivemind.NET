import { Ganger } from './Ganger';
import { GangTerritory } from './GangTerritory';

export class Gang {
    public gangId: string;
    public name: string;
    public credits: number;
    public gangHouse: string;
    public gangers: Ganger[];
    public territories: GangTerritory[];
    public gangRating: number;

    public constructor(partial: Partial<Gang>) {
        if (partial.gangId) {
            this.gangId = partial.gangId;
        }
        if (partial.name) {
            this.name = partial.name;
        }
        if (partial.credits) {
            this.credits = partial.credits;
        }
        if (partial.gangHouse) {
            this.gangHouse = partial.gangHouse;
        }
        if (partial.gangers) {
            this.gangers = partial.gangers;
        }
        if (partial.territories) {
            this.territories = partial.territories;
        }
        if (partial.gangRating) {
            this.gangRating = partial.gangRating;
        }
    }
}