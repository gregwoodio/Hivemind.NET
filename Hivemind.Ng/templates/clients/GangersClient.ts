import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs/Observable';
import { GangerWeapon } from '../entities/GangerWeapon';
import { Weapon } from '../entities/Weapon';
import { Ganger } from '../entities/Ganger';

@Injectable()
export class GangersClient {

    constructor(private _http: HttpClient) {}

    public GetWeapons(
        gangerId: string,
    ): Observable<GangerWeapon[]> {

        return this._http.get(
            'http://localhost:61774//api/gangers/' + gangerId + '/weapons'
        );
    }

    public AddGangerWeapon(
        gangerId: string,
        weapon: Weapon,
    ): Observable<GangerWeapon> {
        let body = weapon;

        return this._http.post(
            'http://localhost:61774//api/gangers/' + gangerId + '/weapons'
            , body
        );
    }

    public RemoveGangerWeapon(
        gangerId: string,
        gangerWeaponId: string,
    ): Observable<string> {

        return this._http.delete(
            'http://localhost:61774//api/gangers/' + gangerId + '/weapons/' + gangerWeaponId + ''
        );
    }

    public GetGanger(
    ): Observable<Ganger> {

        return this._http.get(
            'http://localhost:61774//api/Gangers'
        );
    }

    public UpdateGanger(
        ganger: Ganger,
    ): Observable<Ganger> {
        let body = ganger;

        return this._http.put(
            'http://localhost:61774//api/Gangers'
            , body
        );
    }

    public AddGanger(
        ganger: Ganger,
    ): Observable<Ganger> {
        let body = ganger;

        return this._http.post(
            'http://localhost:61774//api/Gangers'
            , body
        );
    }

}