using System.Collections.Generic;
using System.Linq;
using System.Windows.Input;
using System.Windows.Interactivity;
using ElasticOps.Extensions;
using ICSharpCode.AvalonEdit;
using ICSharpCode.AvalonEdit.CodeCompletion;

namespace ElasticOps.Behaviors
{
    public class IntellisenseBehavior : Behavior<TextEditor>
    {
        protected override void OnAttached()
        {

            AssociatedObject.TextArea.TextEntered += TextEntered;
            AssociatedObject.TextArea.TextEntering += TextEntering;
            base.OnAttached();
        }

        CompletionWindow _completionWindow;

        void TextEntered(object sender, TextCompositionEventArgs e)
        {

            var intellisenseResult = Intellisense.TrySuggest(AssociatedObject.TextArea.Document.Text, AssociatedObject.TextArea.Caret.Line, AssociatedObject.TextArea.Caret.Column);
            var context = intellisenseResult.Item1;
            var suggestions = intellisenseResult.Item2;

            if (!context.Mode.IsNone)
            {
                _completionWindow = new CompletionWindow(AssociatedObject.TextArea);
                IList<ICompletionData> data = _completionWindow.CompletionList.CompletionData;

                if (suggestions!=null && suggestions.Value.Any())
                {
                    suggestions.Value.ForEach(suggestion => data.Add(new CompletionData(context, suggestion)));
                    _completionWindow.Show();
                    _completionWindow.Closed += delegate
                    {
                        _completionWindow = null;
                    };

                }
                else if (_completionWindow != null)
                {
                    _completionWindow.Close();
                }

            }
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
