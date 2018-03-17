/*
 * This file is autogenerated. Please see README.md for instructions on editing.
 */

import { Skill } from './Skill';
import { Injury } from './Injury';
import { Weapon } from './Weapon';
import { HttpParams } from '@angular/common/http';

export class Ganger {
    public gangerId: string;
    public gangId: string;
    public name: string;
    public gangerType: string;
    public move: number;
    public weaponSkill: number;
    public ballisticSkill: number;
    public strength: number;
    public toughness: number;
    public wounds: number;
    public initiative: number;
    public attack: number;
    public leadership: number;
    public experience: number;
    public cost: number;
    public skills: Skill[];
    public injuries: Injury[];
    public active: boolean;
    public title: string;
    public weapons: Weapon[];
    public isEnabled: boolean;
    public isOneEyed: boolean;
    public isDeafened: boolean;
    public isOneHanded: boolean;
    public rightHandFingers: number;
    public leftHandFingers: number;
    public hasHorribleScars: boolean;
    public hasImpressiveScars: boolean;
    public hasHeadWound: boolean;
    public hasOldBattleWound: boolean;
    public isCaptured: boolean;
    public hasBitterEnmity: boolean;
    public hasSporeSickness: boolean;
    public hasFleshWound: boolean;

    public constructor(partial: Partial<Ganger>) {
        if (partial.gangerId) {
            this.gangerId = partial.gangerId;
        }
        if (partial.gangId) {
            this.gangId = partial.gangId;
        }
        if (partial.name) {
            this.name = partial.name;
        }
        if (partial.gangerType) {
            this.gangerType = partial.gangerType;
        }
        if (partial.move) {
            this.move = partial.move;
        }
        if (partial.weaponSkill) {
            this.weaponSkill = partial.weaponSkill;
        }
        if (partial.ballisticSkill) {
            this.ballisticSkill = partial.ballisticSkill;
        }
        if (partial.strength) {
            this.strength = partial.strength;
        }
        if (partial.toughness) {
            this.toughness = partial.toughness;
        }
        if (partial.wounds) {
            this.wounds = partial.wounds;
        }
        if (partial.initiative) {
            this.initiative = partial.initiative;
        }
        if (partial.attack) {
            this.attack = partial.attack;
        }
        if (partial.leadership) {
            this.leadership = partial.leadership;
        }
        if (partial.experience) {
            this.experience = partial.experience;
        }
        if (partial.cost) {
            this.cost = partial.cost;
        }
        if (partial.skills) {
            this.skills = partial.skills;
        }
        if (partial.injuries) {
            this.injuries = partial.injuries;
        }
        if (partial.active) {
            this.active = partial.active;
        }
        if (partial.title) {
            this.title = partial.title;
        }
        if (partial.weapons) {
            this.weapons = partial.weapons;
        }
        if (partial.isEnabled) {
            this.isEnabled = partial.isEnabled;
        }
        if (partial.isOneEyed) {
            this.isOneEyed = partial.isOneEyed;
        }
        if (partial.isDeafened) {
            this.isDeafened = partial.isDeafened;
        }
        if (partial.isOneHanded) {
            this.isOneHanded = partial.isOneHanded;
        }
        if (partial.rightHandFingers) {
            this.rightHandFingers = partial.rightHandFingers;
        }
        if (partial.leftHandFingers) {
            this.leftHandFingers = partial.leftHandFingers;
        }
        if (partial.hasHorribleScars) {
            this.hasHorribleScars = partial.hasHorribleScars;
        }
        if (partial.hasImpressiveScars) {
            this.hasImpressiveScars = partial.hasImpressiveScars;
        }
        if (partial.hasHeadWound) {
            this.hasHeadWound = partial.hasHeadWound;
        }
        if (partial.hasOldBattleWound) {
            this.hasOldBattleWound = partial.hasOldBattleWound;
        }
        if (partial.isCaptured) {
            this.isCaptured = partial.isCaptured;
        }
        if (partial.hasBitterEnmity) {
            this.hasBitterEnmity = partial.hasBitterEnmity;
        }
        if (partial.hasSporeSickness) {
            this.hasSporeSickness = partial.hasSporeSickness;
        }
        if (partial.hasFleshWound) {
            this.hasFleshWound = partial.hasFleshWound;
        }
    }

    public toHttpParams(): HttpParams {
        let params = new HttpParams();
        let properties = [];
        if (this.gangerId) {
            properties.push('gangerId');
        }
        if (this.gangId) {
            properties.push('gangId');
        }
        if (this.name) {
            properties.push('name');
        }
        if (this.gangerType) {
            properties.push('gangerType');
        }
        if (this.move) {
            properties.push('move');
        }
        if (this.weaponSkill) {
            properties.push('weaponSkill');
        }
        if (this.ballisticSkill) {
            properties.push('ballisticSkill');
        }
        if (this.strength) {
            properties.push('strength');
        }
        if (this.toughness) {
            properties.push('toughness');
        }
        if (this.wounds) {
            properties.push('wounds');
        }
        if (this.initiative) {
            properties.push('initiative');
        }
        if (this.attack) {
            properties.push('attack');
        }
        if (this.leadership) {
            properties.push('leadership');
        }
        if (this.experience) {
            properties.push('experience');
        }
        if (this.cost) {
            properties.push('cost');
        }
        if (this.skills) {
            properties.push('skills');
        }
        if (this.injuries) {
            properties.push('injuries');
        }
        if (this.active) {
            properties.push('active');
        }
        if (this.title) {
            properties.push('title');
        }
        if (this.weapons) {
            properties.push('weapons');
        }
        if (this.isEnabled) {
            properties.push('isEnabled');
        }
        if (this.isOneEyed) {
            properties.push('isOneEyed');
        }
        if (this.isDeafened) {
            properties.push('isDeafened');
        }
        if (this.isOneHanded) {
            properties.push('isOneHanded');
        }
        if (this.rightHandFingers) {
            properties.push('rightHandFingers');
        }
        if (this.leftHandFingers) {
            properties.push('leftHandFingers');
        }
        if (this.hasHorribleScars) {
            properties.push('hasHorribleScars');
        }
        if (this.hasImpressiveScars) {
            properties.push('hasImpressiveScars');
        }
        if (this.hasHeadWound) {
            properties.push('hasHeadWound');
        }
        if (this.hasOldBattleWound) {
            properties.push('hasOldBattleWound');
        }
        if (this.isCaptured) {
            properties.push('isCaptured');
        }
        if (this.hasBitterEnmity) {
            properties.push('hasBitterEnmity');
        }
        if (this.hasSporeSickness) {
            properties.push('hasSporeSickness');
        }
        if (this.hasFleshWound) {
            properties.push('hasFleshWound');
        }

        properties.forEach(prop => {
            params = params.set(prop, this[prop]);
        });

        return params;
    }
}