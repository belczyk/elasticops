using System;
using System.Windows.Media;
using ICSharpCode.AvalonEdit;
using ICSharpCode.AvalonEdit.CodeCompletion;
using ICSharpCode.AvalonEdit.Document;
using ICSharpCode.AvalonEdit.Editing;

namespace ElasticOps.Behaviors
{
    public class CodeCompletionData : ICompletionData
    {
        public CodeCompletionData(string text)
        {
            Text = text;
            Content = text;
        }

        public CodeCompletionData(Context context, Suggestion suggestion)
        {
            if (suggestion == null)
                throw new ArgumentNullException("suggestion");

            Suggestion = suggestion;
            Context = context;
            Text = suggestion.Text;
            Content = suggestion.Text;
        }

        public void Complete(TextArea textArea, ISegment completionSegment, EventArgs insertionRequestEventArgs)
        {
            if (textArea == null)
                throw new ArgumentNullException("textArea");

            var result = Intellisense.Complete(Context, Suggestion);

            textArea.Document.Text = result.NewText;
            textArea.Caret.Position = new TextViewPosition(result.NewCaretPosition.Item1, result.NewCaretPosition.Item2);
            textArea.Caret.BringCaretToView();
        }

        public object Content { get; set; }

        public object Description { get; set; }

        public ImageSource Image { get; set; }

        public double Priority { get; set; }

        public string Text { get; set; }

        public Context Context { get; set; }

        public Suggestion Suggestion { get; set; }

        public override string ToString()
        {
            return Text;
        }
    }
}