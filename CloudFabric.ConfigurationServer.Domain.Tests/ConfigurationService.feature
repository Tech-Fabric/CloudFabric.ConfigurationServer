Feature: ConfigurationService
	In order to avoid silly mistakes
	As a math idiot
	I want to be told the sum of two numbers

@mytag
Scenario: Get configuration properties
	Given there are the following properties configured
	| Name     | Value  |
	| testval1 | value  |
	| WebApi   | value2 |
	When I get configuration properties
	Then the configuration transform result should be
	| Name     | Value  |
	| testval1 | value  |
	| WebApi   | value2 |

Scenario: Add configuration property conflict
	Given there are the following properties configured
    | Name     | Value    |
    | testval1 | testval2 |
	When I add a configuration property Name: 'testval1' Value: 'testing'
	Then there should be an exception thrown 'ConfigurationPropertyAlreadyExistsException'

Scenario: Add configuration
	Given there are the following properties configured
    | Name     | Value |
    | testval1 | 431   |
	When I add the configuration property Name: 'testval2' Value: 'testing'
	Then the configuration transform result should be
	| Name     | Value   |
	| testval1 | 431     |
	| testval2 | testing |

#
#	Start of adding environment settings to root
#
Scenario: One environment in root
	Given there are the following properties configured
    | Name     | Value |
    | testval1 | 123   |
    | testval2 | asdf  |
	And there is an environment configuration Name: 'Staging' with the properties
	| Name     | Value |
	| testval1 | abc   |
	When I get configuration properties with environment Name: 'Staging'
	Then the configuration transform result should be
    | Name     | Value |
    | testval1 | abc   |
    | testval2 | asdf  |

Scenario: One environment in root new variable exists in environment
	Given there are the following properties configured
    | Name     | Value |
    | testval1 | 123   |
	And there is an environment configuration Name: 'Staging' with the properties
	| Name     | Value |
	| testval1 | abc   |
	| testval2 | asdf  |
	When I get configuration properties with environment Name: 'Staging'
	Then the configuration transform result should be
    | Name     | Value |
    | testval1 | abc   |
    | testval2 | asdf  |

Scenario: Two environments in root
	Given there are the following properties configured
    | Name     | Value |
    | testval1 | 123   |
    | testval2 | asdf  |
	And there is an environment configuration Name: 'Staging' with the properties
    | Name     | Value |
    | testval1 | abc   |
	And there is an environment configuration Name: 'Production' with the properties
	| Name     | Value |
	| testval1 | fds   |
	When I get configuration properties with environment Name: 'Staging'
	Then the configuration transform result should be
    | Name     | Value |
    | testval1 | abc   |
    | testval2 | asdf  |

Scenario: Fetched environment doesn't exist
	Given there are the following properties configured
    | Name     | Value |
    | testval1 | 123   |
	And there is an environment configuration Name: 'Staging' with the properties
    | Name     | Value |
    | testval1 | 321   |
    | testval2 | fds   |
	When I get configuration properties with environment Name: 'Production'
	Then the configuration transform result should be
    | Name     | Value |
    | testval1 | 123   |

Scenario: add duplicate environment throws exception
	Given there are the following properties configured
	| Name     | Value |
	| testval1 | 123   |
	And there is an environment configuration Name: 'Staging' with the properties
    | Name     | Value |
    | testval1 | 123   |
	When I add an environment Name: 'Staging' with the properties
	| Name     | Value |
	| testval2 | fds   |
	Then there should be an exception thrown 'EnvironmentConfigurationAlreadyExistsException'


# Start of having applications in the mix
Scenario: one application in root
	Given there are the following properties configured
    | Name     | Value |
    | testval1 | 123   |
	And there is an application configuration Name: 'Web' with the properties
	| Name     | Value |
	| testval1 | 321   |
	| testval2 | fds   |
	When I get configuration properties with application Name: 'Web'
	Then the configuration transform result should be
    | Name     | Value |
    | testval1 | 321   |
    | testval2 | fds   |

Scenario: one application in root but fetch wrong name
	Given there are the following properties configured
    | Name     | Value |
    | testval1 | 123   |
	And there is an application configuration Name: 'Web' with the properties
	| Name     | Value |
	| testval1 | 321   |
	| testval2 | fds   |
	When I get configuration properties with application Name: 'Webs'
	Then the configuration transform result should be
    | Name     | Value |
    | testval1 | 123   |

Scenario: one application and environment in root
	Given there are the following properties configured
    | Name     | Value |
    | testval1 | 123   |
	And there is an environment configuration Name: 'Staging' with the properties
    | Name     | Value |
    | testval1 | adsf  |
	And there is an application configuration Name: 'Web' with the properties
	| Name     | Value |
	| testval1 | 321   |
	| testval2 | fds   |
	When I get configuration properties with application Name: 'Web' and environment Name: 'Staging'
	Then the configuration transform result should be
    | Name     | Value |
    | testval1 | 321   |
    | testval2 | fds   |

Scenario: one application and environment in root
	Given there are the following properties configured
    | Name     | Value |
    | testval1 | 123   |
    | testval2 | a     |
	And there is an environment configuration Name: 'Staging' with the properties
    | Name     | Value |
    | testval1 | adsf  |
	And there is an application configuration Name: 'Web' with the properties
	| Name     | Value |
	| testval1 | 321   |
	| testval3 | fds   |
	When I get configuration properties with application Name: 'Web' and environment Name: 'Staging'
	Then the configuration transform result should be
    | Name     | Value |
    | testval1 | 321   |
    | testval2 | a     |
    | testval3 | fds   |
