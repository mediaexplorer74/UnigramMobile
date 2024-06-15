using Telegram.Td.Api;

namespace Unigram.ViewModels.Folders
{
    internal class DeleteChatFilter : Function
    {
        private int id;

        public DeleteChatFilter(int id)
        {
            this.id = id;
        }

        public NativeObject ToUnmanaged()
        {
            throw new System.NotImplementedException();
        }
    }
}