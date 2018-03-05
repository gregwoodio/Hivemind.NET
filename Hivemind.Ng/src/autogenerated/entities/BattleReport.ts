/*
 * This file is autogenerated. Please see README.md for instructions on editing.
 */

import { GangerBattleStats } from './GangerBattleStats';
import { HttpParams } from '@angular/common/http';

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

    public toHttpParams(): HttpParams {
        //return new HttpParams()
        //    .set('gangId', this.gangId !== undefined ? this.gangId.toString() : '')
        //    .set('gangBattleStats', this.gangBattleStats !== undefined ? this.gangBattleStats.toString() : '')
        //    .set('hasWon', this.hasWon !== undefined ? this.hasWon.toString() : '')
        //    .set('isAttacker', this.isAttacker !== undefined ? this.isAttacker.toString() : '')
        //    .set('opponentGangRating', this.opponentGangRating !== undefined ? this.opponentGangRating.toString() : '')
        //    .set('gameType', this.gameType !== undefined ? this.gameType.toString() : '')
        //      ;

        let params = new HttpParams();
        let properties = [];
        if (this.gangId) {
            properties.push('gangId');
        }
        if (this.gangBattleStats) {
            properties.push('gangBattleStats');
        }
        if (this.hasWon) {
            properties.push('hasWon');
        }
        if (this.isAttacker) {
            properties.push('isAttacker');
        }
        if (this.opponentGangRating) {
            properties.push('opponentGangRating');
        }
        if (this.gameType) {
            properties.push('gameType');
        }

        properties.forEach(prop => {
            params = params.set(prop, this[prop]);
        });

        return params;
    }
}