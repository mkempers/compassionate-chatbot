using System;
using System.Threading.Tasks;
using Microsoft.Bot.Builder.Dialogs;

namespace Bot.Sample.Compassion.Dialogs
{
    [Serializable]
    public class RootDialog : IDialog<object>
    {
        public Task StartAsync(IDialogContext context)
        {
            context.Wait(MessageReceivedAsync);

            return Task.CompletedTask;
        }

        private static async Task MessageReceivedAsync(IDialogContext context, IAwaitable<object> result)
        {
            var message = await result;
            SendHowAreYou(context);
        }

        private static void SendHowAreYou(IDialogContext context)
        {
            context.Call(new HowAreYouDialog(), HowAreYouDialogResumeAfter);
        }

        private static async Task HowAreYouDialogResumeAfter(IDialogContext context, IAwaitable<Double> result)
        {
            var sentiment = await result;

            if (sentiment > 0.5)
            {
                await context.PostAsync($"Good to hear, now I also feel good!");
            }
            else
            {
                await context.PostAsync($"Too bad, makes me feel a bit sad!");
            }
        }
    }
}