using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.Bot.Builder.Dialogs;
using Microsoft.Bot.Connector;

namespace Bot1_Maratona.Dialogs
{
    [Serializable]
    public class RootDialog : IDialog<object>
    {
        public Task StartAsync(IDialogContext context)
        {
            context.Wait(MessageReceivedAsync);

            return Task.CompletedTask;
        }

        private async Task MessageReceivedAsync(IDialogContext context, IAwaitable<object> result)
        {
            var activity = await result as Activity;

            // Calculate something for us to return
            //int length = (activity.Text ?? string.Empty).Length;
            // Return our reply to the user
            //await context.PostAsync($"You sent {activity.Text} which was {length} characters");

            await context.PostAsync("Olá, tudo bem?");

            var message = activity.CreateReply();

            var heroCard = new HeroCard()
            {
                Title = "Titulo 1",
                Subtitle = "Subtitle",
                Images = new List<CardImage>
                {
                    new CardImage("https://cdn.technologyadvice.com/wp-content/uploads/2018/02/friendly-chatbot-700x408.jpg",
                    "Imagem de Bot")
                }
            };

            message.Attachments.Add(heroCard.ToAttachment());

            await context.PostAsync(message);

            context.Wait(MessageReceivedAsync);
        }
    }
}