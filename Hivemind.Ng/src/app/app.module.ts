import { GangersClient } from './clients/GangersClient';
import { GangerComponent } from './ganger/ganger.component';
import { GangComponent } from './gang/gang.component';
import { GangsClient } from './clients/GangsClient';
import { GangService } from './redux/GangService';
import { reduce } from './redux/GangState';
import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { HttpModule } from '@angular/http';

import { AppComponent } from './app.component';

import { NgReduxModule, NgRedux } from '@angular-redux/store';
import { IAppState } from './redux/IAppState';
import { Store, createStore } from "redux";
import { HttpClientModule } from "@angular/common/http";

var initialState: IAppState = {
  gang: null
};

export const createAppStore = () => {
  return createStore<IAppState>(reduce, initialState);
};

@NgModule({
  declarations: [
    AppComponent,
    GangComponent,
    GangerComponent
  ],
  imports: [
    BrowserModule,
    FormsModule,
    HttpClientModule
  ],
  providers: [
    { provide: NgRedux, useFactory: createAppStore },
    GangService,
    GangsClient,
    GangersClient
  ],
  bootstrap: [AppComponent]
})
export class AppModule { 
  constructor(ngRedux: NgRedux<IAppState>) {
    
  }
}
