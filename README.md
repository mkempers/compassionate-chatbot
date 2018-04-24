# Compassionate chatbot
Solution to enable compassion in a chatbot. Uses text sentiment analysis. 
![Compassionate chatbot](chat.png)

To run the solution:
* Register for Azure text analytics api, see https://azure.microsoft.com/en-us/services/cognitive-services/text-analytics/.
* Edit the following lines in file HowAreYouDialog.cs:
```
            //Sentiment analysis
            ITextAnalyticsAPI client = new TextAnalyticsAPI
            {
                AzureRegion = AzureRegions.Westcentralus,
                SubscriptionKey = "Replace with your key"
            };
```
Replace the azure region and subscription key with the values received when registering for Azure text analytics api.

To test the bot you can use the Bot Framework Emulator, see https://github.com/Microsoft/BotFramework-Emulator/.