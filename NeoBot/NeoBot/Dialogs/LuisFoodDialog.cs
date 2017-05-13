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
        public string TypeofFood { get; set; }
        public string Cuisine { get; set; }
        public string Name { get; set; }
        public string TypeofPlace { get; set; }

        [LuisIntent("Pick restaurant")]
        public async Task TurnOffAlarm(IDialogContext context, LuisResult result)
        {
            Cuisine = GetCuisine(result);
            TypeofFood = mapParameter(result.Entities, "Places.MealType");
            await context.PostAsync($"You should visit Bella Ciao. They have the best {Cuisine} food");
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
            await context.PostAsync("Hi,friend! My name is Neo");
            context.Wait(MessageReceived);
        }

        [LuisIntent("Help")]
        public async Task Help(IDialogContext context, LuisResult result)
        {
            await context.PostAsync("I will try to help you)");
            context.Wait(MessageReceived);
        }

        [LuisIntent("Get.Cuisine")]
        public async Task GetCuisine(IDialogContext context, LuisResult result)
        {
            await context.PostAsync("I will try to help you)");
            context.Wait(MessageReceived);
        }
     
        [LuisIntent("Get.TypeofFood")]
        public async Task GetTypeofFood(IDialogContext context, LuisResult result)
        {
            TypeofFood = mapParameter(result.Entities, "Places.MealType");
            await context.PostAsync($"Got it. I`ll find you some {TypeofFood} for you");
            var cafes = await NeoBot.Map.GeoSearch.GetPlacesNearBy(49.836178,24.031274, 5000, "cafe", TypeofFood);
            Data_Base.GeoObject first = cafes.First();
            await context.PostAsync(Map.GeoSearch.GetUrlDirection(49.811585, 24.019715, first.Latitude, first.Longitude));
            context.Wait(MessageReceived);
        }

        [LuisIntent("Bye")]
        public async Task Bye(IDialogContext context, LuisResult result)
        {
            await context.PostAsync("Bye. It was nice to meet you)");
            context.Wait(MessageReceived);
        }

        [LuisIntent("TypeofPlace")]
        public async Task GetTypeofPlace(IDialogContext context, LuisResult result)
        {
            TypeofPlace = mapParameter(result.Entities, "Places.PlaceType");
            await context.PostAsync($"Ok, I`ll find you some good {TypeofPlace}");
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