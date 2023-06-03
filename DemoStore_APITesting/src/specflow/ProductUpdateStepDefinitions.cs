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
    [Scope(Tag = "product-update")]
    public class ProductUpdateStepDefinitions : ProductsAPICommon, IApiRequest
    {
        private string token = string.Empty;
        private Product product;

        [Given(@"An authorized user ""([^""]*)"" with password ""([^""]*)""")]
        public void GivenAnAuthorizedUserWithPassword(string user, string password)
        {
            var auth = Authenticate(user, password);
            token = JObject.Parse(auth.Content).SelectToken("token").ToString();
        }

        [Given(@"A valid Id (.*) of an existing product")]
        public void GivenAValidIdOfAnExistingProduct(int id)
        {
            request = new RestRequest("api/product/{id}", Method.Put);
            request.AddUrlSegment("id", id);
        }

        [Given(@"A valid new product data with the following details")]
        public void GivenAValidNewProductDataWithTheFollowingDetails(Table table)
        {
            product = table.CreateInstance<Product>();
        }

        [When(@"I send a request")]
        public void WhenISendARequest()
        {
            request.AddHeader("Authorization", $"Bearer {token}");
            request.AddJsonBody(product);
            response = client.Execute(request);
        }

        [Then(@"I expect a valid code response")]
        public void ThenIExpectAValidCodeResponse()
        {
            Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.OK));
        }

        [Then(@"I expect the data of updated product")]
        public void ThenIExpectTheDataOfUpdatedProduct()
        {
            var updatedProductResponse = JObject.Parse(response.Content);
            Assert.That(updatedProductResponse.SelectToken("name").ToString(), Is.EqualTo(product.Name), "Updated product name is not the same as the initial parameter");
        }
    }
}
