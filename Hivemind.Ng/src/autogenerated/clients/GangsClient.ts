/*
 * This file is autogenerated. Please see README.md for instructions on editing.
 */

import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Observable } from 'rxjs/Observable';
import { TokenService } from './../../app/redux/TokenService';
import { Gang } from '../entities/Gang';
import { GangWeapon } from '../entities/GangWeapon';
import { Weapon } from '../entities/Weapon';

@Injectable()
export class GangsClient {

    constructor(private _http: HttpClient, private _tokenService: TokenService) {}

    public GetGang(
        gangId: string,
    ): Observable<Gang> {

        return this._http.get<Gang>(
            'http://localhost:61774/api/gangs/' + gangId + ''
            , {
                headers: new HttpHeaders({
                    'Authorization': 'Bearer ' + this._tokenService.token,
                    'Content-Type': 'application/x-www-form-urlencoded'
                })
            }
        );
    }

    public AddGang(
        gang: Gang,
    ): Observable<Gang> {
        let body = gang.toHttpParams();

        return this._http.post<Gang>(
            'http://localhost:61774/api/gangs'
            , body.toString()
            , {
                headers: new HttpHeaders({
                    'Authorization': 'Bearer ' + this._tokenService.token,
                    'Content-Type': 'application/x-www-form-urlencoded'
                })
            }
        );
    }

    public GetWeapons(
        gangId: string,
    ): Observable<GangWeapon[]> {

        return this._http.get<GangWeapon[]>(
            'http://localhost:61774/api/gangs/' + gangId + '/weapons'
            , {
                headers: new HttpHeaders({
                    'Authorization': 'Bearer ' + this._tokenService.token,
                    'Content-Type': 'application/x-www-form-urlencoded'
                })
            }
        );
    }

    public AddGangWeapon(
        gangId: string,
        weapon: Weapon,
    ): Observable<GangWeapon> {
        let body = weapon.toHttpParams();

        return this._http.post<GangWeapon>(
            'http://localhost:61774/api/gangs/' + gangId + '/weapons'
            , body.toString()
            , {
                headers: new HttpHeaders({
                    'Authorization': 'Bearer ' + this._tokenService.token,
                    'Content-Type': 'application/x-www-form-urlencoded'
                })
            }
        );
    }

    public RemoveGangWeapon(
        gangId: string,
        gangWeaponId: string,
    ): Observable<string> {

        return this._http.delete<string>(
            'http://localhost:61774/api/gangs/' + gangId + '/weapons/' + gangWeaponId + ''
            , {
                headers: new HttpHeaders({
                    'Authorization': 'Bearer ' + this._tokenService.token,
                    'Content-Type': 'application/x-www-form-urlencoded'
                })
            }
        );
    }

    public UpdateGang(
        gang: Gang,
    ): Observable<Gang> {
        let body = gang.toHttpParams();

        return this._http.put<Gang>(
            'http://localhost:61774/api/Gangs'
            , body.toString()
            , {
                headers: new HttpHeaders({
                    'Authorization': 'Bearer ' + this._tokenService.token,
                    'Content-Type': 'application/x-www-form-urlencoded'
                })
            }
        );
    }

}