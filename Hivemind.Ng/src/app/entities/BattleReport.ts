import { GangerBattleStats } from './GangerBattleStats';

export class BattleReport {
    public gangId: string;
    public gangBattleStats: GangerBattleStats[];
    public hasWon: boolean;
    public isAttacker: boolean;
    public opponentGangRating: number;
    public gameType: string;
}