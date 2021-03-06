import { weaponType } from './../../autogenerated/entities/WeaponType';
import { TestBed, inject } from '@angular/core/testing';

import { FormDataHelper } from './FormDataHelper';
import { Ganger } from '../../autogenerated/entities/Ganger';
import { Weapon } from '../../autogenerated/entities/Weapon';
import { gangerType } from 'autogenerated/entities/GangerType';
import { GangerWeapon } from 'autogenerated/entities/GangerWeapon';

describe('FormDataHelper', () => {
  beforeEach(() => {
    TestBed.configureTestingModule({
      providers: [FormDataHelper]
    });
  });

  it('should encode arrays of objects properly', inject([FormDataHelper], (helper: FormDataHelper) => {
    const ganger = new Ganger({
        gangerId: '12345',
        gangerType: gangerType.Ganger.toString(),
        isCaptured: true,
        cost: 123,
        weapons: [
            new GangerWeapon({
                weapon: new Weapon({
                    name: 'weapon1',
                    weaponType: weaponType.Pistols.toString() // 2
                })
            }),
            new GangerWeapon({
                weapon: new Weapon({
                    name: 'weapon2',
                    weaponType: weaponType.Heavy.toString() // 5
                }),
            }),
            new GangerWeapon({
                weapon: new Weapon({
                    name: 'weapon3',
                    weaponType: weaponType.Grenades.toString() // 6
                })
            })
        ]
    });

    const expected = 'gangerId=12345&gangerType=2&cost=123&weapons%5B0%5D%5Bname%5D=weapon1' +
                '&weapons%5B0%5D%5BweaponType%5D=2&weapons%5B1%5D%5Bname%5D=weapon2&weapons%5B1%5D%5BweaponType%5D=5&' +
                'weapons%5B2%5D%5Bname%5D=weapon3&weapons%5B2%5D%5BweaponType%5D=6&isCaptured=true';

    const actual = helper.getFormData(ganger);

    expect(expected).toEqual(actual);
  }));
});
