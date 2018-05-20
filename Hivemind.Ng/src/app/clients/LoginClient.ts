import { ClientService } from './ClientService';
import { Injectable, OnInit } from '@angular/core';
import { HttpClient, HttpHeaders, HttpParams } from '@angular/common/http';
import { Observable } from 'rxjs/Observable';
import { TokenService } from './../../app/redux/TokenService';
import { LoginResponse } from './LoginResponse';
import 'rxjs/add/observable/of';
import { Subject } from 'rxjs/Subject';
import { _createDefaultCookieXSRFStrategy } from '@angular/http/src/http_module';
import { UserService } from './../../app/redux/UserService';

@Injectable()
export class LoginClient {

    private _path;
    private _loginUrl = '/api/login';
    private _loginSubject: Subject<LoginResponse>;

    constructor(
        private _http: HttpClient,
        private _clientService: ClientService,
        private _tokenService: TokenService,
        private _userService: UserService
    ) {
        this._loginSubject = new Subject<LoginResponse>();
        this._clientService.getPath().subscribe(path => {
            this._path = path;
        })
    }

    public Login(email: string, password: string): Observable<LoginResponse> {
        if (!this._path) {
            this._clientService.getPath();
        }

        let body = new HttpParams()
            .set('Email', email)
            .set('Password', password)
            .set('grant_type', 'password');

        let headers = new HttpHeaders({
            'Content-Type': 'application/x-www-form-urlencoded'
        });

        this._http.post(this._path + this._loginUrl, body.toString(), { 'headers': headers }).subscribe((data: any) => {
            if (data && data.access_token) {
                this._tokenService.setToken(data.access_token);

                this._userService.getUser();

                this._loginSubject.next(new LoginResponse({
                    success: true
                }));
            } else {
                this._loginSubject.next(new LoginResponse({
                    success: false,
                    message: 'Token was not received from the server.'
                }));
            }
        }, err => {
            let errorMessage;

            if (err.error) {
                const errorJson = JSON.parse(err.error);
                errorMessage = errorJson.error_description;
            }

            this._loginSubject.next(new LoginResponse({
                success: false,
                message: errorMessage
            }));
        });

        return this._loginSubject.asObservable();
    }
}
