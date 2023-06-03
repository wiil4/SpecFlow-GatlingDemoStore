using Newtonsoft.Json.Linq;
using RestSharp;
using System;
using System.Net;
using TechTalk.SpecFlow;

namespace DemoStore_APITesting.src.specflow
{
    [Binding]
    [Scope(Tag = "get-all-products")]
    public class GetProductsStepDefinitions : ProductsAPICommon, IApiRequest
    {

        [When(@"I send a request")]
        public void WhenISendARequest()
        {
            request = new RestRequest("api/product", Method.Get);
            response = client.ExecuteGet(request);
        }

        [Then(@"I expect a valid code response")]
        public void ThenIExpectAValidCodeResponse()
        {
            Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.OK));
        }

        [Then(@"the response should contain a list of products")]
        public void ThenTheResponseShouldContainAListOfProducts()
        {
            var productList = JArray.Parse(response.Content);
            Assert.NotNull(productList, "Empty list of products");
            foreach (var product in productList)
            {
                Assert.That(product.SelectToken("name").ToString(), Is.Not.Empty, $"Product has an Empty name");
            }
        }
    }
}
