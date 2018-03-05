/*
 * This file is autogenerated. Please see README.md for instructions on editing.
 */

import { InjuryReport } from './InjuryReport';
import { GangLevelUpReport } from './GangLevelUpReport';
import { IncomeReport } from './IncomeReport';
import { HttpParams } from '@angular/common/http';

export class PostGameReport {
    public injuries: InjuryReport;
    public experience: GangLevelUpReport;
    public income: IncomeReport;

    public constructor(partial: Partial<PostGameReport>) {
        if (partial.injuries) {
            this.injuries = partial.injuries;
        }
        if (partial.experience) {
            this.experience = partial.experience;
        }
        if (partial.income) {
            this.income = partial.income;
        }
    }

    public toHttpParams(): HttpParams {
        //return new HttpParams()
        //    .set('injuries', this.injuries !== undefined ? this.injuries.toString() : '')
        //    .set('experience', this.experience !== undefined ? this.experience.toString() : '')
        //    .set('income', this.income !== undefined ? this.income.toString() : '')
        //      ;

        let params = new HttpParams();
        let properties = [];
        if (this.injuries) {
            properties.push('injuries');
        }
        if (this.experience) {
            properties.push('experience');
        }
        if (this.income) {
            properties.push('income');
        }

        properties.forEach(prop => {
            params = params.set(prop, this[prop]);
        });

        return params;
    }
}