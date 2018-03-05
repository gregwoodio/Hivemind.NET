/*
 * This file is autogenerated. Please see README.md for instructions on editing.
 */

import { HttpParams } from '@angular/common/http';

export class User {
    public email: string;
    public userGuid: string;
    public userGangIds: any;

    public constructor(partial: Partial<User>) {
        if (partial.email) {
            this.email = partial.email;
        }
        if (partial.userGuid) {
            this.userGuid = partial.userGuid;
        }
        if (partial.userGangIds) {
            this.userGangIds = partial.userGangIds;
        }
    }

    public toHttpParams(): HttpParams {
        //return new HttpParams()
        //    .set('email', this.email !== undefined ? this.email.toString() : '')
        //    .set('userGuid', this.userGuid !== undefined ? this.userGuid.toString() : '')
        //    .set('userGangIds', this.userGangIds !== undefined ? this.userGangIds.toString() : '')
        //      ;

        let params = new HttpParams();
        let properties = [];
        if (this.email) {
            properties.push('email');
        }
        if (this.userGuid) {
            properties.push('userGuid');
        }
        if (this.userGangIds) {
            properties.push('userGangIds');
        }

        properties.forEach(prop => {
            params = params.set(prop, this[prop]);
        });

        return params;
    }
}