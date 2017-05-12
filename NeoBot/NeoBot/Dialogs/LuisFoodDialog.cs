using Microsoft.Bot.Builder.Dialogs;
using Microsoft.Bot.Builder.Luis;
using Microsoft.Bot.Builder.Luis.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace NeoBot.Dialogs
{
    [LuisModel("0f4052d2-2453-4066-bc18-9b3b5b1eaeb7", "a9cac82f4c594428aeb0ffbe5d1dbc62")]
    [Serializable]
    public class LuisFoodDialog : LuisDialog<object>
    {
        [LuisIntent("Pick restaurant")]
        public async Task TurnOffAlarm(IDialogContext context, LuisResult result)
        {
            var cusine = GetCuisine(result) ?? "casual";

            await context.PostAsync($"You should visit Bella Ciao. They have the best {cusine} food");
            context.Wait(MessageReceived);
        }

        [LuisIntent("None")]
        public async Task None(IDialogContext context, LuisResult result)
        {
            await context.PostAsync("Sorry, I`m not sure...");
            context.Wait(MessageReceived);
        }

        [LuisIntent("Greetings")]
        public async Task SayHi(IDialogContext context, LuisResult result)
        {
            await context.PostAsync("Hi,friend!");
            context.Wait(MessageReceived);
        }

        [LuisIntent("Help")]
        public async Task Help(IDialogContext context, LuisResult result)
        {
            await context.PostAsync("I will try to help you)");
            context.Wait(MessageReceived);
        }

        #region Private
        private static string GetCuisine(LuisResult result)
        {
            return mapParameter(result.Entities, NAME);
        }

        private static string mapParameter(IList<EntityRecommendation> entities, string type)
        {
            return entities.FirstOrDefault(it => it.Type == type)?.Entity;
        }
        private const string NAME = "Places.Cuisine";
        #endregion
    }
}