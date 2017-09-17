import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs/Observable';
import { GangTerritory } from '../entities/GangTerritory';
import { Territory } from '../entities/Territory';

@Injectable()
export class TerritoriesClient {

    constructor(private _http: HttpClient) {}

    public GetGangTerritoryById(
        gangId: string,
    ): Observable<GangTerritory[]> {

        return this._http.get(
            'http://localhost:61774//api/territories/' + gangId + ''
        );
    }

    public AddGangTerritory(
        gangId: string,
        territory: Territory,
    ): Observable<GangTerritory> {
        let body = territory;

        return this._http.post(
            'http://localhost:61774//api/territories/' + gangId + ''
            , body
        );
    }

    public RemoveGangTerritory(
        gangTerritoryId: string,
    ): Observable<string> {

        return this._http.delete(
            'http://localhost:61774//api/territories/' + gangTerritoryId + ''
        );
    }

    public GetAllTerritories(
    ): Observable<Territory[]> {

        return this._http.get(
            'http://localhost:61774//api/Territories'
        );
    }

}