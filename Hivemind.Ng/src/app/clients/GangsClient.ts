import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs/Observable';
import { Gang } from '../entities/Gang';
import { GangWeapon } from '../entities/GangWeapon';
import { Weapon } from '../entities/Weapon';

@Injectable()
export class GangsClient {

    constructor(private _http: HttpClient) {}

    public GetGang(
        gangId: string,
    ): Observable<Gang> {

        return this._http.get(
            'http://localhost:61774//api/gangs/' + gangId + ''
        );
    }

    public AddGang(
        gangName: string,
        house: ,
    ): Observable<Gang> {
        let body = house;

        return this._http.post(
            'http://localhost:61774//api/gangs/' + gangName + ''
            , body
        );
    }

    public GetWeapons(
        gangId: string,
    ): Observable<GangWeapon[]> {

        return this._http.get(
            'http://localhost:61774//api/gangs/' + gangId + '/weapons'
        );
    }

    public AddGangWeapon(
        gangId: string,
        weapon: Weapon,
    ): Observable<GangWeapon> {
        let body = weapon;

        return this._http.post(
            'http://localhost:61774//api/gangs/' + gangId + '/weapons'
            , body
        );
    }

    public RemoveGangWeapon(
        gangId: string,
        gangWeaponId: string,
    ): Observable<string> {

        return this._http.delete(
            'http://localhost:61774//api/gangs/' + gangId + '/weapons/' + gangWeaponId + ''
        );
    }

    public UpdateGang(
        gang: Gang,
    ): Observable<Gang> {
        let body = gang;

        return this._http.put(
            'http://localhost:61774//api/Gangs'
            , body
        );
    }

}