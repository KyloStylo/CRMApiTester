using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace CRMApiTester
{
    class Program
    {
        static void Main(string[] args)
        {
            MainAsync().Wait();
        }

        static async Task<int> MainAsync()
        {
            //New Chat
            BotChat chat = new BotChat("From User", "Message", "Channel", DateTime.Now);
            //Existing Chat
            BotChat chat2 = new BotChat("From User", "Message", "Channel", DateTime.Now, chatID: new Guid());
            //Existing Chat with Identified User
            BotChat chat3 = new BotChat("From User", "Message", "Channel", DateTime.Now, chatID: new Guid(), regarding: new Guid());

            HttpClient cons = new HttpClient();
            cons.BaseAddress = new Uri("http://localhost:56227/");
            cons.DefaultRequestHeaders.Accept.Clear();
            cons.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));

            using (cons)
            {
                var content = new StringContent(JsonConvert.SerializeObject(chat), Encoding.UTF8, "application/json");
                HttpResponseMessage res;

                try
                {
                    res = await cons.PostAsJsonAsync("CRM/CreateBotChat", chat);
                }
                catch (Exception e)
                {

                    throw e;
                }

                return 42;
            }
        }
    }
}
