Feature: ProductUpdate

Test for updating data
The operation on API is http://demostore.gatling.io/api/product/{id}

@functionality
@product-update
Scenario: ProductUpdate
	Given A valid API endpoint "http://demostore.gatling.io/"
	And An authorized user "admin" with password "admin"
	And A valid Id 17 of an existing product
	And A valid new product data with the following details
		| Name                 | Description          | Image                    | Price  | CategoryId |
		| Casual Brown Glasses | Casual Brown glasses | casual-brown-glasses.jpg | 999.99 | 7          |
	When I send a request
	Then I expect a valid code response
	And I expect the data of updated product
