using System;
using System.Threading.Tasks;
using System.Web.Http;

using Microsoft.Bot.Connector;
using Microsoft.Bot.Builder.Dialogs;
using System.Web.Http.Description;
using System.Net.Http;
using System.Diagnostics;
using System.Linq;
using AdaptiveCards;
using System.Collections.Generic;

namespace Microsoft.Bot.Sample.LuisBot
{
    [BotAuthentication]
    public class MessagesController : ApiController
    {
        /// <summary>
        /// POST: api/Messages
        /// receive a message from a user and send replies
        /// </summary>
        /// <param name="activity"></param>
        [ResponseType(typeof(void))]
        public virtual async Task<HttpResponseMessage> Post([FromBody] Activity activity)
        {
            // check if activity is of type message
            if (activity.GetActivityType() == ActivityTypes.Message)
            {
                await Conversation.SendAsync(activity, () => new BasicLuisDialog());
            }
            else
            {
                HandleSystemMessage(activity);
            }
            return new HttpResponseMessage(System.Net.HttpStatusCode.Accepted);
        }

        private Activity HandleSystemMessage(Activity message)
        {
            if (message.Type == ActivityTypes.DeleteUserData)
            {
                // Implement user deletion here
                // If we handle user deletion, return a real message
            }
            else if (message.Type == ActivityTypes.ConversationUpdate)
            {
                if (message.MembersAdded.Any(o => o.Id == message.Recipient.Id))
                {
                    ConnectorClient connector = new ConnectorClient(new Uri(message.ServiceUrl));
                    Activity replyToConversation = message.CreateReply("�Bienvenido a Ask Bot Upecino!, Para comenzar, debes iniciar sesi�n o registrarte");
                    AdaptiveCard card = new AdaptiveCard()
                    {
                        // Buttons
                        Actions = new List<ActionBase>() {
                            new ShowCardAction()
                            {
                                Title = "Iniciar Sesi�n",
                                Speak = "<s>Iniciar Sesi�n</s>",
                                Card = new AdaptiveCard()
                                {
                                    Body = new List<CardElement>()
                                    {
                                        new TextBlock()
                                        {
                                            Text = "Iniciar Sesi�n",
                                            Speak = "<s>test</s>",
                                            Weight = TextWeight.Bolder
                                        }
                                    }
                                }
                            },
                            new ShowCardAction()
                            {
                                Title = "Reg�strate",
                                Speak = "<s>Reg�strate</s>",
                                Card = new AdaptiveCard()
                                {
                                    Body = new List<CardElement>()
                                    {
                                        new TextBlock()
                                        {
                                            Text = "Reg�strate",
                                            Speak = "<s>test</s>",
                                            Weight = TextWeight.Bolder
                                        }
                                    }
                                }
                            }
                        }
                    };

                    Attachment attachment = new Attachment()
                    {
                        ContentType = AdaptiveCard.ContentType,
                        Content = card
                    };
                    replyToConversation.Attachments.Add(attachment);
                    var reply = connector.Conversations.SendToConversationAsync(replyToConversation);
                }
                // Handle conversation state changes, like members being added and removed
                // Use Activity.MembersAdded and Activity.MembersRemoved and Activity.Action for info
                // Not available in all channels
            }
            else if (message.Type == ActivityTypes.ContactRelationUpdate)
            {
                // Handle add/remove from contact lists
                // Activity.From + Activity.Action represent what happened
            }
            else if (message.Type == ActivityTypes.Typing)
            {
                // Handle knowing tha the user is typing
            }
            else if (message.Type == ActivityTypes.Ping)
            {
            }

            return null;
        }
    }
}