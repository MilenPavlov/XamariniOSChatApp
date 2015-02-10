using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XamChat.Core.Models;

namespace XamChat.Core.ViewModels
{
    public class MessageViewModel: BaseViewModel
    {
        public Conversation[] Conversations { get; private set; }
        public Conversation Conversation { get; set; }
        public Message[] Messages { get; set; }
        public string Text { get; set; }

        public async Task GetConversations()
        {
            if (settings.User == null)
            {
                throw new Exception("Not logged in.");
            }

            IsBusy = true;

            try
            {
                Conversations = await service.GetConversations(settings.User.Id);
            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }
            finally
            {
                IsBusy = false;
            }
        }

        public async Task GetMessages()
        {
            if (Conversation == null)
            {
                throw  new Exception("No conversations");
            }

            IsBusy = true;
            try
            {
                Messages = await service.GetMessages(Conversation.Id);
            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }
            finally
            {
                IsBusy = false;
            }
        }

        public async Task SendMessage()
        {
            if (settings.User == null)
            {
                throw new Exception("Not logged in.");
            }

            if (Conversation == null)
            {
                throw new Exception("No conversation.");
            }

            if (string.IsNullOrEmpty(Text))
            {
                throw  new Exception("Message is blank");
            }

            IsBusy = true;

            try
            {
                var message = await service.SendMessage(new Message
                {
                    UserId = settings.User.Id,
                    ConversationId = Conversation.Id,
                    Text = Text
                });

                var messages = new List<Message>();
                if (Messages != null)
                {
                    messages.AddRange(Messages);
                }
                messages.Add(message);

                Messages = messages.ToArray();

            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }
            finally
            {
                IsBusy = false;
            }
        }

    }
}
