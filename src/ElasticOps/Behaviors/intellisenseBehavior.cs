using System.Collections.Generic;
using System.Linq;
using System.Windows.Input;
using System.Windows.Interactivity;
using ElasticOps.Configuration;
using ElasticOps.Extensions;
using ElasticOps.Views;
using ICSharpCode.AvalonEdit;
using ICSharpCode.AvalonEdit.CodeCompletion;
using ICSharpCode.AvalonEdit.Editing;

namespace ElasticOps.Behaviors
{
    public class IntellisenseBehavior : Behavior<QueryView>
    {
        private TextArea _textEditor;
        private IntellisenseConfig _config;
        protected override void OnAttached()
        {
            _config = AppBootstrapper.GetInstance<IntellisenseConfig>();

            AssociatedObject.Loaded += AssociatedObject_Loaded;
            base.OnAttached();
        }

        void AssociatedObject_Loaded(object sender, System.Windows.RoutedEventArgs e)
        {
            _textEditor = AssociatedObject.QueryEditor.TextEditor.TextArea;

            _textEditor.TextEntered += TextEntered;
            _textEditor.TextEntering += TextEntering;
            _textEditor.KeyDown += KeyDown;
            _textEditor.KeyUp += KeyUp;
        }

        void KeyUp(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Delete || e.Key == Key.Back)
            {
                TryComplete();
            }
        }

        void KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Space && Keyboard.Modifiers == ModifierKeys.Control)
            {
                TryComplete();
                e.Handled = true;
            }
        }

        CompletionWindow _completionWindow;

        void TextEntered(object sender, TextCompositionEventArgs e)
        {
            TryCompleteChar(e.Text[0]);
            TryComplete();
        }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Globalization", "CA1303:Do not pass literals as localized parameters", MessageId = "ICSharpCode.AvalonEdit.Document.TextDocument.Insert(System.Int32,System.String)")]
        private void TryCompleteChar(char c)
        {
            if (c == '{' && _config.AutoCompleteChars.Contains("{"))
            {
                _textEditor.Document.Insert(_textEditor.Caret.Offset, "}");
                _textEditor.Caret.Position = new TextViewPosition(_textEditor.Caret.Line, _textEditor.Caret.Column - 1);
            }
            if (c == '"' && _config.AutoCompleteChars.Contains("\""))
            {
                _textEditor.Document.Insert(_textEditor.Caret.Offset, "\"");
                _textEditor.Caret.Position = new TextViewPosition(_textEditor.Caret.Line, _textEditor.Caret.Column - 1);
            }
            if (c == '[' && _config.AutoCompleteChars.Contains("["))
            {
                _textEditor.Document.Insert(_textEditor.Caret.Offset, "]");
                _textEditor.Caret.Position = new TextViewPosition(_textEditor.Caret.Line, _textEditor.Caret.Column - 1);
            }
        }

        private void TryComplete()
        {
            var endpoint = GetEndpoint();

            if (endpoint == null) return;
            if (!_config.IntellisenseEndpoints.Contains(endpoint)) return;

            var intellisenseResult = Intellisense.TrySuggest(_textEditor.Document.Text, _textEditor.Caret.Line, _textEditor.Caret.Column, endpoint);
            var context = intellisenseResult.Item1;
            var suggestions = intellisenseResult.Item2;

            if (suggestions != null)
            {
                _completionWindow = new CompletionWindow(_textEditor);
                IList<ICompletionData> data = _completionWindow.CompletionList.CompletionData;

                if (suggestions.Value.Any())
                {
                    suggestions.Value.ForEach(suggestion => data.Add(new CodeCompletionData(context, suggestion)));
                    _completionWindow.Show();
                    _completionWindow.Closed += delegate { _completionWindow = null; };
                }
            }
            else if (_completionWindow != null)
            {
                _completionWindow.Close();
            }
        }

        private string GetEndpoint()
        {
            var url = AssociatedObject.URL.Text;

            if (string.IsNullOrEmpty(url)) return null;

            var parts = url.Split('/');

            if (parts.Last().StartsWithIgnoreCase("_")) return parts.Last();

            return null;
        }

        void TextEntering(object sender, TextCompositionEventArgs e)
        {
            if (e.Text.Length > 0 && _completionWindow != null)
            {
                if (!char.IsLetterOrDigit(e.Text[0]))
                {
                    _completionWindow.CompletionList.RequestInsertion(e);
                }
            }
        }

    }
}
