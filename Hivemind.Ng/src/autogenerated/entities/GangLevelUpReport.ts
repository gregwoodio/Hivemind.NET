import { GangerLevelUpReport } from './GangerLevelUpReport';

export class GangLevelUpReport {
    public gangerAdvancements: GangerLevelUpReport[];

    public constructor(partial: Partial<GangLevelUpReport>) {
        if (partial.gangerAdvancements) {
            this.gangerAdvancements = partial.gangerAdvancements;
        }
    }
}