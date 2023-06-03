using DemoStore_APITesting.src.models;
using Newtonsoft.Json.Linq;
using RestSharp;
using System;
using System.Net;
using TechTalk.SpecFlow;
using TechTalk.SpecFlow.Assist;

namespace DemoStore_APITesting.src.specflow
{
    [Binding]
    [Scope(Tag = "product-creation")]
    public class ProductCreationStepDefinitions : ProductsAPICommon, IApiRequest
    {
        private string token = string.Empty;
        private Product product;

        [Given(@"An authorized user ""([^""]*)"" with password ""([^""]*)""")]
        public void GivenAnAuthorizedUserWithPassword(string user, string password)
        {
            var auth = Authenticate(user, password);
            token = JObject.Parse(auth.Content).SelectToken("token").ToString();
        }

        [Given(@"A valid product with the following details")]
        public void GivenAValidProductWithTheFollowingDetails(Table table)
        {
            product = table.CreateInstance<Product>();
        }

        [When(@"I send a request")]
        public void WhenISendARequest()
        {
            request = new RestRequest("api/product", Method.Post);
            request.AddHeader("Authorization", $"Bearer {token}");
            request.AddJsonBody(product);
            response = client.Execute(request);
        }

        [Then(@"I expect a valid code response")]
        public void ThenIExpectAValidCodeResponse()
        {
            Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.OK));
        }

        [Then(@"I expect the information of created product")]
        public void ThenIExpectTheInformationOfCreatedProduct()
        {
            var createdProductResponse = JObject.Parse(response.Content);
            Assert.That(createdProductResponse.SelectToken("name").ToString(), Is.EqualTo(product.Name), "Created product name is not the same as the initial parameter");
        }
    }
}
