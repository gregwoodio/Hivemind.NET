/* tslint:disable:no-unused-variable */
import { async, ComponentFixture, TestBed } from '@angular/core/testing';
import { By } from '@angular/platform-browser';
import { DebugElement } from '@angular/core';
import { GangersComponent } from 'app/gangers/gangers.component';

describe('GangerComponent', () => {
  let component: GangersComponent;
  let fixture: ComponentFixture<GangersComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ GangersComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(GangersComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
