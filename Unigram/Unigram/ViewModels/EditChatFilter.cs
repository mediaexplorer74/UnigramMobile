using Telegram.Td.Api;
using Unigram.Converters;

namespace Unigram.ViewModels
{
    internal class EditChatFilter : Function
    {
        private int chatFilterId;
        private ChatFilter filter;

        public EditChatFilter(int chatFilterId, ChatFilter filter)
        {
            this.chatFilterId = chatFilterId;
            this.filter = filter;
        }

        public NativeObject ToUnmanaged()
        {
            throw new System.NotImplementedException();
        }
    }
}