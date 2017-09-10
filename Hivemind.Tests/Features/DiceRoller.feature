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

Scenario: Parse dice string D6*10
	Given no setup
	When I parse the dice string 'D6*10'
	Then the result should be between 10 and 60

Scenario: Parse dice string 10
	Given no setup
	When I parse the dice string '10'
	Then the result should be 10

Scenario: Parse dice string 2D6*10
	Given no setup
	When I parse the dice string '2D6*10'
	Then the result should be between 20 and 120

Scenario: Parse dice string 35+3D6
	Given no setup
	When I parse the dice string '35+3D6'
	Then the result should be between 36 and 53