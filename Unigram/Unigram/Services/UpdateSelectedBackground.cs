using Telegram.Td.Api;

namespace Unigram.Services
{
    public class UpdateSelectedBackground
    {
        public bool ForDarkTheme;
        public Background Background;
        private bool v;

        public UpdateSelectedBackground(bool v, Background background)
        {
            this.v = v;
            Background = background;
        }
    }
}