using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Caliburn.Micro;
using FormattedTextViewer.View.Services;
using PropertyChanged;
using System.Windows;

namespace FormattedTextViewer.View
{
    public interface IMainWindowViewModel
    {
    }


    [ImplementPropertyChanged]
    public class MainWindowViewModel : PropertyChangedBase, IMainWindowViewModel
    {
        private ITextFormattingFactory textFormattingFactory;
        private IDetectTextFormatService detectTextFormatService;

        public string ApplicationTitle
        {
            get
            {
                var assemblyVersion = Assembly.GetExecutingAssembly().GetName().Version;
                return string.Format("Formatted Text Viewer v{0}", assemblyVersion);
            }
        }

        public List<Tuple<TextFormattingTypes, string>> FormattingTypes { get; set; }

        public Tuple<TextFormattingTypes, string> SelectedFormattingType { get; set; }

        public string UnprocessedText { get; set; }

        public string UnprocessedTextSelected { get; set; }

        public string ProcessedText { get; set; }        

        public bool IsPlainTextTabSelected
        {
            get { return !IsFormattedTextTabSelected; }
            set { IsFormattedTextTabSelected = !value; }
        }

        public bool IsFormattedTextTabSelected { get; set; }

        public MainWindowViewModel(IDetectTextFormatService detectTextFormatService, ITextFormattingFactory textFormattingFactory)
        {
            this.detectTextFormatService = detectTextFormatService;
            this.textFormattingFactory = textFormattingFactory;

            FormattingTypes = new List<Tuple<TextFormattingTypes, string>>();
            FormattingTypes.Add(new Tuple<TextFormattingTypes, string>(TextFormattingTypes.Json, TextFormattingTypes.Json.ToString()));
            FormattingTypes.Add(new Tuple<TextFormattingTypes, string>(TextFormattingTypes.Xml, TextFormattingTypes.Xml.ToString()));
            FormattingTypes.Add(new Tuple<TextFormattingTypes, string>(TextFormattingTypes.Html, TextFormattingTypes.Html.ToString()));
            FormattingTypes.Add(new Tuple<TextFormattingTypes, string>(TextFormattingTypes.PlainText, TextFormattingTypes.PlainText.ToString()));
            NotifyOfPropertyChange(() => FormattingTypes);

            SelectedFormattingType = FormattingTypes.First();
            NotifyOfPropertyChange(() => SelectedFormattingType);
        }

        public void Process()
        {
            var textFormatingType = detectTextFormatService.DetectTextFormat(UnprocessedText);
            SelectedFormattingType = FormattingTypes.First(f => f.Item1 == textFormatingType);
            NotifyOfPropertyChange(() => SelectedFormattingType);

            var textFormattingStrategy = textFormattingFactory.FindTextFormattingStrategy(textFormatingType);
            ProcessedText = textFormattingStrategy.FormatString(UnprocessedText);
            NotifyOfPropertyChange(() => ProcessedText);
        }

        public void Clear()
        {
            UnprocessedText = string.Empty;
            NotifyOfPropertyChange(() => UnprocessedText);
        }

        public void CopyFromClipboard()
        {
            UnprocessedText = Clipboard.GetText(TextDataFormat.Text);
        }

        public void CopyToClipboard()
        {
            Clipboard.SetText(ProcessedText, TextDataFormat.Text);
        }
    }
}
