using Microsoft.Bot.Builder.Dialogs;
using Microsoft.Bot.Connector;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;

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
                /* Completes the dialog, removes it from the dialog stack, and returns the result to the parent/calling
                    dialog. */
                var url = "https://www.saltash.website/api/apiaicalendar";
                var httpClient = new HttpClient();
                HttpResponseMessage response = await httpClient.PostAsync(url, null);

                Mausty mausty = await response.Content.ReadAsAsync<Mausty>();
                //var message = await argument;
                await context.PostAsync("I found: " + mausty.displayText);
                context.Done(mausty.displayText);
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
    }

    public class Mausty
    {
        public string speech;

        public string displayText;

        public string source;
    }
}