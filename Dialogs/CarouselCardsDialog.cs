namespace TournamaticBot
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Microsoft.Bot.Builder.Dialogs;
    using Microsoft.Bot.Connector;

    [Serializable]
    public class CarouselCardsDialog : IDialog<object>
    {
        private const string ExplorerOption = "Search by sport";
        private const string SearchOption = "Search by content";
        public async Task StartAsync(IDialogContext context)
        {
            context.Wait(this.MessageReceivedAsync);
        }

        public virtual async Task MessageReceivedAsync(IDialogContext context, IAwaitable<IMessageActivity> result)
        {
            PromptDialog.Choice(context, this.AfterMenuSelection, new List<string>() { ExplorerOption, SearchOption }, "How would you like to explore tournaments on tournamatic?");
        }

        private async Task AfterMenuSelection(IDialogContext context, IAwaitable<string> result)
        {
            var optionSelected = await result;
            switch (optionSelected)
            {
                case ExplorerOption:
                    context.Call(new TournamentExplorerDialog(), ResumeAfterOptionDialog);
                    break;
                case SearchOption:
                    context.Call(new TournamentSearchDialog(), ResumeAfterOptionDialog);
                    break;
            }

        }

        private async Task ResumeAfterOptionDialog(IDialogContext context, IAwaitable<object> result)
        {
            //This means  MessageRecievedAsync function of this dialog (PromptButtonsDialog) will receive users' messeges
            context.Wait(MessageReceivedAsync);
        }

    }
}
