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
        private int attempts = 0;
        private int maxCards = 5;
        private List<string> imageUrlsDay;
        private Random random;

        public EventDialog()
        {
            this.imageUrlsDay = new List<string>();
            this.imageUrlsDay.Add("http://www.saltash.website/content/images/backgrounds/day/Arial Shot of Saltash and St Budeaux.jpg");
            this.imageUrlsDay.Add("http://www.saltash.website/content/images/backgrounds/day/Churchtown Farm - Saltash.jpg");
            this.imageUrlsDay.Add("http://www.saltash.website/content/images/backgrounds/day/Churchtown Farm Top Field.jpg");
            this.imageUrlsDay.Add("http://www.saltash.website/content/images/backgrounds/day/Churchtown View of Tamar.jpg");
            this.imageUrlsDay.Add("http://www.saltash.website/content/images/backgrounds/day/Outside the Cecil pub in St Stephens.jpg");
            this.imageUrlsDay.Add("http://www.saltash.website/content/images/backgrounds/day/River Tamar From Saltash.jpg");
            this.imageUrlsDay.Add("http://www.saltash.website/content/images/backgrounds/day/Saltash Waterfront Beach.jpg");
            this.imageUrlsDay.Add("http://www.saltash.website/content/images/backgrounds/day/Saltash Waterfront with Union Inn.jpg");
            this.imageUrlsDay.Add("http://www.saltash.website/content/images/backgrounds/day/Saltash Waterfront.jpg");
            this.imageUrlsDay.Add("http://www.saltash.website/content/images/backgrounds/day/View From Top of Saltash Town.jpg");
            this.imageUrlsDay.Add("http://www.saltash.website/content/images/backgrounds/day/View of the Tamar bridges from St Budeaux.jpg");
            this.imageUrlsDay.Add("http://www.saltash.website/content/images/backgrounds/day/Waterfront Shot of Tamar Bridges.jpg");
            this.imageUrlsDay.Add("http://www.saltash.website/content/images/backgrounds/day/Waterfront With Brunel and Bridge.jpg");
            this.imageUrlsDay.Add("http://www.saltash.website/content/images/backgrounds/day/Waterfront With Two Bridges and Pier.jpg");
            this.imageUrlsDay.Add("http://www.saltash.website/content/images/backgrounds/day/Waterfront With Two Bridges and Union Inn.jpg");
            this.imageUrlsDay.Add("http://www.churchtownfarm.saltash.website/content/images/gallery/landscape/thumbnails/Anthony Passage from Chall Park field.jpg");
            this.imageUrlsDay.Add("http://www.churchtownfarm.saltash.website/content/images/gallery/landscape/thumbnails/Chall Park from Antony passage.jpg");
            this.imageUrlsDay.Add("http://www.churchtownfarm.saltash.website/content/images/gallery/landscape/thumbnails/Forder Creek 1.jpg");
            this.imageUrlsDay.Add("http://www.churchtownfarm.saltash.website/content/images/gallery/landscape/thumbnails/Forder Creek 2.JPG");
            this.imageUrlsDay.Add("http://www.churchtownfarm.saltash.website/content/images/gallery/landscape/thumbnails/Forder Creek from Antony Passage.jpg");
            this.imageUrlsDay.Add("http://www.churchtownfarm.saltash.website/content/images/gallery/landscape/thumbnails/Forder Creek from Chall Park.JPG");
            this.imageUrlsDay.Add("http://www.churchtownfarm.saltash.website/content/images/gallery/landscape/thumbnails/Forder Creek from Quay park.jpg");
            this.imageUrlsDay.Add("http://www.churchtownfarm.saltash.website/content/images/gallery/landscape/thumbnails/Forder Creek looking towards Viaduct.jpg");
            this.imageUrlsDay.Add("http://www.churchtownfarm.saltash.website/content/images/gallery/landscape/thumbnails/Forder viaduct from Lower Southground field.jpg");
            this.imageUrlsDay.Add("http://www.churchtownfarm.saltash.website/content/images/gallery/landscape/thumbnails/Forder viaduct from Passage point.jpg");
            this.imageUrlsDay.Add("http://www.churchtownfarm.saltash.website/content/images/gallery/landscape/thumbnails/Hay making at Blackers field.jpg");
            this.imageUrlsDay.Add("http://www.churchtownfarm.saltash.website/content/images/gallery/landscape/thumbnails/Hay making at Hitchings field.jpg");
            this.imageUrlsDay.Add("http://www.churchtownfarm.saltash.website/content/images/gallery/landscape/thumbnails/Hay making with Paul Maunder at Blackers field.jpg");
            this.imageUrlsDay.Add("http://www.churchtownfarm.saltash.website/content/images/gallery/landscape/thumbnails/Jupitor point from points field 2.JPG");
            this.imageUrlsDay.Add("http://www.churchtownfarm.saltash.website/content/images/gallery/landscape/thumbnails/Jupitor Point from Points field.jpg");
            this.imageUrlsDay.Add("http://www.churchtownfarm.saltash.website/content/images/gallery/landscape/thumbnails/Marsh meadow park from Bramble park field.jpg");
            this.imageUrlsDay.Add("http://www.churchtownfarm.saltash.website/content/images/gallery/landscape/thumbnails/Name the Bird on a freshly ploughed Sand Acre.JPG");
            this.imageUrlsDay.Add("http://www.churchtownfarm.saltash.website/content/images/gallery/landscape/thumbnails/Old Mill at Antony passage from Chall Park.JPG");
            this.imageUrlsDay.Add("http://www.churchtownfarm.saltash.website/content/images/gallery/landscape/thumbnails/Plymouth from Sand Acre Bay.jpg");
            this.imageUrlsDay.Add("http://www.churchtownfarm.saltash.website/content/images/gallery/landscape/thumbnails/Plymouth from Wearde quay.jpg");
            this.imageUrlsDay.Add("http://www.churchtownfarm.saltash.website/content/images/gallery/landscape/thumbnails/Sandy Acre meadow.jpg");
            this.imageUrlsDay.Add("http://www.churchtownfarm.saltash.website/content/images/gallery/landscape/thumbnails/Trematon castle from Seine House field.jpg");
            this.imageUrlsDay.Add("http://www.churchtownfarm.saltash.website/content/images/gallery/landscape/thumbnails/Viaduct with the 18.50 Train going over from Saltash.jpg");
            this.imageUrlsDay.Add("http://www.churchtownfarm.saltash.website/content/images/gallery/landscape/thumbnails/View from Lower south ground field.jpg");


            this.random = new Random();
        }


        public async Task StartAsync(IDialogContext context)
        {
            await context.PostAsync("Hi, I'm the Saltash event bot. I understand the commands \"events\", \"about\" and \"photos\".");
            context.Wait(MessageReceivedAsync);
        }

        public async Task MessageReceivedAsync(IDialogContext context, IAwaitable<IMessageActivity> result)
        {
            var message = await result;

            /* If the message returned is a valid name, return it to the calling dialog. */
            if ((message.Text != null) && (message.Text.Trim().Length > 0))
            {
                if (message.Text.IndexOf("event", StringComparison.InvariantCultureIgnoreCase) != -1)
                {
                    IMessageActivity reply = BuildEventResponse(context);
                    await context.PostAsync("Here are the next five events taking place in Saltash.  Click on each event for more information.");
                    await context.PostAsync(reply);
                    context.Done(reply);
                }
                else if (message.Text.IndexOf("about", StringComparison.InvariantCultureIgnoreCase) != -1)
                {
                    IMessageActivity reply = BuildWikiResponse(context, "Saltash");
                    await context.PostAsync(reply);
                    context.Done(reply);
                }
                else if (
                            message.Text.IndexOf("photo", StringComparison.InvariantCultureIgnoreCase) != -1
                            || message.Text.IndexOf("picture", StringComparison.InvariantCultureIgnoreCase) != -1
                    )
                {
                    IMessageActivity reply = BuildPhotoResponse(context);
                    await context.PostAsync("Here are some randomly selected photographs of Saltash");
                    await context.PostAsync(reply);
                    context.Done(reply);
                }
                else
                {
                    await context.PostAsync("I'm sorry, I don't understand.  I'm a bit simple!  I only understand the commands \"events\", \"about\" and \"photos\".");
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

        private IMessageActivity BuildWikiResponse(IDialogContext context, string keyword)
        {
            string url = string.Format("https://en.wikipedia.org/w/api.php?format=json&action=query&prop=extracts&exintro=&explaintext=&titles={0}", keyword);
            string response = string.Empty;
            var reply = context.MakeMessage();
            reply.Attachments = new List<Attachment>();
            reply.AttachmentLayout = "Single";

            using (HttpClient client = new HttpClient())
            {
                response = client.GetStringAsync(url).Result;
            }

            response = response.Replace("\"452227\"", "Article");

            var wikiResponse = JsonConvert.DeserializeObject<WikipediaObject>(response);

            reply.Attachments.Add(
                new HeroCard
                {
                    Title = "About Saltash",
                    Subtitle = "Extract from Wikipedia - https://en.wikipedia.org/wiki/Saltash",
                    Text = wikiResponse.Query.Pages.Article.Extract,
                    Tap = new CardAction("openUrl", "Saltash Wikipedia", GetRandomImage(), "https://en.wikipedia.org/wiki/Saltash")
                }
                .ToAttachment());

            return reply;
        }

        private IMessageActivity BuildEventResponse(IDialogContext context)
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
                        Subtitle = evt.Location,
                        Text = string.Format("Starts {0}\r\nEnds {1}", evt.StartDate.Value.ToString("F"), evt.EndDate.Value.ToString("F")),
                        Tap = new CardAction("openUrl", evt.Title, GetRandomImage(), evt.CalendarName.ToLower().Contains("facebook") ? "https://www.facebook.com/events/" + evt.Id : "http://www.saltash.website")
                    }
                    .ToAttachment()
                    );

                if (counter == this.maxCards)
                {
                    break;
                }

                counter++;

            }

            return reply;

        }

        private IMessageActivity BuildPhotoResponse(IDialogContext context)
        {
            int counter = 0;
            var reply = context.MakeMessage();

            /* Completes the dialog, removes it from the dialog stack, and returns the result to the parent/calling
                dialog. */
            reply.Attachments = new List<Attachment>();
            reply.AttachmentLayout = AttachmentLayoutTypes.Carousel;
            List<string> images = new List<string>();

            do
            {
                counter++;
                string url = string.Empty;

                do
                {
                    url = GetRandomImage();
                }
                while (images.Contains(url));

                images.Add(url);

                string imageName = url.Substring(url.LastIndexOf("/") + 1, (url.LastIndexOf(".") - url.LastIndexOf("/")) - 1);
                HeroCard card = new HeroCard
                {
                    Title = imageName,
                    Images = new List<CardImage>(),
                    Subtitle = "Photos provided by http://www.saltash.website"
                };

                card.Images.Add(new CardImage { Url = url, Alt = imageName });


                reply.Attachments.Add(
                    card.ToAttachment()
                        );

            }
            while (counter != maxCards);

            return reply;
        }


        private string GetRandomImage()
        {
            return this.imageUrlsDay[random.Next(this.imageUrlsDay.Count)];
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