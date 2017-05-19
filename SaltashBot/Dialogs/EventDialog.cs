using Microsoft.Bot.Builder.Dialogs;
using Microsoft.Bot.Connector;
using Newtonsoft.Json;
using SaltashBot.Models;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace SaltashBot.Dialogs
{
    [Serializable]
    public class EventDialog : IDialog<object>
    {
        private int attempts = 3;

        public async Task StartAsync(IDialogContext context)
        {
            await context.PostAsync("Hi, I'm the Saltash event bot. Are you looking for events?");

            context.Wait(MessageReceivedAsync);
        }

        public async Task MessageReceivedAsync(IDialogContext context, IAwaitable<IMessageActivity> result)
        {
            var message = await result;

            /* If the message returned is a valid name, return it to the calling dialog. */
            if ((message.Text != null) && (message.Text.Trim().Length > 0))
            {
                var reply = context.MakeMessage();
                /* Completes the dialog, removes it from the dialog stack, and returns the result to the parent/calling
                    dialog. */
                IList<Event> events = GetSaltashEvents();
                reply.Attachments = new List<Attachment>();
                reply.AttachmentLayout = AttachmentLayoutTypes.Carousel;
                int counter = 1;

                //context.
                foreach (Event evt in events)
                {
                    reply.Attachments.Add(
                        new HeroCard
                        {
                            Title = evt.Title,
                            Text = string.Format("Starts {0} and ends {1}", evt.StartDate.Value.ToString("F"), evt.EndDate.Value.ToString("F"))
                        }
                        .ToAttachment()
                        );

                    if(counter == 5)
                    {
                        break;
                    }

                    counter++;

                }
                await context.PostAsync(reply);
                context.Done(reply);
            }
            /* Else, try again by re-prompting the user. */
            else
            {
                --attempts;
                if (attempts > 0)
                {
                    await context.PostAsync("I'm sorry, I don't understand your reply. Are you looking for events?");
                    context.Wait(this.MessageReceivedAsync);
                }
                else
                {
                    /* Fails the current dialog, removes it from the dialog stack, and returns the exception to the 
                        parent/calling dialog. */
                    context.Fail(new TooManyAttemptsException("Message was not a string or was an empty string."));
                }
            }
        }

        private IList<Event> GetSaltashEvents()
        {
            return GetSaltashEvents(null, null);

        }

        private IList<Event> GetSaltashEvents(DateTime? startDate, DateTime? endDate)
        {
            string url = "http://www.saltash.website/api/calendar";
            string calendarResponse;
            using (HttpClient client = new HttpClient())
            {
                IList<KeyValuePair<string, string>> parameters = new List<KeyValuePair<string, string>>();

                if (startDate.HasValue)
                {
                    parameters.Add(new KeyValuePair<string, string>("StartDate", startDate.Value.ToString("yyyy-MM-ddTHH:mm:ss")));
                }

                if (endDate.HasValue)
                {
                    parameters.Add(new KeyValuePair<string, string>("EndDate", endDate.Value.ToString("yyyy-MM-ddTHH:mm:ss")));
                }

                var content = new FormUrlEncodedContent(parameters);
                calendarResponse = client.PostAsync(url, content).Result.Content.ReadAsStringAsync().Result;
            }

            return JsonConvert.DeserializeObject<IList<Event>>(calendarResponse);
        }
    }
}