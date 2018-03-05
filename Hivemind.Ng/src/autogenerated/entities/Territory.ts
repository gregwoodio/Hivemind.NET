/*
 * This file is autogenerated. Please see README.md for instructions on editing.
 */

import { HttpParams } from '@angular/common/http';

export class Territory {
    public territoryId: number;
    public name: string;
    public description: string;
    public income: string;
    public workTerritory: any;

    public constructor(partial: Partial<Territory>) {
        if (partial.territoryId) {
            this.territoryId = partial.territoryId;
        }
        if (partial.name) {
            this.name = partial.name;
        }
        if (partial.description) {
            this.description = partial.description;
        }
        if (partial.income) {
            this.income = partial.income;
        }
        if (partial.workTerritory) {
            this.workTerritory = partial.workTerritory;
        }
    }

    public toHttpParams(): HttpParams {
        //return new HttpParams()
        //    .set('territoryId', this.territoryId !== undefined ? this.territoryId.toString() : '')
        //    .set('name', this.name !== undefined ? this.name.toString() : '')
        //    .set('description', this.description !== undefined ? this.description.toString() : '')
        //    .set('income', this.income !== undefined ? this.income.toString() : '')
        //    .set('workTerritory', this.workTerritory !== undefined ? this.workTerritory.toString() : '')
        //      ;

        let params = new HttpParams();
        let properties = [];
        if (this.territoryId) {
            properties.push('territoryId');
        }
        if (this.name) {
            properties.push('name');
        }
        if (this.description) {
            properties.push('description');
        }
        if (this.income) {
            properties.push('income');
        }
        if (this.workTerritory) {
            properties.push('workTerritory');
        }

        properties.forEach(prop => {
            params = params.set(prop, this[prop]);
        });

        return params;
    }
}