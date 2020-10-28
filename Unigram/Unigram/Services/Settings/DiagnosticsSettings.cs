using Windows.ApplicationModel;

namespace Unigram.Services.Settings
{
    public class DiagnosticsSettings : SettingsServiceBase
    {
        public DiagnosticsSettings()
            : base("Diagnostics")
        {
        }

        private bool? _loadMediaImmediately;
        public bool LoadMediaImmediately
        {
            get
            {
                if (_loadMediaImmediately == null)
                    _loadMediaImmediately = GetValueOrDefault("LoadMediaImmediately", false);

                return _loadMediaImmediately ?? false;
            }
            set
            {
                _loadMediaImmediately = value;
                AddOrUpdateValue("LoadMediaImmediately", value);
            }
        }

        private bool? _softwareDecoderEnabled;
        public bool SoftwareDecoderEnabled
        {
            get
            {
                if (_softwareDecoderEnabled == null)
                    _softwareDecoderEnabled = GetValueOrDefault("SoftwareDecoderEnabled", false);

                return _softwareDecoderEnabled ?? false;
            }
            set
            {
                _softwareDecoderEnabled = value;
                AddOrUpdateValue("SoftwareDecoderEnabled", value);
            }
        }

        private bool? _fastAnimationsEnabled;
        public bool FastAnimationsEnabled
        {
            get
            {
                if (_fastAnimationsEnabled == null)
                    _fastAnimationsEnabled = GetValueOrDefault("FastAnimationsEnabled", false);

                return _fastAnimationsEnabled ?? false;
            }
            set
            {
                _fastAnimationsEnabled = value;
                AddOrUpdateValue("FastAnimationsEnabled", value);
            }
        }

        private bool? _animateStickersInPanel;
        public bool AnimateStickersInPanel
        {
            get
            {
                if (_animateStickersInPanel == null)
                    _animateStickersInPanel = GetValueOrDefault("AnimateStickersInPanel", false);

                return _animateStickersInPanel ?? false;
            }
            set
            {
                _animateStickersInPanel = value;
                AddOrUpdateValue("AnimateStickersInPanel", value);
            }
        }

        private static bool? _playGifPreview;
        public bool PlayGifPreview
        {
            get
            {
                if (_playGifPreview == null)
                    _playGifPreview = GetValueOrDefault("PlayGifPreview", false);

                return _playGifPreview ?? false;
            }
            set
            {
                _playGifPreview = value;
                AddOrUpdateValue("PlayGifPreview", value);
            }
        }

        private bool? _showFilesInFolder;
        public bool ShowFilesInFolder
        {
            get
            {
                if (_showFilesInFolder == null)
                    _showFilesInFolder = GetValueOrDefault("ShowFilesInFolder", false);

                return _showFilesInFolder ?? false;
            }
            set
            {
                _showFilesInFolder = value;
                AddOrUpdateValue("ShowFilesInFolder", value);
            }
        }

        private bool? _showOpenWithVlc;
        public bool ShowOpenWithVlc
        {
            get
            {
                if (_showOpenWithVlc == null)
                    _showOpenWithVlc = GetValueOrDefault("ShowOpenWithVlc", false);

                return _showOpenWithVlc ?? false;
            }
            set
            {
                _showOpenWithVlc = value;
                AddOrUpdateValue("ShowOpenWithVlc", value);
            }
        }

        private bool? _bubbleMeasureAlpha;
        public bool BubbleMeasureAlpha
        {
            get
            {
                if (_bubbleMeasureAlpha == null)
                    _bubbleMeasureAlpha = GetValueOrDefault("BubbleMeasureAlpha", true);

                return _bubbleMeasureAlpha ?? true;
            }
            set
            {
                _bubbleMeasureAlpha = value;
                AddOrUpdateValue("BubbleMeasureAlpha", value);
            }
        }

        private bool? _bubbleAnimations;
        public bool BubbleAnimations
        {
            get
            {
                if (_bubbleAnimations == null)
                    _bubbleAnimations = GetValueOrDefault("BubbleAnimations", Package.Current.SignatureKind != PackageSignatureKind.Store);

                return _bubbleAnimations ?? false;
            }
            set
            {
                _bubbleAnimations = value;
                AddOrUpdateValue("BubbleAnimations", value);
            }
        }

        private bool? _minithumbnails;
        public bool Minithumbnails
        {
            get
            {
                if (_minithumbnails == null)
                    _minithumbnails = GetValueOrDefault("Minithumbnails", true);

                return _minithumbnails ?? true;
            }
            set
            {
                _minithumbnails = value;
                AddOrUpdateValue("Minithumbnails", value);
            }
        }
    }
}
