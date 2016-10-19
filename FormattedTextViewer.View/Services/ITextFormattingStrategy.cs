using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FormattedTextViewer.View.Services
{
    public interface ITextFormattingStrategy
    {
        string FormatString(string text);
    }
}
