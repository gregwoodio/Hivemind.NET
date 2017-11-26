import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Observable } from 'rxjs/Observable';
import { TokenService } from './../../app/tokenService/token.service';
import { User } from '../entities/User';
import { Login } from '../entities/Login';

@Injectable()
export class UsersClient {

    constructor(private _http: HttpClient, private _tokenService: TokenService) {}

    public GetUser(
    ): Observable<User> {

        return this._http.get<User>(
            'http://localhost:61774/api/user'
            , {
                headers: new HttpHeaders({
                    'Authorize': 'Bearer ' + this._tokenService.token
                })
            }
        );
    }

    public Register(
        user: Login,
    ): Observable<User> {
        let body = user;

        return this._http.post<User>(
            'http://localhost:61774/api/user'
            , body
            , {
                headers: new HttpHeaders({
                    'Authorize': 'Bearer ' + this._tokenService.token
                })
            }
        );
    }

}