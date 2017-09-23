import { GangerBattleStats } from './GangerBattleStats';

export class BattleReport {
    public gangId: string;
    public gangBattleStats: GangerBattleStats[];
    public hasWon: boolean;
    public isAttacker: boolean;
    public opponentGangRating: number;
    public gameType: string;

    public constructor(partial: Partial<BattleReport>) {
        if (partial.gangId) {
            this.gangId = partial.gangId;
        }
        if (partial.gangBattleStats) {
            this.gangBattleStats = partial.gangBattleStats;
        }
        if (partial.hasWon) {
            this.hasWon = partial.hasWon;
        }
        if (partial.isAttacker) {
            this.isAttacker = partial.isAttacker;
        }
        if (partial.opponentGangRating) {
            this.opponentGangRating = partial.opponentGangRating;
        }
        if (partial.gameType) {
            this.gameType = partial.gameType;
        }
    }
}