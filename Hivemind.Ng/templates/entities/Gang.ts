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
}