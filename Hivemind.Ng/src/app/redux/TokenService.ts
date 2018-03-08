import { SET_TOKEN } from './GangState';
import { NgRedux } from '@angular-redux/store';
import { IAppState } from './IAppState';
import { Injectable } from '@angular/core';

@Injectable()
export class TokenService {

  private _token: string;
  private _key = 'HMToken';

  public get token(): string {
    return this._token;
  }

  constructor(private _ngRedux: NgRedux<IAppState>) {
    let token = this._getCookie(this._key);
    if (token) {
      this.setToken(token);
    }
  }

  public setToken(token: string) {
    this._token = token;
    this._setCookie(this._key, token, 2);

    this._ngRedux.dispatch({
        type: SET_TOKEN,
        payload: token
    });
  }

  private _setCookie(cname, cvalue, exdays) {
    let d = new Date();
    d.setTime(d.getTime() + (exdays * 24 * 60 * 60 * 1000));
    let expires = 'expires='+ d.toUTCString();
    document.cookie = cname + '=' + cvalue + ';' + expires + ';path=/';
  }

  private _getCookie(cname) {
    let name = cname + '=';
    let decodedCookie = decodeURIComponent(document.cookie);
    let ca = decodedCookie.split(';');
    for (let i = 0; i <ca.length; i++) {
        let c = ca[i];
        while (c.charAt(0) == ' ') {
            c = c.substring(1);
        }
        if (c.indexOf(name) == 0) {
            return c.substring(name.length, c.length);
        }
    }
    return '';
  }
}
