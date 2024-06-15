using Telegram.Td.Api;

namespace Unigram.ViewModels
{
    internal class SetBackground : Function
    {
        private InputBackgroundLocal inputBackgroundLocal;
        private BackgroundTypeWallpaper backgroundTypeWallpaper;
        private bool v;
        private InputBackgroundRemote inputBackgroundRemote;
        private BackgroundType type;

        public SetBackground(InputBackgroundLocal inputBackgroundLocal, BackgroundTypeWallpaper backgroundTypeWallpaper, bool v)
        {
            this.inputBackgroundLocal = inputBackgroundLocal;
            this.backgroundTypeWallpaper = backgroundTypeWallpaper;
            this.v = v;
        }

        public SetBackground(InputBackgroundRemote inputBackgroundRemote, BackgroundType type, bool v)
        {
            this.inputBackgroundRemote = inputBackgroundRemote;
            this.type = type;
            this.v = v;
        }

        public NativeObject ToUnmanaged()
        {
            throw new System.NotImplementedException();
        }
    }
}