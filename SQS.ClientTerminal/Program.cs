using Newtonsoft.Json;
using System;
using System.Net;

namespace SQS.ClientTerminal
{
    public class QueueMessageModel
    {
        public string QueueName { get; set; }
        public string Message { get; set; }
    }
    class Program
    {
        static void Main(string[] args)
        {
            using (WebClient client = new WebClient())
            {
                //list queues
                var result = client.UploadString("https://iems98dbo0.execute-api.us-east-2.amazonaws.com/dev/", "");
                var queues = result.Replace("\"","").Split(new char[] { '#' },StringSplitOptions.RemoveEmptyEntries);
                foreach (var name in queues)
                {
                    Console.WriteLine(name);
                }
            }
            ///////////////////////////////////////////
            using (WebClient client = new WebClient())
            {
                var model = new QueueMessageModel() { QueueName="Complaint", Message="Please fix this bla bla..." };
                var modelSerialized = JsonConvert.SerializeObject(model);
                var result = client.UploadString("https://0zb5x56ho7.execute-api.us-east-2.amazonaws.com/dev/", modelSerialized);
                Console.WriteLine(result);
            }

            Console.ReadLine();
        }
    }
}
