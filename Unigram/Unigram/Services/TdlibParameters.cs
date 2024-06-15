namespace Unigram.Services
{
    public class TdlibParameters
    {
        internal string FilesDirectory;

        public string DatabaseDirectory { get; set; }
        public bool UseSecretChats { get; set; }
        public bool UseMessageDatabase { get; set; }
        public int ApiId { get; set; }
        public string ApiHash { get; set; }
        public string ApplicationVersion { get; set; }
        public string SystemVersion { get; set; }
        public string SystemLanguageCode { get; set; }
        public string DeviceModel { get; set; }
        public bool UseTestDc { get; set; }
    }
}