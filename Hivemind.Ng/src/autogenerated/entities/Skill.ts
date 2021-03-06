/*
 * This file is autogenerated. Please see README.md for instructions on editing.
 */

import { HttpParams } from '@angular/common/http';

export class Skill {
    public skillId: number;
    public name: string;
    public description: string;
    public skillType: string;
    public restrictedTypes: any;

    public constructor(partial: Partial<Skill>) {
        if (partial.skillId) {
            this.skillId = partial.skillId;
        }
        if (partial.name) {
            this.name = partial.name;
        }
        if (partial.description) {
            this.description = partial.description;
        }
        if (partial.skillType) {
            this.skillType = partial.skillType;
        }
        if (partial.restrictedTypes) {
            this.restrictedTypes = partial.restrictedTypes;
        }
    }

    public toHttpParams(): HttpParams {
        let params = new HttpParams();
        let properties = [];
        if (this.skillId) {
            properties.push('skillId');
        }
        if (this.name) {
            properties.push('name');
        }
        if (this.description) {
            properties.push('description');
        }
        if (this.skillType) {
            properties.push('skillType');
        }
        if (this.restrictedTypes) {
            properties.push('restrictedTypes');
        }

        properties.forEach(prop => {
            params = params.set(prop, this[prop]);
        });

        return params;
    }
}