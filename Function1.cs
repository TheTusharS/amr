using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using RestSharp;

namespace FunctionApp1
{
    public static class Function1
    {
        [FunctionName("Function1")]
        public static  ActionResult Run(
            [HttpTrigger(AuthorizationLevel.Function, "get", "post", Route = null)] HttpRequest req,
            ILogger log)
        {
            log.LogInformation("C# HTTP trigger function processed a request.");

            string message = req.Query["text"];
            string channel = req.Query["channel"];
            try
            {
                var client = new RestClient("https://slack.com/api/chat.postMessage");
                var request = new RestRequest(Method.POST);

                request.AddHeader("Authorization", "Bearer xoxp-715816215412-715816216372-716316350085-c997cad930cdd7a304368222250cc514");
                request.AddHeader("Content-Type", "application/json");
                request.AddParameter("undefined", "{\n\t\n\t\"text\":\"Hi Eveyone\",\n\t\"channel\":\"#general\"\n\t\n}", ParameterType.RequestBody);
                IRestResponse response = client.Execute(request);
                return response != null
               ? (ActionResult)new OkObjectResult(response.Content)
               : new BadRequestObjectResult("Error");

            }
            catch
            {
                return new BadRequestObjectResult("Error");
            }
           
        }
    }
}
