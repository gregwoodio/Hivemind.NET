import { GangerInjuryReport } from './GangerInjuryReport';

export class InjuryReport {
    public injuries: GangerInjuryReport[];

    public constructor(partial: Partial<InjuryReport>) {
        if (partial.injuries) {
            this.injuries = partial.injuries;
        }
    }
}