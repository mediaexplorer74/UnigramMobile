using Telegram.Td.Api;

namespace Unigram.ViewModels
{
    internal class UploadFile : Function
    {
        private InputFile inputFile;
        private FileType type;
        private int v;

        public UploadFile(InputFile inputFile, FileType type, int v)
        {
            this.inputFile = inputFile;
            this.type = type;
            this.v = v;
        }

        public NativeObject ToUnmanaged()
        {
            throw new System.NotImplementedException();
        }
    }
}