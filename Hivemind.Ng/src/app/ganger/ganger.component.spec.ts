/* tslint:disable:no-unused-variable */
import { async, ComponentFixture, TestBed } from '@angular/core/testing';
import { By } from '@angular/platform-browser';
import { DebugElement } from '@angular/core';

import { GangerComponent } from './ganger.component';

describe('GangerComponent', () => {
  let component: GangerComponent;
  let fixture: ComponentFixture<GangerComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ GangerComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(GangerComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
