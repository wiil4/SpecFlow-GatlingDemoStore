using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DemoStore_APITesting.src.specflow
{
    public interface IApiRequest
    {
        public void WhenISendARequest();
        public void ThenIExpectAValidCodeResponse();
    }
}
