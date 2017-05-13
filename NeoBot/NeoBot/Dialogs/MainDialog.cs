using Microsoft.Bot.Builder.Dialogs;
using Microsoft.Bot.Builder.Location;
using Microsoft.Bot.Connector;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Configuration;

namespace NeoBot.Dialogs
{
    [Serializable]
    public class MainDialog : IDialog<string>
    {
        private readonly string channelId;

        public MainDialog(string channelId)
        {
            this.channelId = channelId;
        }

#pragma warning disable CS1998 // Async method lacks 'await' operators and will run synchronously
        public async Task StartAsync(IDialogContext context)
#pragma warning restore CS1998 // Async method lacks 'await' operators and will run synchronously
        {
            context.Wait(this.MessageReceivedAsync);
        }

#pragma warning disable CS1998 // Async method lacks 'await' operators and will run synchronously
        private async Task MessageReceivedAsync(IDialogContext context, IAwaitable<IMessageActivity> argument)
#pragma warning restore CS1998 // Async method lacks 'await' operators and will run synchronously
        {
            var apiKey = WebConfigurationManager.AppSettings["BingMapsApiKey"];
            var options = LocationOptions.UseNativeControl | LocationOptions.ReverseGeocode;

            var requiredFields = LocationRequiredFields.None;

            var prompt = "Where should I ship your order?";

            var locationDialog = new LocationDialog(apiKey, this.channelId, prompt, options, requiredFields);

            //await context.Forward(locationDialog, this.ResumeAfterLocationDialogAsync, argument, CancellationToken.None );
            context.Call(locationDialog, this.ResumeAfterLocationDialogAsync);
        }

        private async Task ResumeAfterLocationDialogAsync(IDialogContext context, IAwaitable<Place> result)
        {
            try
            {
                var place = await result;

                if (place != null)
                {
                    var address = place.GetPostalAddress();
                    var formatteAddress = string.Join(", ", new[]
                     {
                        address.StreetAddress,
                        address.Locality,
                        address.Region,
                        address.PostalCode,
                        address.Country
                    }.Where(x => !string.IsNullOrEmpty(x)));

                    await context.PostAsync("Thanks, I will ship it to " + formatteAddress);
                }
            }
            catch (Exception e)
            {
            }
            context.Done<string>(null);
        }

    }
}