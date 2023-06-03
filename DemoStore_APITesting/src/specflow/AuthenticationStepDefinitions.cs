using Newtonsoft.Json.Linq;
using RestSharp;
using System;
using System.Net;
using TechTalk.SpecFlow;

namespace DemoStore_APITesting.src.specflow
{
    [Binding]
    [Scope(Tag = "authentication")]
    public class AuthenticationStepDefinitions : ProductsAPICommon, IApiRequest
    {
        private string _username = string.Empty;
        private string _password = string.Empty;

        [Given(@"I have a valid username ""([^""]*)"" and password ""([^""]*)""")]
        public void GivenIHaveAValidUsernameAndPassword(string username, string password)
        {
            _username = username;
            _password = password;
        }

        [When(@"I send a request")]
        public void WhenISendARequest()
        {
            response = Authenticate(_username, _password);
        }

        [Then(@"I expect a valid code response")]
        public void ThenIExpectAValidCodeResponse()
        {
            Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.OK));
        }

        [Then(@"I expect a security token")]
        public void ThenIExpectASecurityToken()
        {
            var token = JObject.Parse(response.Content);
            Assert.That(token.SelectToken("token").ToString(), Is.Not.Empty, "Token has not been provided by the server");
        }
    }
}
