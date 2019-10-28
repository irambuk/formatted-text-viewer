namespace FormattedTextViewer.View.Services
{
    class PlainTextFormattingStrategy : ITextFormattingStrategy
    {
        public string FormatString(string text)
        {
            return text;
        }
    }
}
