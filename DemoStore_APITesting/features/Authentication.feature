Feature: Authentication

Test for authentication expecting a token
The operation on API is http://demostore.gatling.io/api/authenticate

@functionality
@authentication
Scenario: Authentication
	Given A valid API endpoint "http://demostore.gatling.io/"
	And I have a valid username "admin" and password "admin"
	When I send a request
	Then I expect a valid code response
	And I expect a security token
