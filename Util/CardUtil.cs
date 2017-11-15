using System;
using System.Collections.Generic;
using Microsoft.Bot.Connector;

namespace TournamaticBot.Util
{
    public static class CardUtil
    {
        public static async void ShowHeroCard(IMessageActivity message, IList<Tournament> searchResult)
        {
            //Make reply activity and set layout
            Activity reply = ((Activity)message).CreateReply();
            reply.AttachmentLayout = AttachmentLayoutTypes.Carousel;

            //Make each Card for each musician
            foreach (var tournament in searchResult)
            {
                List<CardImage> cardImages = new List<CardImage>();
                cardImages.Add(new CardImage(url: tournament.ImageUrl));
                ThumbnailCard card = new ThumbnailCard()
                {
                    Title = tournament.Title,
                    Subtitle = $"Start: {tournament.Start.ToShortDateString() } | End: {tournament.End.ToShortDateString()}",
                    Text = tournament.ShortDescription,
                    Images = cardImages,
                    Buttons = new List<CardAction>() { new CardAction(ActionTypes.OpenUrl, "Sign up", value: $"https://tournamatic.com/#!/tournaments/{tournament.TournamentId}") }
                };
                reply.Attachments.Add(card.ToAttachment());
            }

            // Make connector and reply message

            ConnectorClient connector = new ConnectorClient(new Uri(reply.ServiceUrl));
            await connector.Conversations.SendToConversationAsync(reply);
        }
    }
}