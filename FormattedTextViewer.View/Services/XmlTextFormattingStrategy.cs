using FormattedTextViewer.View.Common;
using System;
using System.IO;
using System.Text;
using System.Xml;

namespace FormattedTextViewer.View.Services
{
    class XmlTextFormattingStrategy : ITextFormattingStrategy
    {
        public string FormatString(string text)
        {
            try
            {
                var xmlDoc = new XmlDocument();
                xmlDoc.LoadXml(text);

                var stream = new MemoryStream();
                var writer = new XmlTextWriter(stream, Encoding.ASCII);
                writer.Formatting = Formatting.Indented;
                xmlDoc.WriteContentTo(writer);

                writer.Flush();
                stream.Flush();
                stream.Position = 0;
                var sReader = new StreamReader(stream);
                var formattedXml = sReader.ReadToEnd();
                return formattedXml;
            }
            catch (Exception)
            {
                return Constants.TextInvalidFormat;
            }
        }
    }
}
