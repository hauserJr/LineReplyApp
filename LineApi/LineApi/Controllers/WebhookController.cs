using Line.Messaging;
using Line.Messaging.Webhooks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;

namespace LineApi.Controllers
{
    [Route("api")]
    public class WebhookController : ApiController
    {
        private static LineMessagingClient lineMessagingClient;
        private string AccessToken = "Your channel Token";
        private string ChannelSecret = "ChannelSecret";
        public WebhookController()
        {
            lineMessagingClient = new LineMessagingClient(AccessToken);
        }



        [Route("api/alive")]
        [HttpGet]
        public string Get()
        {
            return DateTime.Now.ToString();
        }

        [Route("api/lineapp/")]
        [HttpPost]
        public async Task<HttpResponseMessage> Post(HttpRequestMessage request)
        {
            var @events = await request.GetWebhookEventsAsync(ChannelSecret);

            var App = new LineBotApp(lineMessagingClient);
            await App.RunAsync(@events);
            return Request.CreateResponse(HttpStatusCode.OK);
        }
    }
}
