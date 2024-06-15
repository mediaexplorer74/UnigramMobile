using Telegram.Td.Api;

namespace Unigram.Converters
{
    public class GetChatFilterDefaultIconName : Function
    {
        private ChatFilter filter;

        public GetChatFilterDefaultIconName(ChatFilter filter)
        {
            this.filter = filter;
        }

        public NativeObject ToUnmanaged()
        {
            throw new System.NotImplementedException();
        }
    }
}