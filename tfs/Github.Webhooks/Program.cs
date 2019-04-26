using Microsoft.Extensions.CommandLineUtils;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace Github.Webhooks
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Setting Github status");
            CommandLineApplication commandLineApplication = new CommandLineApplication(throwOnUnexpectedArg: false);
            
            CommandOption state = commandLineApplication.Option(
               "-s |--state <state>",
               "TFS Build state.", CommandOptionType.SingleValue);
            CommandOption buildId = commandLineApplication.Option(
                "-b |--buildid <buildid>",
                "TFS Build Id.", CommandOptionType.SingleValue);
            CommandOption sha = commandLineApplication.Option(
                "-sha |--sha <sha>",
                "Github sha.", CommandOptionType.SingleValue);
            
            
            commandLineApplication.HelpOption("-? | -h | --help");

            commandLineApplication.OnExecute(() =>
            {
                ServicePointManager.SecurityProtocol = 
                                SecurityProtocolType.Ssl3 | 
                                SecurityProtocolType.Tls | 
                                SecurityProtocolType.Tls11 | 
                                SecurityProtocolType.Tls12;

                Console.WriteLine("Build Id = " + buildId.Value());
                Console.WriteLine("State = " + state.Value());
                Console.WriteLine("Sha = " + sha.Value());

                HttpClient client = new HttpClient();
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("token", "56881bd4f3440c06af865747539426147a458cc9");
                client.DefaultRequestHeaders.Add("User-Agent", "TFS Status publisher");
                State st = new State()
                {
                    state = state.Value(),
                    target_url = "http://168.62.57.106:8080/tfs/Projects/Olimp2019/_build?_a=summary&buildId=" + buildId.Value(),
                    description = "The build is running",
                    context = "continuous-integration/tfs"
                };
                
                HttpContent content = new StringContent(JsonConvert.SerializeObject(st), 
                                                                Encoding.UTF8, "application/json");
                Console.WriteLine("Github webhook ready");
                HttpRequestMessage message = new HttpRequestMessage(HttpMethod.Post, 
                        "https://api.github.com/repos/ddrakonn/olimp/statuses/" + sha.Value());
                message.Content = content;
                
                var result = client.SendAsync(message).GetAwaiter().GetResult();
                Console.WriteLine("Github webhook  - " + result.StatusCode.ToString());
                var str = result.Content.ReadAsStringAsync().GetAwaiter().GetResult();
                return 0;
            });
            commandLineApplication.Execute(args);

            Console.WriteLine("Github status'comleted");

          }
    }

    public class  State
    {
        public string state { get; set; }
        public string target_url { get; set; }
        public string description { get; set; }
        public string context { get; set; }
    }
}
