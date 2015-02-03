using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Input;
using System.Windows.Interactivity;
using System.Windows.Media;
using ElasticOps.Extensions;
using ICSharpCode.AvalonEdit;
using ICSharpCode.AvalonEdit.CodeCompletion;
using ICSharpCode.AvalonEdit.Document;
using ICSharpCode.AvalonEdit.Editing;
using ElasticOps.Parsing;
using ElasticOps;

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

    public class IntellisenseBehavior : Behavior<TextEditor>
    {
        protected override void OnAttached()
        {

            AssociatedObject.TextArea.TextEntering += TextEntering;
            AssociatedObject.TextArea.TextEntered += TextEntered;
            base.OnAttached();
        }

        CompletionWindow _completionWindow;

        void TextEntered(object sender, TextCompositionEventArgs e)
        {
            var intellisenseContext = Intellisense.TryComplete(AssociatedObject.TextArea.Document.Text, AssociatedObject.TextArea.Caret.Line,AssociatedObject.TextArea.Caret.Column);

            if (!intellisenseContext.Mode.IsNone && intellisenseContext.Suggestions.Length >0)
            {
                _completionWindow = new CompletionWindow(AssociatedObject.TextArea);
                IList<ICompletionData> data = _completionWindow.CompletionList.CompletionData;
                intellisenseContext.Suggestions.ForEach(x=> data.Add(new DSLCompletionData(intellisenseContext,x)));
                _completionWindow.Show();
                _completionWindow.Closed += delegate
                {
                    _completionWindow = null;
                };
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
