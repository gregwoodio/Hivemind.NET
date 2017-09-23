import { Ganger } from './Ganger';
import { Injury } from './Injury';

export class GangerInjuryReport {
    public theGanger: Ganger;
    public injuries: Injury[];

    public constructor(partial: Partial<GangerInjuryReport>) {
        if (partial.theGanger) {
            this.theGanger = partial.theGanger;
        }
        if (partial.injuries) {
            this.injuries = partial.injuries;
        }
    }
}