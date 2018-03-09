import { GangerService } from './../redux/GangerService';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { NgRedux } from '@angular-redux/store';
import { IAppState } from './../redux/IAppState';
import { Gang } from './../../autogenerated/entities/Gang';
import { Ganger } from './../../autogenerated/entities/Ganger';
import { Component, OnInit, Input } from '@angular/core';

@Component({
  selector: 'gangers',
  templateUrl: './gangers.component.html',
  styleUrls: ['./gangers.component.css']
})
export class GangersComponent implements OnInit {
  public gangers: Ganger[];
  public gang: Gang;
  public addGangerForm: FormGroup;
  public showAddGangerDialog: boolean;

  constructor(
    private _formBuilder: FormBuilder,
    private _gangerService: GangerService,
    private _ngRedux: NgRedux<IAppState>
  ) {
    this._ngRedux.subscribe(() => {
      const state = this._ngRedux.getState();
      this.gang = state.gang;
      this.gangers = state.gang.gangers;
    });

    this.addGangerForm = _formBuilder.group({
      'gangerName': ['', Validators.required],
      'gangerType': ['', Validators.required]
    });

    this.gangers = new Array<Ganger>();
  }

  public ngOnInit() {
    const state = this._ngRedux.getState();
    this.gang = state.gang;
    if (state.gang && state.gang.gangers) {
      this.gangers = state.gang.gangers;
    }
  }

  public displayAddGangerDialog() {
    this.addGangerForm.controls['gangerName'].setValue('');
    this.addGangerForm.controls['gangerType'].setValue('');
    this.showAddGangerDialog = true;
  }

  public checkAddGangerType() {
    // check if the gang can afford to add a new ganger
  }

  public submitAddGangerForm() {
    const ganger = new Ganger({
      name: this.addGangerForm.controls['gangerName'].value,
      gangerType: this.addGangerForm.controls['gangerType'].value,
      gangId: this.gang.gangId
    });

    this._gangerService.addGanger(ganger);
    this.showAddGangerDialog = false;
  }

  public parseGangerEquipment(ganger: Ganger): string {
    let out = '';

    for (let i = 0; i < ganger.weapons.length; i++) {
      out += ganger.weapons[i].name;

      if (i + 1 < ganger.weapons.length) {
        out += ', ';
      }
    }

    return out;
  }
}
