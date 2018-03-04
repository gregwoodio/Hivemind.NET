import { SET_TOKEN } from './GangState';
import { NgRedux } from '@angular-redux/store';
import { IAppState } from './IAppState';
import { Injectable } from '@angular/core';

@Injectable()
export class TokenService {

  private _token: string;

  public get token(): string {
    return this._token;
  }

  constructor(private _ngRedux: NgRedux<IAppState>) {

  }

  public setToken(token: string) {
    this._token = token;
    this._ngRedux.dispatch({
        type: SET_TOKEN,
        payload: token
    });
  }
}
