Feature: Configuration

Scenario: Adding new Client configuration
	When I add a new Client with name K&L Wines
	Then the Client configuration for K&L Wines is created


@ExpectException	
Scenario: Adding duplicated Client configuration
	Given a Client with name K&L Wines
	When I add a new Client with name K&L Wines
	Then adding fails as Client with name K&L Wines already exists

	
Scenario: Adding new Client configuration with unique name
	Given a Client with name Tech Fabric
	And a Client with name Cloud Fabric
	And a Client with name ServiceWare
	When I add a new Client with name K&L Wines
	Then the Client configuration for K&L Wines is created