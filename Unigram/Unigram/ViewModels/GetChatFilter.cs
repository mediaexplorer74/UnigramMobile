using Telegram.Td.Api;

namespace Unigram.ViewModels
{
    internal class GetChatFilter : Function
    {
        private int chatFilterId;

        public GetChatFilter(int chatFilterId)
        {
            this.chatFilterId = chatFilterId;
        }

        public NativeObject ToUnmanaged()
        {
            throw new System.NotImplementedException();
        }
    }
}