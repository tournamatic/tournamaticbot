using System;
using System.Threading.Tasks;
using Microsoft.Bot.Builder.Dialogs;
using Microsoft.Bot.Connector;
using System.Diagnostics;
using System.Net.Http;
using TournamaticBot.Util;

namespace TournamaticBot
{
    [Serializable]
    public class TournamentSearchDialog : IDialog<object>
    {
        public async Task StartAsync(IDialogContext context)
        {
            await context.PostAsync("Type in something about the tournament you are searching for:");
            context.Wait(MessageRecievedAsync);
        }

        public virtual async Task MessageRecievedAsync(IDialogContext context, IAwaitable<IMessageActivity> result)
        {
            var message = await result;
            try
            {
                var tournaments = await TournamaticService.GetTournamentsByTitle(message.Text);
                if (tournaments.Count > 0)
                {
                    CardUtil.ShowHeroCard(message, tournaments);
                }
                else{
                    await context.PostAsync($"No tournaments that contains {message.Text} found");
                }
            }
            catch(Exception e)
            {
                Debug.WriteLine($"Error when searching for musician: {e.Message}");
            }
            context.Done<object>(null);
        }
        
    }
}