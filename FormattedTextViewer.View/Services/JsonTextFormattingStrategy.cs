using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FormattedTextViewer.View.Common;
using Newtonsoft.Json;

namespace FormattedTextViewer.View.Services
{
    class JsonTextFormattingStrategy : ITextFormattingStrategy
    {
        public string FormatString(string text)
        {
            try
            {
                var data = JsonConvert.DeserializeObject(text);
                var jsonText = JsonConvert.SerializeObject(data, Formatting.Indented);
                return jsonText;
            }
            catch (Exception ex)
            {
                var stringBuffer = new StringBuilder();
                stringBuffer.AppendFormat("Error: {0}", ex.Message);
                return stringBuffer.ToString();
            }
        }
    }
}
