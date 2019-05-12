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


Scenario: Removing existing Client configuration
	Given a Client with name Tech Fabric
	And a Client with name K&L Wines
	And a Client with name Cloud Fabric
	When I remove Client with name K&L Wines
	Then the Client K&L Wines is removed


Scenario: Removing non-existing Client configuration
	Given a Client with name Tech Fabric
	And a Client with name Cloud Fabric
	When I remove Client with name K&L Wines
	Then removal succeeds while Client K&L Wines is not actully removed

	
Scenario: Getting the list of all Client names
	Given a Client with name Tech Fabric
	And a Client with name Cloud Fabric
	And a Client with name ServiceWare
	When I get the list of Client names
	Then the following names should be returned
	| Name         |
	| Tech Fabric  |
	| Cloud Fabric |
	| ServiceWare  |
			

Scenario: List of all Client names doesn't contain removed Clients
	Given a Client with name Tech Fabric
	And a Client with name Cloud Fabric
	And a Client with name ServiceWare
	And a Client Cloud Fabric was removed
	When I get the list of Client names
	Then the following names should be returned
	| Name         |
	| Tech Fabric  |
	| ServiceWare  |