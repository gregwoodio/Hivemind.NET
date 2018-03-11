import { Injectable } from '@angular/core';

@Injectable()
export class FormDataHelper {

    public getFormData(obj: any) {
        const formData = [];

        Object.keys(obj).forEach(key => {
            this.addItemsToForm(formData, obj[key], key);
        });

        let out = '';
        formData.forEach(data => {
            if (out !== '') {
                out += '&';
            }
            out += `${encodeURI(data['key'])}=${encodeURI(data['obj'])}`;
        });
        return out;
    }

    private addItemsToForm(form: any[], obj: any, name: string) {
        if (obj === undefined || obj === '' || obj === null) {
            return;
        }

        if (typeof obj === 'string' ||
            typeof obj === 'number' ||
            obj === true ||
            obj === false
        ) {
            return this.addItemToForm(form, obj, name);
        }

        if (obj instanceof Array) {
            return obj.forEach((value, i) => {
                this.addItemsToForm(form, value, name + '[' + i + ']');
            });
        }

        if (typeof obj === 'object') {
            return Object.keys(obj).forEach(key => {
                this.addItemsToForm(form, obj[key], name + '[' + key + ']');
            });
        }
    }

    private addItemToForm(form: any[], obj: any, key: string) {
        form.push({ key, obj });
    }
}
