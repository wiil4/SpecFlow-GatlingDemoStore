using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DemoStore_APITesting.src.test.PreviousDefinedTest
{
    public class BaseTest
    {
        protected RestClient client;

        [SetUp]
        public void SetUp()
        {
            client = new RestClient("http://demostore.gatling.io/");
        }
    }
}
