Feature: ProductCreation

Test for creating products with name, description, image path, price and categoryId
The operation on API is http://demostore.gatling.io/api/product

@functionality
@product-creation
Scenario: ProductCreation
	Given A valid API endpoint "http://demostore.gatling.io/"
	And An authorized user "admin" with password "admin"
	And A valid product with the following details
		| Name         | Description    | Image              | Price | CategoryId |
		| Purple Glass | Purple glasses | purple-glasses.jpg | 9.99  | 7          |
	When I send a request
	Then I expect a valid code response
	And I expect the information of created product
