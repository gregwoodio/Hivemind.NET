import { HttpClient } from '@angular/common/http';
import { OnInit, Injectable } from '@angular/core';

@Injectable()
export class ClientService {

    public data: any;

    constructor(private _http: HttpClient) {
        this._http.get('./assets/data/config.json').subscribe((res: Response) => {
            this.data = res;
        });
    }

    public getPath() {
        if (!this.data || !this.data['apiPath']) {
            this._http.get('./assets/data/config.json').subscribe((res: Response) => {
                this.data = res;
                return this.data['apiPath'];
            });
        } else {
            return this.data['apiPath'];
        }
    }
}