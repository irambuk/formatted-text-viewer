using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
