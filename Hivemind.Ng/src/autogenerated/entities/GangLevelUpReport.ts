/*
 * This file is autogenerated. Please see README.md for instructions on editing.
 */

import { GangerLevelUpReport } from './GangerLevelUpReport';
import { HttpParams } from '@angular/common/http';

export class GangLevelUpReport {
    public gangerAdvancements: GangerLevelUpReport[];

    public constructor(partial: Partial<GangLevelUpReport>) {
        if (partial.gangerAdvancements) {
            this.gangerAdvancements = partial.gangerAdvancements;
        }
    }

    public toHttpParams(): HttpParams {
        //return new HttpParams()
        //    .set('gangerAdvancements', this.gangerAdvancements !== undefined ? this.gangerAdvancements.toString() : '')
        //      ;

        let params = new HttpParams();
        let properties = [];
        if (this.gangerAdvancements) {
            properties.push('gangerAdvancements');
        }

        properties.forEach(prop => {
            params = params.set(prop, this[prop]);
        });

        return params;
    }
}