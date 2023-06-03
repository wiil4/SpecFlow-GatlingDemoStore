using DemoStore_APITesting.src.models;
using Newtonsoft.Json.Linq;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace DemoStore_APITesting.src.test.PreviousDefinedTest
{
    public class GatlingDSTest : BaseTest
    {
        RestRequest request;
        RestResponse response;

        [Test]
        public void GetAllProductsTest()
        {
            request = new RestRequest("api/product", Method.Get);
            response = client.ExecuteGet(request);
            Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.OK));

            var productList = JArray.Parse(response.Content);
            Assert.NotNull(productList);
            foreach (var product in productList)
            {
                Assert.That(product.SelectToken("name").ToString(), Is.Not.Empty);
            }
        }

        [Test]
        public void GetSpecificProductTest()
        {
            request = new RestRequest("api/product/{id}", Method.Get);
            request.AddUrlSegment("id", 20);
            response = client.ExecuteGet(request);
            Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.OK));

            var product = JObject.Parse(response.Content);
            string imageName = product.SelectToken("image").ToString();
            Assert.That(imageName, Is.EqualTo("casual-blue-open.jpg"), "Image name does not correspond to specified product");
        }

        [Test]
        public void AuthenticateTest()
        {
            response = Authenticate();
            Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.OK));
        }

        [Test]
        public void CreateProductTest()
        {
            var auth = Authenticate();
            var token = JObject.Parse(auth.Content).SelectToken("token").ToString();
            var product = new Product();
            product.Name = "Purple Glass";
            product.Description = "Purple glasses";
            product.Image = "purple-glasses.jpg";
            product.Price = "9.99";
            product.CategoryId = 7;

            request = new RestRequest("api/product", Method.Post);
            request.AddHeader("Authorization", $"Bearer {token}");
            request.AddJsonBody(product);
            response = client.Execute(request);
            Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.OK));
            var createdProductResponse = JObject.Parse(response.Content);
            Assert.That(createdProductResponse.SelectToken("name").ToString(), Is.EqualTo(product.Name), "Created product name is not the same as the initial parameter");
        }

        [Test]
        public void UpdateProductTest()
        {
            var auth = Authenticate();
            var token = JObject.Parse(auth.Content).SelectToken("token").ToString();

            var product = new Product();
            product.Name = "Casual Brown Glasses";
            product.Description = "Casual Brown glasses";
            product.Image = "asual-brown-glasses.jpg";
            product.Price = "999.99";
            product.CategoryId = 7;

            request = new RestRequest("api/product/{id}", Method.Put);
            request.AddUrlSegment("id", 19);
            request.AddHeader("Authorization", $"Bearer {token}");
            request.AddJsonBody(product);

            response = client.Execute(request);
            Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.OK));
            var updatedProductResponse = JObject.Parse(response.Content);
            Assert.That(updatedProductResponse.SelectToken("name").ToString(), Is.EqualTo(product.Name), "Updated product name is not the same as the initial parameter");
        }
        private RestResponse Authenticate()
        {
            request = new RestRequest("api/authenticate", Method.Post);
            request.AddJsonBody(new { username = "admin", password = "admin" });
            return client.Execute(request);
        }
    }
}
