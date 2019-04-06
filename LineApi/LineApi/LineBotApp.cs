using Line.Messaging;
using Line.Messaging.Webhooks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace LineApi
{
    internal class LineBotApp : WebhookApplication
    {
        private LineMessagingClient MessagingClient { get; }

        public LineBotApp(LineMessagingClient _LineMessagingClient)
        {
            this.MessagingClient = _LineMessagingClient;
        }

        protected override async Task OnMessageAsync(MessageEvent ev)
        {
            switch (ev.Message.Type)
            {
                case EventMessageType.Text:
                    await HandleTextAsync(ev.ReplyToken, ((TextEventMessage)ev.Message).Text, ev.Source.UserId);
                    break;

                case EventMessageType.Image:
                case EventMessageType.Audio:
                case EventMessageType.Video:
                case EventMessageType.File:
                case EventMessageType.Location:
                case EventMessageType.Sticker:
                    break;
            }
        }


        private async Task HandleTextAsync(string ReplyToken, string UserMessage, string UserId)
        {
            List<ISendMessage> IReplyMessage = new List<ISendMessage>();
            var Hellosticks = new StickerMessage[] {
                            new StickerMessage("1", "2"), new StickerMessage("1", "134"), new StickerMessage("2", "143") ,
                            new StickerMessage("2", "34"), new StickerMessage("1", "407"), new StickerMessage("1", "106")
                        };

            var ReplyMessage = new TextMessage($"這是你的UserId ： {UserId}");
            var Sticks = Hellosticks[new Random().Next(Hellosticks.Length - 1)];


            IReplyMessage.Add(ReplyMessage);
            IReplyMessage.Add(Sticks);

            await MessagingClient.ReplyMessageAsync(ReplyToken, IReplyMessage);
        }
    }
}