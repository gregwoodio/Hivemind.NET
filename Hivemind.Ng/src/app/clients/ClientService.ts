import { Subject } from 'rxjs/Subject';
import { HttpClient } from '@angular/common/http';
import { OnInit, Injectable } from '@angular/core';
import { Observable } from 'rxjs/Observable';

@Injectable()
export class ClientService {

    public data: any;
    public dataObs: Observable<string>;
    private _dataSubject: Subject<string>;

    constructor(private _http: HttpClient) {
        this._dataSubject = new Subject<string>();
        this.dataObs = this._dataSubject.asObservable();

        this._http.get('./assets/data/config.json').subscribe((res: Response) => {
            this.data = res;
        });
    }

    public getPath(): Observable<string> {
        this._getPath();
        return this.dataObs;
    }

    private _getPath() {
        if (!this.data || !this.data['apiPath']) {
            this._http.get('./assets/data/config.json').subscribe((res: Response) => {
                this.data = res;
                this._dataSubject.next(this.data['apiPath']);
            });
            return;
        }

        this._dataSubject.next(this.data['apiPath']);
    }
}