Feature: Users
	
Scenario: Add new user
	When I add a user as follows: 
		| Email              | Password |
		| testuser@email.com | hunter2  |
	Then the user should be added

Scenario: Try to get user information without logging in
	When I get user information
	Then I should receive an error as follow:
		| ErrorMessage |
		| Dang!        |
