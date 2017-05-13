using System;
using System.Threading.Tasks;
using Microsoft.Bot.Builder.Dialogs;
using Microsoft.Bot.Connector;
using ApiAiSDK;
using ApiAiSDK.Model;

namespace NeoBot.Dialogs
{
    [Serializable]
    public class ApiAIDialog : IDialog<object>
    {
        public Task StartAsync(IDialogContext context)
        {
            context.Wait(MessageReceivedAsync);

            return Task.CompletedTask;
        }

        private async Task MessageReceivedAsync(IDialogContext context, IAwaitable<object> result)
        {
            var activity = await result as Activity;
            var config = new AIConfiguration("6a867e192476428b8df2fab998464b57", SupportedLanguage.English);
            var apiAi = new ApiAi(config);
            var response = apiAi.TextRequest(activity.Text);
            // return our reply to the user
            await context.PostAsync($"{response.Result.Fulfillment.Speech}");
            context.Wait(MessageReceivedAsync);
        }
    }
}