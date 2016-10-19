using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FormattedTextViewer.View.Services
{
    class TextFormattingFactory : ITextFormattingFactory
    {
        public ITextFormattingStrategy FindTextFormattingStrategy(TextFormattingTypes type)
        {
            switch (type)
            {
                case TextFormattingTypes.Json:
                    return new JsonTextFormattingStrategy();
                case TextFormattingTypes.Xml:
                    return new XmlTextFormattingStrategy();
                case TextFormattingTypes.Html:
                    return new HtmlTextFormattingStrategy();
                case TextFormattingTypes.PlainText:
                    return new PlainTextFormattingStrategy();
                default:
                    throw new NotSupportedException("Given text formatting type is not supported:"+ type);
            }
        }
    }
}
