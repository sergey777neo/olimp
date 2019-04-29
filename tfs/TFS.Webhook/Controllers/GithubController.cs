using log4net;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Web.Http;


namespace TFS.Webhook.Controllers
{
    [AllowAnonymous]
    public class GithubController : ApiController
    {
        // GET api/values
        public HttpResponseMessage Post([FromBody]JObject payload)
        {

            dynamic data = payload as dynamic;

            ILog log = log4net.LogManager.GetLogger(typeof(GithubController));
            log.Error(payload.ToString());
                HttpClient client = new HttpClient();
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("token", ConfigurationManager.AppSettings["token"].ToString());
            client.DefaultRequestHeaders.Add("User-Agent", "TFS Status publisher");

            var gitState = string.Empty;
            var tfsState = data.resource.status.ToString();
            if (tfsState == "succeeded")
                gitState = "success";
            else gitState = "failure";

            State st = new State()
            {
                state = gitState,
                target_url = "http://168.62.57.106:8080/tfs/Projects/Olimp2019/_build?_a=summary&buildId=" + data.resource.id.ToString(),
                description = data.message.text.ToString(),
                context = "continuous-integration/tfs"
            };

            HttpContent content = new StringContent(JsonConvert.SerializeObject(st),
                                                            Encoding.UTF8, "application/json");
            Console.WriteLine("Github webhook ready");

            var revision = data.resource.sourceGetVersion.ToString();

            HttpRequestMessage message = new HttpRequestMessage(HttpMethod.Post,
                    "https://api.github.com/repos/ddrakonn/olimp/statuses/" + revision);
            message.Content = content;

            var result = client.SendAsync(message).GetAwaiter().GetResult();
            Console.WriteLine("Github webhook  - " + result.StatusCode.ToString());
            var str = result.Content.ReadAsStringAsync().GetAwaiter().GetResult();

            return result;
        }

        //[ActionName("pushpr")]
        //public HttpResponseMessage PostToTfs([FromBody]JObject payload)
        //{
        //    dynamic data = payload as dynamic;
        //}
    }

    public class State
    {
        public string state { get; set; }
        public string target_url { get; set; }
        public string description { get; set; }
        public string context { get; set; }
    }
}
