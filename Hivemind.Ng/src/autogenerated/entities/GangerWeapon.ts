import { Weapon } from './Weapon';

export class GangerWeapon {
    public gangerWeaponId: string;
    public gangerId: string;
    public weapon: Weapon;

    public constructor(partial: Partial<GangerWeapon>) {
        if (partial.gangerWeaponId) {
            this.gangerWeaponId = partial.gangerWeaponId;
        }
        if (partial.gangerId) {
            this.gangerId = partial.gangerId;
        }
        if (partial.weapon) {
            this.weapon = partial.weapon;
        }
    }
}