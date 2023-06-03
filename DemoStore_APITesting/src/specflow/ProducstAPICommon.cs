using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TechTalk.SpecFlow;

namespace DemoStore_APITesting.src.specflow
{
    [Binding]
    public class ProductsAPICommon
    {
        protected RestClient client;
        protected RestRequest request;
        protected RestResponse response;

        [Given(@"A valid API endpoint ""([^""]*)""")]
        public void GivenAValidAPIEndpoint(string p0)
        {
            client = new RestClient(p0);
        }

        protected RestResponse Authenticate(string user, string pwd)
        {
            request = new RestRequest("api/authenticate", Method.Post);
            request.AddJsonBody(new { username = user, password = pwd });
            return client.Execute(request);
        }
    }
}
