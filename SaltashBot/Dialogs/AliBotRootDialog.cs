using System;
using System.Threading.Tasks;
using Microsoft.Bot.Builder.Dialogs;
using Microsoft.Bot.Connector;
using System.Threading;

namespace SaltashBot.Dialogs
{
    [Serializable]
    public class AliBotRootDialog : IDialog<object>
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
            //await this.SendWelcomeMessageAsync(context);
            var message = await result;
            SendWelcomeMessageAsync(context);
        }

        private void SendWelcomeMessageAsync(IDialogContext context)
        {
            context.Call(new AliBotEventDialog(), this.ResumeAfterEventDialog);
        }

        public async Task ResumeAfterEventDialog(IDialogContext context, IAwaitable<object> result)
        {
            // Store the value that EventDialog returned. 
            // (At this point, new order dialog has finished and returned some value to use within the root dialog.)
            //           var resultFromNewOrder = await result;
            var message = await result;
            this.SendWelcomeMessageAsync(context);
        }
    }
}