Feature: Application Configuration

@mytag
Scenario: Blank State
	Given a new instance of an application is created
	When get the number of properties
	Then the result should be 0

Scenario: Add Property
	Given a new instance of an application is created
	When I add a new property Name: 'WebApi' Value: 'http://localhost.com'
	Then the property Name: 'WebApi' Value: 'http://localhost.com' should exist

Scenario: testing
	Given the following application named: 'Test Application' and the configuration properties
	| Name    | Value             |
	| TestUri | http://google.com |
	| TestVal | abcd1234          |
	And the following environment named: 'Staging' and the configuration properties
	| Name    | Value                  |
	| TestUri | https://www.google.com |
	Then the application with the following environment 'Staging' has the following properties
	| Name    | Value                  |
	| TestUri | https://www.google.com |
	| TestVal | abcd1234               |

	