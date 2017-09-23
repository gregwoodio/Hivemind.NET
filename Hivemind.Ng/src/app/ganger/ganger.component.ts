import { Ganger } from './../entities/Ganger';
import { Component, OnInit, Input } from '@angular/core';

@Component({
  selector: 'ganger',
  templateUrl: './ganger.component.html',
  styleUrls: ['./ganger.component.css']
})
export class GangerComponent {

  @Input() public ganger: Ganger;

}
