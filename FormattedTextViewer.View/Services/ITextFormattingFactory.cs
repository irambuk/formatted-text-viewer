using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
