Feature: GetProductById

This operation should return a product based on the input id
The operation on API is http://demostore.gatling.io/api/product/{id}

@functionality
@get-product-by-id
Scenario: GetProductById
	Given A valid API endpoint "http://demostore.gatling.io/"
	And I have a product Id 4
	When I send a request
	Then I expect a valid code response
	And The product for the given Id
