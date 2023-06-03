Feature: GetProducts

This operation should return a list of products
The operation on API is http://demostore.gatling.io/api/product

@functionality
@get-all-products
Scenario: GetAllProducts
	Given A valid API endpoint "http://demostore.gatling.io/"
	When I send a request
	Then I expect a valid code response
	And the response should contain a list of products
