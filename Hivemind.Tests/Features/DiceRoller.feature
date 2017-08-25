Feature: DiceRoller

@mytag
Scenario: Roll a die
	Given no setup
	When I roll a die
	Then the result should be between 1 and 6

Scenario: Roll 2D6
	Given no setup
	When I roll 2 dice with 6 sides
	Then the result should be between 2 and 12

Scenario: Roll D66
	Given no setup
	When I roll D66
	Then the result should be between valid for a D66.