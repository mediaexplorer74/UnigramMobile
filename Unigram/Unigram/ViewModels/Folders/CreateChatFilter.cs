using Telegram.Td.Api;

namespace Unigram.ViewModels.Folders
{
    internal class CreateChatFilter : Function
    {
        private object filter;

        public CreateChatFilter(object filter)
        {
            this.filter = filter;
        }

        public NativeObject ToUnmanaged()
        {
            throw new System.NotImplementedException();
        }
    }
}