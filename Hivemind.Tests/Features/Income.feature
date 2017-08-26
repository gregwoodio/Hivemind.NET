Feature: IncomeService
	In order to avoid silly mistakes
	As a math idiot
	I want to be told the sum of two numbers

@mytag
Scenario: Get the correct gang income after upkeep 1
	Given the gang's income is 120
	And the gang has 10 members
	When I calculate the gang's upkeep
	Then the upkeep should be 45

Scenario: Get the correct gang income after upkeep 2
	Given the gang's income is 220
	And the gang has 6 members
	When I calculate the gang's upkeep
	Then the upkeep should be 80

Scenario: Get the correct giant killer bonus 1
	Given my gang has a rating of 1000
	And an opponent gang rating of 1150
	When I calculate the giant killer bonus
	Then the bonus should be 20

Scenario: Get the correct giant killer bonus 2
	Given my gang has a rating of 1000
	And an opponent gang rating of 2501
	When I calculate the giant killer bonus
	Then the bonus should be 250