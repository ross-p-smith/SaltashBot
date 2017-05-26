using Microsoft.Bot.Connector;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http.Controllers;
using System.Threading;
using System.Threading.Tasks;

namespace SaltashBot.Attributes
{
    public class AliBotAuthentication : BotAuthentication
    {
        public AliBotAuthentication()
        {

            this.MicrosoftAppId = "ac1b0a8c-a7db-46c8-be3b-0b6602f58778";
            this.MicrosoftAppPassword = "FD4oaoz0hwzPEvWWOvYVvrn";

        }
    }

}