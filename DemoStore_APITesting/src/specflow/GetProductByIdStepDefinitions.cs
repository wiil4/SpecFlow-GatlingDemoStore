using Newtonsoft.Json.Linq;
using RestSharp;
using System;
using System.Net;
using TechTalk.SpecFlow;

namespace DemoStore_APITesting.src.specflow
{
    [Binding]
    [Scope(Tag = "get-product-by-id")]
    public class GetProductByIdStepDefinitions : ProductsAPICommon, IApiRequest
    {

        [Given(@"I have a product Id (.*)")]
        public void GivenIHaveAProductId(int p0)
        {
            request = new RestRequest("api/product/{id}", Method.Get);
            request.AddUrlSegment("id", 20);
        }

        [When(@"I send a request")]
        public void WhenISendARequest()
        {
            response = client.ExecuteGet(request);
        }

        [Then(@"I expect a valid code response")]
        public void ThenIExpectAValidCodeResponse()
        {
            Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.OK));
        }

        [Then(@"The product for the given Id")]
        public void ThenTheProductForTheGivenId()
        {
            var product = JObject.Parse(response.Content);
            string imageName = product.SelectToken("image").ToString();
            Assert.That(imageName, Is.EqualTo("casual-blue-open.jpg"), "Image name does not correspond to specified product");
        }
    }
}
