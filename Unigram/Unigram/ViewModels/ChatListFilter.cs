using Telegram.Td.Api;

namespace Unigram.ViewModels
{
    public class ChatListFilter : ChatList
    {
        internal int ChatFilterId;
        private int id;

        public ChatListFilter(int id)
        {
            this.id = id;
        }

        public NativeObject ToUnmanaged()
        {
            throw new System.NotImplementedException();
        }
    }
}