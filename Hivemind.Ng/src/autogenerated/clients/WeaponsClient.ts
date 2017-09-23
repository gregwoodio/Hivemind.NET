import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs/Observable';
import { Weapon } from '../entities/Weapon';

@Injectable()
export class WeaponsClient {

    constructor(private _http: HttpClient) {}

    public GetWeapon(
        weaponId: number,
    ): Observable<Weapon> {

        return this._http.get<Weapon>(
            'http://localhost:61774/api/weapons/' + weaponId + ''
        );
    }

    public GetAllWeapons(
    ): Observable<Weapon[]> {

        return this._http.get<Weapon[]>(
            'http://localhost:61774/api/Weapons'
        );
    }

}