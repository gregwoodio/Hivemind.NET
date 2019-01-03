Feature: Users
	
Scenario: Add new user
	When I add a user as follows: 
		| Email               | Password |
		| testuser3@email.com | hunter2  |
	When I retrieve token with using:
		| Email               | Password |
		| testuser3@email.com | hunter2  |
	Then I should receive a token

Scenario: Try to get user information without logging in
	When I get user information
	Then I should receive an error as follow:
		| ErrorMessage |
		| Dang!        |
