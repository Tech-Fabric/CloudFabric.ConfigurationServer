Feature: Application Configuration

@mytag
Scenario: Blank State
	Given a new instance of an application is created
	When get the number of properties
	Then the result should be 0

Scenario: Add Property
	Given a new instance of an application is created
	When I add a new property Name: "WebApi" Value: "http://localhost.com"
	Then