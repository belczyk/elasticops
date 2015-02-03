using System;
using System.Windows.Media;
using ElasticOps.Extensions;
using ICSharpCode.AvalonEdit;
using ICSharpCode.AvalonEdit.CodeCompletion;
using ICSharpCode.AvalonEdit.Document;
using ICSharpCode.AvalonEdit.Editing;

namespace ElasticOps.Behaviors
{
    public class CompletionData : ICompletionData
    {
        public CompletionData(string text)
        {
            Text = text;
            Content = text;
        }

        public CompletionData(Intellisense.Context context , Intellisense.Suggestion suggestion)
        {
            Suggestion = suggestion;
            Context = context;
            Text = suggestion.Text;
            Content = suggestion.Text;
        }

        public void Complete(TextArea textArea, ISegment completionSegment, EventArgs insertionRequestEventArgs)
        {
            var result = Intellisense.Complete(Context, Suggestion);

            textArea.Document.Text = result.NewText;
            textArea.Caret.Position = new TextViewPosition(result.NewCaretPosition.Item1, result.NewCaretPosition.Item2);
        }

        public object Content { get; set; }

        public object Description { get; set; }

        public ImageSource Image { get; set; }

        public double Priority { get; set; }

        public string Text { get; set; }

        public Intellisense.Context Context { get; set; }

        public Intellisense.Suggestion Suggestion { get; set; }

        public override string ToString()
        {
            return Text;
        }
    }
}