import { Weapon } from './Weapon';

export class GangWeapon {
    public gangWeaponId: string;
    public gangId: string;
    public weapon: Weapon;

    public constructor(partial: Partial<GangWeapon>) {
        if (partial.gangWeaponId) {
            this.gangWeaponId = partial.gangWeaponId;
        }
        if (partial.gangId) {
            this.gangId = partial.gangId;
        }
        if (partial.weapon) {
            this.weapon = partial.weapon;
        }
    }
}