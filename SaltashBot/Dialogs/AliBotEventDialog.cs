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
    public class AliBotEventDialog : IDialog<object>
    {
        private int attempts = 0;
        private int maxCards = 5;
        private Random random;
        private Dictionary<string, string> aliisms;

        public AliBotEventDialog()
        {
            this.random = new Random();
            this.aliisms = new Dictionary<string, string>();
            aliisms.Add("squirt", "The method of moving an item from one place to another.");
            aliisms.Add("boatload","A large quantity of items.....\'Take a boatload of requirements, produce some clag and squirt it up to TEST\'");
            aliisms.Add("nectar", "A drink, typically involving alchol.");
            aliisms.Add("agent ballsac", "A senior member of staff who is a Helm in nature");
            aliisms.Add("helm", "Describing a person who is not to the sayers taste");
            aliisms.Add("ship", "The process of moving an item from one place to another.  Also can mean a deployment.");
            aliisms.Add("rig", "Typically used when describing an environment, i.e. \"My development rig\" meaning my development environment");
            aliisms.Add("blitz", "The process of working on an item at pace");
            aliisms.Add("snag", "The process of massaging an implementation to meet a clients requirements");
            aliisms.Add("shoot up the pipe", "To deploy something into a test/live environment");
            aliisms.Add("sniff", "The process of reading data from an external source");
            aliisms.Add("buckz", "buuuuuuuuuuuuuuuuuuucks");
            aliisms.Add("smeg", "Same as Helm, but of a more friendly nature.");
            aliisms.Add("juggernaut", "The coffee filter.");
            aliisms.Add("cruff", "Mess/unnecessary items....\"I'll take all of that cruff out before I squirt to TEST\"");
        }


        public async Task StartAsync(IDialogContext context)
        {
            context.Wait(MessageReceivedAsync);
        }

        public async Task MessageReceivedAsync(IDialogContext context, IAwaitable<IMessageActivity> result)
        {
            var message = await result;

            /* If the message returned is a valid name, return it to the calling dialog. */
            if ((message.Text != null) && (message.Text.Trim().Length > 0))
            {
                if(message.Text.IndexOf("translate", StringComparison.InvariantCultureIgnoreCase) != -1)
                {
                    string phrase = message.Text.ToLower().Replace("translate", "").Trim();
                    if(this.aliisms.ContainsKey(phrase))
                    {
                        await context.PostAsync(string.Format("Ali-ism '{0}'....meaning.....\"{1}\"", phrase, this.aliisms[phrase]));
                    }
                    else
                    {
                        await context.PostAsync("New one on me.  Terms I know are, " + string.Join(", ", this.aliisms.Keys));
                    }

                }
                else if(message.Text.IndexOf("deploy", StringComparison.InvariantCultureIgnoreCase) != -1)
                {
                    await context.PostAsync("Sure, I'll squirt it up in a second.");
                }
                else
                {
                    await context.PostAsync("I'm sorry, I don't understand, you Helm.  Say translate along with an Ali-ism.  Valid Ali-isms are - " + string.Join(", ", this.aliisms.Keys));
                }
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
}