using Telegram.Td.Api;

namespace Unigram.Views
{
    internal class MessageForwardOriginHiddenUser : MessageOrigin
    {
        internal string SenderName;

        public NativeObject ToUnmanaged()
        {
            throw new System.NotImplementedException();
        }
    }
}