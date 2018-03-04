import { SET_TOKEN } from './GangState';
import { NgRedux } from '@angular-redux/store';
import { IAppState } from './IAppState';
import { Injectable } from '@angular/core';

@Injectable()
export class TokenService {

  public token: string;

  constructor(private ngRedux: NgRedux<IAppState>) { }

  public setToken(token: string) {
    this.ngRedux.dispatch({
        type: SET_TOKEN,
        payload: token
    });
  }
}
