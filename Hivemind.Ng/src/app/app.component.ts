import { GangService } from './redux/GangService';
import { Component } from '@angular/core';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent {
  
  public title: string = "Hivemind NG";
  
  constructor(private _gangService: GangService) {
    this._gangService.getGang('38D577BE-2173-4B9F-B435-12A955BAC8AD');
  }
}
