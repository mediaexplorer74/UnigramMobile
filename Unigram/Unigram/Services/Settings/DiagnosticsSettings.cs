namespace Unigram.Services.Settings
{
    public class DiagnosticsSettings : SettingsServiceBase
    {
        public DiagnosticsSettings()
            : base("Diagnostics")
        {
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

        private bool? _bubbleKnockout;
        public bool BubbleKnockout
        {
            get
            {
                if (_bubbleKnockout == null)
                    _bubbleKnockout = GetValueOrDefault("BubbleKnockout", false);

                return _bubbleKnockout ?? false;
            }
            set
            {
                _bubbleKnockout = value;
                AddOrUpdateValue("BubbleKnockout", value);
            }
        }
    }
}
