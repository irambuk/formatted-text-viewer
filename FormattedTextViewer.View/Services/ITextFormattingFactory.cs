namespace FormattedTextViewer.View.Services
{
    public enum TextFormattingTypes
    {
        Json,
        Xml,
        Html,
        PlainText,
    }

    public interface ITextFormattingFactory
    {
        ITextFormattingStrategy FindTextFormattingStrategy(TextFormattingTypes type);
    }
}
