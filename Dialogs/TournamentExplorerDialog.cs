using System;
using System.Threading.Tasks;
using Microsoft.Bot.Builder.Dialogs;
using Microsoft.Bot.Connector;
using System.Diagnostics;
using System.Collections.Generic;
using System.Net.Http;
using Newtonsoft.Json.Linq;
using TournamaticBot.Util;

namespace TournamaticBot
{
    [Serializable]
    public class TournamentExplorerDialog : IDialog<object>
    {
        private IDictionary<string, int> _Sports = new Dictionary<string, int>();
        public async Task StartAsync(IDialogContext context)
        {
            try
            {
                _Sports = await TournamaticService.GetSports();
                if (_Sports.Count > 0)
                {
                    List<string> eras = new List<string>();
                    foreach (var era in _Sports)
                    {
                        eras.Add($"{era.Key}");
                    }
                    PromptDialog.Choice(context, AfterMenuSelection, eras, "Which sport are you interested in?");
                }
                else
                {
                    await context.PostAsync("I couldn't find any genres to show you");
                }
            }
            catch (Exception e)
            {
                Debug.WriteLine($"Error when faceting by era: {e}");
                context.Done <object>(null);
            }
        }

        private async Task AfterMenuSelection(IDialogContext context, IAwaitable<string> result)
        {
            var optionSelected = await result;
            string selectedSport = optionSelected.Split(' ')[0];

            try
            {
                var tournaments = await TournamaticService.GetTournamentsBySport(_Sports[selectedSport]);
                if (tournaments.Count > 0)
                {
                    CardUtil.ShowHeroCard((IMessageActivity)context.Activity, tournaments);
                }
                else
                {
                    await context.PostAsync($"I couldn't find any tournaments :0");
                }
            }
            catch(Exception e)
            {
                Debug.WriteLine($"Error when filtering by sport: {e}");
            }
            context.Done<object>(null);
        }
    }
}