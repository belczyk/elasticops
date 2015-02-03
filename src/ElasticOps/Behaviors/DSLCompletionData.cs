using System;
using System.Windows.Media;
using ElasticOps.Extensions;
using ICSharpCode.AvalonEdit;
using ICSharpCode.AvalonEdit.CodeCompletion;
using ICSharpCode.AvalonEdit.Document;
using ICSharpCode.AvalonEdit.Editing;

namespace ElasticOps.Behaviors
{
    public class DSLCompletionData : ICompletionData
    {
        public DSLCompletionData(string text)
        {
            Text = text;
            Content = text;
        }

        public DSLCompletionData(Intellisense.Context context , Intellisense.Suggestion suggestion)
        {
            Suggestion = suggestion;
            Content = context;
            Text = suggestion.Text;
            Content = suggestion.Text;
        }

        public void Complete(TextArea textArea, ISegment completionSegment, EventArgs insertionRequestEventArgs)
        {
            var caretPosition = textArea.Caret.Position;

            var text = textArea.Document.Text;
            var codeTillCaret = text.GetTextBeforePosition(textArea.Caret.Line, textArea.Caret.Column);
            var codeFromCaret = text.Substring(codeTillCaret.Length);
            var lastQuotePos = codeTillCaret.LastIndexOf("\"");
            var codeTillQuote = codeTillCaret.Substring(0, lastQuotePos+1);
            
            textArea.Document.Text = codeTillQuote + Text + "\" : " + codeFromCaret;
            textArea.Caret.Position = new TextViewPosition(caretPosition.Line,lastQuotePos+Text.Length);
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