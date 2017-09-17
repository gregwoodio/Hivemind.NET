Feature: Experience

@mytag
Scenario: Get the underdog bonus after winning with a small difference
	Given my gang has a rating of 1000
	And a battle report as follows:
		| OpponentGangRating | HasWon |
		| 1050               | true   | 
	When I calculate the underdog bonus
	Then the experience result should be 2

Scenario: Get the underdog bonus after losing with a large difference
	Given my gang has a rating of 1000 
	And a battle report as follows:
		| OpponentGangRating | HasWon |
		| 2501               | false  | 
	When I calculate the underdog bonus
	Then the experience result should be 9

Scenario: Calculate the wounding hit bonus
	Given a ganger has downed 3 opponents
	When I calculate the wounding hit bonus
	Then the experience result should be 15

Scenario: Get leader's bonus for winning a gang fight
	Given a ganger with experience as follows:
         | Name   | GangerType | Experience |
         | Leader | LEADER     | 61         |
	And a battle report as follows:
		| HasWon | GameType   |
		| true   | GANG_FIGHT |
	When I calculate the leader's bonus
	Then the experience result should be 10

Scenario: Get leader's bonus for winning the defense of a rescue mission
	Given a ganger with experience as follows:
         | Name   | GangerType | Experience |
         | Leader | LEADER     | 61         |
	And a battle report as follows:
         | HasWon | IsAttacker | GameType       |
         | true   | false      | RESCUE_MISSION |
	When I calculate the leader's bonus
	Then the experience result should be 10

Scenario: Get the appropriate number of advance rolls for a Juve.
	Given a ganger with experience as follows:
		| Name | GangerType | Experience |
		| Juve | JUVE       | 0          |
	When the ganger gets 16 experience
	Then the experience result should be 3

Scenario: Get the appropriate number of advance rolls for a Ganger
	Given a ganger with experience as follows:
		| Name   | Type   | Experience |
		| Ganger | GANGER | 40         |
	When the ganger gets 5 experience
	Then the experience result should be 1

Scenario: Get the correct experience for collecting objectives in a rescue mission
	Given a ganger collects 3 objectives
	And a battle report as follows:
         | HasWon | GameType       |
         | true   | RESCUE_MISSION |
	When I calculate the objective bonus
	Then the experience result should be 15

Scenario: Get the correct experience for collecting objectives in a gang fight
	Given a battle report as follows:
         | GameType   |
         | GANG_FIGHT |
	And a ganger collects 0 objectives
	When I calculate the objective bonus
	Then the experience result should be 0

Scenario: Get bonus for winning a hit and run scenario
	Given a battle report as follows:
         | HasWon | GameType    |
         | true   | HIT_AND_RUN |
	When I calculate the winning bonus
	Then the experience result should be 10