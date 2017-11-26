import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Observable } from 'rxjs/Observable';
import { TokenService } from './../../app/tokenService/token.service';
import { Weapon } from '../entities/Weapon';

@Injectable()
export class WeaponsClient {

    constructor(private _http: HttpClient, private _tokenService: TokenService) {}

    public GetWeapon(
        weaponId: number,
    ): Observable<Weapon> {

        return this._http.get<Weapon>(
            'http://localhost:61774/api/weapons/' + weaponId + ''
            , {
                headers: new HttpHeaders({
                    'Authorize': 'Bearer ' + this._tokenService.token
                })
            }
        );
    }

    public GetAllWeapons(
    ): Observable<Weapon[]> {

        return this._http.get<Weapon[]>(
            'http://localhost:61774/api/Weapons'
            , {
                headers: new HttpHeaders({
                    'Authorize': 'Bearer ' + this._tokenService.token
                })
            }
        );
    }

}