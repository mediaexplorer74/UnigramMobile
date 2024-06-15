using Telegram.Td.Api;

namespace Unigram.ViewModels.Settings
{
    internal class GetBackgrounds : Function
    {
        private bool v;

        public GetBackgrounds()
        {
        }

        public GetBackgrounds(bool v)
        {
            this.v = v;
        }

        public NativeObject ToUnmanaged()
        {
            throw new System.NotImplementedException();
        }
    }
}