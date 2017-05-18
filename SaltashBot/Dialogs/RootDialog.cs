using System;
using System.Threading.Tasks;
using Microsoft.Bot.Builder.Dialogs;
using Microsoft.Bot.Connector;
using System.Threading;

namespace SaltashBot.Dialogs
{
    [Serializable]
    public class RootDialog : IDialog<object>
    {
        public async Task StartAsync(IDialogContext context)
        {
            // Root dialog initiates and waits for the next message from the user. 
            // When a message arrives, call MessageReceivedAsync.
            context.Wait(this.MessageReceivedAsync);
        }

        private async Task MessageReceivedAsync(IDialogContext context, IAwaitable<IMessageActivity> result)
        {
            /* When MessageReceivedAsync is called, it's passed an IAwaitable<IMessageActivity>. To get the message,
             *  await the result. */
            var message = await result;

            await this.SendWelcomeMessageAsync(context);
        }

        private async Task SendWelcomeMessageAsync(IDialogContext context)
        {
//            await context.PostAsync("Hi, I'm the Saltash bot. Let's get started.");

            context.Call(new EventDialog(), this.ResumeAfterEventDialog);
        }

        public async Task ResumeAfterEventDialog(IDialogContext context, IAwaitable<object> result)
        {
            // Store the value that EventDialog returned. 
            // (At this point, new order dialog has finished and returned some value to use within the root dialog.)
 //           var resultFromNewOrder = await result;

//            await context.PostAsync($"Thanks for using the Saltash bot: {resultFromNewOrder}");

            await this.SendWelcomeMessageAsync(context);
        }

        //public virtual async Task MessageReceivedAsync(IDialogContext context, IAwaitable<IMessageActivity> result)
        //{
        //    var message = await result; // We've got a message!
        //    if (message.Text.ToLower().Contains("event"))
        //    {
        //        // User said 'event', so invoke the Event Search Dialog and wait for it to finish.
        //        // Then, call ResumeAfterEventSearchDialog.
        //        await context.Forward(new EventDialog(), ResumeAfterEventDialog, message, CancellationToken.None);
        //    }
        //    else
        //    {
        //        // calculate something for us to return
        //        int length = (message.Text ?? string.Empty).Length;

        //        // return our reply to the user
        //        await context.PostAsync($"How can I help you?");
        //    }
        //    context.Wait(MessageReceivedAsync);
        //}
    }
}