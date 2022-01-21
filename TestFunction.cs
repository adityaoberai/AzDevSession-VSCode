using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace Company.Function
{
    public static class TestFunction
    {
        [FunctionName("TestFunction")]
        public static async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Anonymous, "get", "post", Route = null)] HttpRequest req,
            ILogger log)
        {
            log.LogInformation("C# HTTP trigger function processed a request.");

            string firstNameGet = req.Query["firstName"];
            string lastNameGet = req.Query["lastName"];

            string nameGet = firstNameGet + lastNameGet;

            string requestBody = await new StreamReader(req.Body).ReadToEndAsync();
            dynamic data = JsonConvert.DeserializeObject(requestBody);
            
            string firstNamePost = data?.firstNamePost;
            string lastNamePost = data?.lastNamePost;

            string namePost = firstNamePost + lastNamePost;

            string responseMessage = string.IsNullOrEmpty(nameGet+namePost)
                ? "Enter Name"
                : $"Get Name: {nameGet}\n\nPost Name: {namePost}";

            return new OkObjectResult(new {response = responseMessage});
        }
    }
}
