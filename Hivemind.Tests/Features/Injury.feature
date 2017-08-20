Feature: Injury
	Gangers may incur injuries after games, which affect their statistics.

Scenario: A ganger has been killed
	Given a ganger with stats as follows:
	| Name   | Move | WeaponSkill | BallisticSkill | Strength | Toughness | Wounds | Initiative | Attack | Leadership | Experience | Active |
	| Ganger | 4    | 3           | 3              | 3        | 3         | 1      | 3          | 1       | 7          | 21         | true   |
	When the ganger gets the injury "DEAD"
	Then the ganger should not be active anymore

Scenario: A ganger has multiple injuries
	Given a ganger with stats as follows:
	| Name   | Move | WeaponSkill | BallisticSkill | Strength | Toughness | Wounds | Initiative | Attack | Leadership | Experience | Active |
	| Ganger | 4    | 3           | 3              | 3        | 3         | 1      | 3          | 1       | 7          | 21         | true   |
	When the ganger gets the injury "MULTIPLE_INJURIES"
	Then the ganger should get at least one more injury.

Scenario: A ganger has a chest wound.
	Given a ganger with stats as follows:
	| Name   | Move | WeaponSkill | BallisticSkill | Strength | Toughness | Wounds | Initiative | Attack | Leadership | Experience | Active |
	| Ganger | 4    | 3           | 3              | 3        | 3         | 1      | 3          | 1       | 7          | 21         | true   |
	When the ganger gets the injury "CHEST_WOUND"
	Then the ganger should have their toughness reduced by 1.

Scenario: A ganger has a leg wound.
	Given a ganger with stats as follows:
	| Name   | Move | WeaponSkill | BallisticSkill | Strength | Toughness | Wounds | Initiative | Attack | Leadership | Experience | Active |
	| Ganger | 4    | 3           | 3              | 3        | 3         | 1      | 3          | 1       | 7          | 21         | true   |
	When the ganger gets the injury "LEG_WOUND"
	Then the ganger should have their movement reduced by 1.

Scenario: A ganger has an arm wound.
	Given a ganger with stats as follows:
	| Name   | Move | WeaponSkill | BallisticSkill | Strength | Toughness | Wounds | Initiative | Attack | Leadership | Experience | Active |
	| Ganger | 4    | 3           | 3              | 3        | 3         | 1      | 3          | 1       | 7          | 21         | true   |
	When the ganger gets the injury "ARM_WOUND"
	Then the ganger should have their strength reduced by 1.

Scenario: A ganger has a head wound.
	Given a ganger with stats as follows:
	| Name   | Move | WeaponSkill | BallisticSkill | Strength | Toughness | Wounds | Initiative | Attack | Leadership | Experience | Active |
	| Ganger | 4    | 3           | 3              | 3        | 3         | 1      | 3          | 1       | 7          | 21         | true   |
	When the ganger gets the injury "HEAD_WOUND"
	Then the ganger should have a head wound.

Scenario: A ganger is blinded in one eye.
	Given a ganger with stats as follows:
	| Name   | Move | WeaponSkill | BallisticSkill | Strength | Toughness | Wounds | Initiative | Attack | Leadership | Experience | Active | IsOneEyed |
	| Ganger | 4    | 3           | 3              | 3        | 3         | 1      | 3          | 1       | 7          | 21         | true   | false     |
	When the ganger gets the injury "BLINDED_IN_ONE_EYE"
	Then the ganger should have their ballistic skill redeced by 1
	And the ganger should be marked as having only one eye.

Scenario: A ganger is blinded in their other eye.
	Given a ganger with stats as follows:
	| Name   | Move | WeaponSkill | BallisticSkill | Strength | Toughness | Wounds | Initiative | Attack | Leadership | Experience | Active | IsOneEyed |
	| Ganger | 4    | 3           | 3              | 3        | 3         | 1      | 3          | 1       | 7          | 21         | true   | true		 |
	When the ganger gets the injury "BLINDED_IN_ONE_EYE"
	Then the ganger should not be active anymore

Scenario: A ganger is partially deafened.
	Given a ganger with stats as follows:
	| Name   | Move | WeaponSkill | BallisticSkill | Strength | Toughness | Wounds | Initiative | Attack | Leadership | Experience | Active | IsDeafened |
	| Ganger | 4    | 3           | 3              | 3        | 3         | 1      | 3          | 1       | 7          | 21         | true   | false      |
	When the ganger gets the injury "PARTIALLY_DEAFENED"
	Then the ganger should be marked as partially deafened.

Scenario: A ganger is partially deafened again.
	Given a ganger with stats as follows:
	| Name   | Move | WeaponSkill | BallisticSkill | Strength | Toughness | Wounds | Initiative | Attack | Leadership | Experience | Active | IsDeafened |
	| Ganger | 4    | 3           | 3              | 3        | 3         | 1      | 3          | 1       | 7          | 21         | true   | true	      |
	When the ganger gets the injury "PARTIALLY_DEAFENED"
	Then the ganger should have their leadership reduced by 1.

Scenario: A ganger has shell shock.
	Given a ganger with stats as follows:
	| Name   | Move | WeaponSkill | BallisticSkill | Strength | Toughness | Wounds | Initiative | Attack | Leadership | Experience | Active | RightHandFingers | LeftHandFingers |
	| Ganger | 4    | 3           | 3              | 3        | 3         | 1      | 3          | 1       | 7          | 21         | true   | 5                | 5               |
	When the ganger gets the injury "SHELL_SHOCK"
	Then the ganger should have their initiative reduced by 1.

Scenario: A ganger has a hand injury.
	Given a ganger with stats as follows:
	| Name   | Move | WeaponSkill | BallisticSkill | Strength | Toughness | Wounds | Initiative | Attack | Leadership | Experience | Active | RightHandFingers | LeftHandFingers |
	| Ganger | 4    | 3           | 3              | 3        | 3         | 1      | 3          | 1       | 7          | 21         | true   | 5                | 5               |
	When the ganger gets the injury "HAND_INJURY"
	Then the ganger loses some fingers on either hand.
	And the ganger should have their weapon skill reduced by 1.

Scenario: A ganger has a hand injury resulting in loss of the arm.
	Given a ganger with stats as follows:
	| Name   | Move | WeaponSkill | BallisticSkill | Strength | Toughness | Wounds | Initiative | Attack | Leadership | Experience | Active | RightHandFingers | LeftHandFingers |
	| Ganger | 4    | 3           | 3              | 3        | 3         | 1      | 3          | 1       | 7          | 21         | true   | 1                | 1               |
	When the ganger gets the injury "HAND_INJURY"
	Then the ganger should be one armed.

Scenario: A ganger has an old battle wound.
	Given a ganger with stats as follows:
	| Name   | Move | WeaponSkill | BallisticSkill | Strength | Toughness | Wounds | Initiative | Attack | Leadership | Experience | Active | RightHandFingers | LeftHandFingers |
	| Ganger | 4    | 3           | 3              | 3        | 3         | 1      | 3          | 1       | 7          | 21         | true   | 5                | 5               |
	When the ganger gets the injury "OLD_BATTLE_WOUND"
	Then the ganger should have an old battle wound.

Scenario: A ganger has made a full recovery.
	Given a ganger with stats as follows:
	| Name   | Move | WeaponSkill | BallisticSkill | Strength | Toughness | Wounds | Initiative | Attack | Leadership | Experience | Active | RightHandFingers | LeftHandFingers |
	| Ganger | 4    | 3           | 3              | 3        | 3         | 1      | 3          | 1       | 7          | 21         | true   | 5                | 5               |
	When the ganger gets the injury "FULL_RECOVERY"
	Then the ganger should not have a statistics change.

Scenario: A ganger has bitter enmity.
	Given a ganger with stats as follows:
	| Name   | Move | WeaponSkill | BallisticSkill | Strength | Toughness | Wounds | Initiative | Attack | Leadership | Experience | Active | RightHandFingers | LeftHandFingers |
	| Ganger | 4    | 3           | 3              | 3        | 3         | 1      | 3          | 1       | 7          | 21         | true   | 5                | 5               |
	When the ganger gets the injury "BITTER_ENMITY"
	Then the ganger should have bitter enmity.

Scenario: A ganger was captured.
	Given a ganger with stats as follows:
	| Name   | Move | WeaponSkill | BallisticSkill | Strength | Toughness | Wounds | Initiative | Attack | Leadership | Experience | Active | RightHandFingers | LeftHandFingers |
	| Ganger | 4    | 3           | 3              | 3        | 3         | 1      | 3          | 1       | 7          | 21         | true   | 5                | 5               |
	When the ganger gets the injury "CAPTURED"
	Then the ganger should be marked as a captive.

Scenario: A ganger has horrible scars.
	Given a ganger with stats as follows:
	| Name   | Move | WeaponSkill | BallisticSkill | Strength | Toughness | Wounds | Initiative | Attack | Leadership | Experience | Active | RightHandFingers | LeftHandFingers |
	| Ganger | 4    | 3           | 3              | 3        | 3         | 1      | 3          | 1       | 7          | 21         | true   | 5                | 5               |
	When the ganger gets the injury "HORRIBLE_SCARS"
	Then the ganger should have horrible scars.

Scenario: A ganger has impressive scars.
	Given a ganger with stats as follows:
	| Name   | Move | WeaponSkill | BallisticSkill | Strength | Toughness | Wounds | Initiative | Attack | Leadership | Experience | Active | HasImpressiveScars |
	| Ganger | 4    | 3           | 3              | 3        | 3         | 1      | 3          | 1       | 7          | 21         | true   | false          |
	When the ganger gets the injury "IMPRESSIVE_SCARS"
	Then the ganger's leadership should increase by 1.
	And the ganger should be marked as having impressive scars.

Scenario: A ganger gets impressive scars again.
	Given a ganger with stats as follows:
	| Name   | Move | WeaponSkill | BallisticSkill | Strength | Toughness | Wounds | Initiative | Attack | Leadership | Experience | Active | HasImpressiveScars |
	| Ganger | 4    | 3           | 3              | 3        | 3         | 1      | 3          | 1      | 7          | 21         | true   | true  |
	When the ganger gets the injury "IMPRESSIVE_SCARS"
	Then the ganger should not have a statistics change.

Scenario: A ganger survives against the odds.
	Given a ganger with stats as follows:
	| Name   | Move | WeaponSkill | BallisticSkill | Strength | Toughness | Wounds | Initiative | Attack | Leadership | Experience | Active | HasImpressiveScars |
	| Ganger | 4    | 3           | 3              | 3        | 3         | 1      | 3          | 1      | 7          | 21         | true   | true  |
	When the ganger gets the injury "SURVIVES_AGAINST_THE_ODDS"
	Then the ganger should have an experience increase.