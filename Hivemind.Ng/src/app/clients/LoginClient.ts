import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders, HttpParams } from '@angular/common/http';
import { Observable } from 'rxjs/Observable';
import { TokenService } from './../../app/redux/TokenService';
import { LoginResponse } from './LoginResponse';
import 'rxjs/add/observable/of';
import { Subject } from 'rxjs/Subject';
import { _createDefaultCookieXSRFStrategy } from '@angular/http/src/http_module';

@Injectable()
export class LoginClient {

    private _loginUrl = 'http://localhost:61774/api/login';
    private _loginSubject: Subject<LoginResponse>;

    constructor(
        private _http: HttpClient,
        private _tokenService: TokenService,
    ) {
        this._loginSubject = new Subject<LoginResponse>();
    }

    public Login(email: string, password: string): Observable<LoginResponse> {
        let body = new HttpParams()
            .set('UserName', email)
            .set('Password', password)
            .set('grant_type', 'password');

        let headers = new HttpHeaders({
            'Content-Type': 'application/x-www-form-urlencoded'
        });

        this._http.post(this._loginUrl, body.toString(), { 'headers': headers }).subscribe((data: any) => {
            if (data && data.access_token) {
                this._tokenService.token = data.access_token;
                // TODO: Call user service, get user info and gangs.
                // Call getGang of first gang, or create an empty gang.

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
            console.log(err);
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
