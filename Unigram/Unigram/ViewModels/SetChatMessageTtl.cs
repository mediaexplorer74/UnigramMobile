using Telegram.Td.Api;

namespace Unigram.ViewModels
{
    internal class SetChatMessageTtl : Function
    {
        private long id;
        private int value;

        public SetChatMessageTtl(long id, int value)
        {
            this.id = id;
            this.value = value;
        }

        public NativeObject ToUnmanaged()
        {
            throw new System.NotImplementedException();
        }
    }
}