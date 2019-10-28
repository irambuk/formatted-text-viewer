using FormattedTextViewer.View.Services;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Windows;
using System.Windows.Input;

namespace FormattedTextViewer.View
{
    public interface IMainWindowViewModel
    {
    }
    
    //[AddINotifyPropertyChangedInterface]
    public class MainWindowViewModel : IMainWindowViewModel, INotifyPropertyChanged
    {
        private ITextFormattingFactory textFormattingFactory;
        private IDetectTextFormatService detectTextFormatService;

        private string _unprocessedText;

        public event PropertyChangedEventHandler PropertyChanged;

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

        public string UnprocessedText
        {
            get { return _unprocessedText; }
            set
            {
                _unprocessedText = value;
                Process();
            }
        }

        public string UnprocessedTextSelected { get; set; }

        public string ProcessedText { get; set; }

        public bool IsPlainTextTabSelected
        {
            get { return !IsFormattedTextTabSelected; }
            set { IsFormattedTextTabSelected = !value; }
        }

        public bool IsFormattedTextTabSelected { get; set; }

        public ICommand ClearCommand => new RelayCommand((object obj) => Clear());
        public ICommand CopyFromClipboardCommand => new RelayCommand((object obj) => CopyFromClipboard());
        public ICommand CopyToClipboardCommand => new RelayCommand((object obj) => CopyToClipboard());

        public MainWindowViewModel(IDetectTextFormatService detectTextFormatService, ITextFormattingFactory textFormattingFactory)
        {
            this.detectTextFormatService = detectTextFormatService;
            this.textFormattingFactory = textFormattingFactory;

            FormattingTypes = new List<Tuple<TextFormattingTypes, string>>();
            FormattingTypes.Add(new Tuple<TextFormattingTypes, string>(TextFormattingTypes.Json, TextFormattingTypes.Json.ToString()));
            FormattingTypes.Add(new Tuple<TextFormattingTypes, string>(TextFormattingTypes.Xml, TextFormattingTypes.Xml.ToString()));
            FormattingTypes.Add(new Tuple<TextFormattingTypes, string>(TextFormattingTypes.Html, TextFormattingTypes.Html.ToString()));
            FormattingTypes.Add(new Tuple<TextFormattingTypes, string>(TextFormattingTypes.PlainText, TextFormattingTypes.PlainText.ToString()));
            NotifyOfPropertyChange(() => nameof(FormattingTypes));

            SelectedFormattingType = FormattingTypes.First();
            NotifyOfPropertyChange(() => nameof(SelectedFormattingType));
        }

        public void Process()
        {
            var textFormatingType = detectTextFormatService.DetectTextFormat(UnprocessedText);
            SelectedFormattingType = FormattingTypes.First(f => f.Item1 == textFormatingType);
            NotifyOfPropertyChange(() => nameof(SelectedFormattingType));

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

            Process();
        }

        public void CopyToClipboard()
        {
            Clipboard.SetText(ProcessedText, TextDataFormat.Text);
        }

        private void NotifyOfPropertyChange(Func<string> func)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(func()));
        }
    }
}
