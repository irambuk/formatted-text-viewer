using System;

namespace FormattedTextViewer.View.Services
{
    public interface IDetectTextFormatService
    {
        TextFormattingTypes DetectTextFormat(string text);
    }

    class DetectTextFormatService : IDetectTextFormatService
    {
        public TextFormattingTypes DetectTextFormat(string text)
        {
            if (!string.IsNullOrEmpty(text))
            {
                var cleanText = text.Trim();
                if (cleanText.StartsWith("{") || cleanText.StartsWith("["))
                {
                    return TextFormattingTypes.Json;
                }
                if (cleanText.StartsWith("<html", StringComparison.CurrentCultureIgnoreCase))
                {
                    return TextFormattingTypes.Html;
                }
                if (cleanText.StartsWith("<"))
                {
                    return TextFormattingTypes.Xml;
                }
            }
            return TextFormattingTypes.PlainText;
        }
    }
}
