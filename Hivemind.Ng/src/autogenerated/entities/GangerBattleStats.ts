
export class GangerBattleStats {
    public gangerId: string;
    public kills: number;
    public objectives: number;
    public down: boolean;
    public outOfAction: boolean;

    public constructor(partial: Partial<GangerBattleStats>) {
        if (partial.gangerId) {
            this.gangerId = partial.gangerId;
        }
        if (partial.kills) {
            this.kills = partial.kills;
        }
        if (partial.objectives) {
            this.objectives = partial.objectives;
        }
        if (partial.down) {
            this.down = partial.down;
        }
        if (partial.outOfAction) {
            this.outOfAction = partial.outOfAction;
        }
    }
}