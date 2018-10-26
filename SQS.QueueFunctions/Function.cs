using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Amazon.Lambda.Core;

using Amazon.SQS;
using Amazon.SQS.Model;
using Newtonsoft.Json.Linq;

// Assembly attribute to enable the Lambda function's JSON input to be converted into a .NET class.
[assembly: LambdaSerializer(typeof(Amazon.Lambda.Serialization.Json.JsonSerializer))]

namespace SQS.QueueFunctions
{
    public class QueueMessageModel
    {
        public string QueueName { get; set; }
        public string Message { get; set; }
    }
    public class Function
    {
        /// <summary>
        /// A simple function that takes a string and does a ToUpper
        /// </summary>
        /// <param name="input"></param>
        /// <param name="context"></param>
        /// <returns></returns>
        public async Task<string> SendMessage(QueueMessageModel input, ILambdaContext context)
        {
            AmazonSQSClient client = new AmazonSQSClient();

            GetQueueUrlResponse urlResponse = await client.GetQueueUrlAsync(input.QueueName);

            SendMessageRequest request = new SendMessageRequest();
            request.MessageBody = input.Message;
            request.QueueUrl = urlResponse.QueueUrl;

            await client.SendMessageAsync(request);
            return "Message sent succesfully";
        }

        public async Task<string> ListQueues()
        {
            AmazonSQSClient client = new AmazonSQSClient();
            ListQueuesResponse response = await client.ListQueuesAsync(string.Empty);
            var queueList = string.Join("#", response.QueueUrls.Select(x=> x.Substring(x.LastIndexOf("/") + 1)));
            //"Compaints#inquairy"
            return queueList;
        }
    }
}
