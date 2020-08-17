using System;
using Windows.UI.Xaml.Data;

namespace Unigram.Converters
{
    public class CompressionToGlyphConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            if (value is double)
            {
                value = (int)(double)value;
            }

            // Use language parameter for fictious maxValue to have an uncompressed option
            if (int.TryParse(language, out int maxValue) && (int)value == maxValue)
                return parameter == null ? "\uE900" : "∞";

            switch ((int)value)
            {
                case 0:
                    return parameter == null ? "\uE901" : "240p";
                case 1:
                    return parameter == null ? "\uE902" : "360p";
                case 2:
                    return parameter == null ? "\uE903" : "480p";
                case 3:
                    return parameter == null ? "\uE904" : "720p";
                case 4:
                    return parameter == null ? "\uE905" : "1080p";
                case 5:
                    return parameter == null ? "\uE907" : "UHD";
                case 6:
                    return parameter == null ? "\uE908" : "4k";
                case 7:
                    return parameter == null ? "\uE909" : "8k";
                default:
                    return parameter == null ? "\uE900" : "∞";
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            throw new NotImplementedException();
        }
    }
}
