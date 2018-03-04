import { NgRedux } from '@angular-redux/store';
import { IAppState } from './IAppState';
import { Injectable } from '@angular/core';

@Injectable()
export class UserService {

  constructor(private ngRedux: NgRedux<IAppState>) { }

}
