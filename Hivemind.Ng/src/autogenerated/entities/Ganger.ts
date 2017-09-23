
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
    public skills: number;
    public injuries: number;
    public active: boolean;
    public title: string;
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
    }
}