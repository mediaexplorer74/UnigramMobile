using Telegram.Td.Api;

namespace Unigram.ViewModels
{
    internal class ToggleMessageSenderIsBlocked : Function
    {
        private MessageSenderUser messageSenderUser;
        private bool v;
        private MessageSenderChat messageSenderChat;

        public ToggleMessageSenderIsBlocked(MessageSenderUser messageSenderUser, bool v)
        {
            this.messageSenderUser = messageSenderUser;
            this.v = v;
        }

        public ToggleMessageSenderIsBlocked(MessageSenderChat messageSenderChat, bool v)
        {
            this.messageSenderChat = messageSenderChat;
            this.v = v;
        }

        public NativeObject ToUnmanaged()
        {
            throw new System.NotImplementedException();
        }
    }
}