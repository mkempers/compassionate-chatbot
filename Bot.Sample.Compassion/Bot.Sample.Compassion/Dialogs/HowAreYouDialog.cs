using Microsoft.Azure.CognitiveServices.Language.TextAnalytics;
using Microsoft.Azure.CognitiveServices.Language.TextAnalytics.Models;
using Microsoft.Bot.Builder.Dialogs;
using Microsoft.Bot.Connector;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Bot.Sample.Compassion.Dialogs
{
    [Serializable]
    public class HowAreYouDialog : IDialog<Double>
    {
        public async Task StartAsync(IDialogContext context)
        {
            await context.PostAsync("How are you today?");
            context.Wait(MessageReceivedAsync);
        }

        private async Task MessageReceivedAsync(IDialogContext context, IAwaitable<IMessageActivity> result)
        {
            var message = await result;

            //Sentiment analysis
            ITextAnalyticsAPI client = new TextAnalyticsAPI
            {
                AzureRegion = AzureRegions.Westcentralus,
                SubscriptionKey = "Replace with your key"
            };

            if ((message.Text != null) && (message.Text.Trim().Length > 0))
            {
                SentimentBatchResult sentimentResult = client.Sentiment(new MultiLanguageBatchInput(
                        new List<MultiLanguageInput>()
                        {
                          new MultiLanguageInput("en", "0", message.Text),
                        }));

                double sentiment = sentimentResult.Documents.First().Score ?? 0;

                // Completes the dialog, remove it from the stack and return the result.                 
                context.Done(sentiment);
            }
            else
            {
                await context.PostAsync("I'm sorry, I don't understand your reply. How are you (e.g. 'Fine', 'I feel sick')?");
                context.Wait(this.MessageReceivedAsync);
            }
        }
    }
}