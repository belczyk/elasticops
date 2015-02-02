using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Windows.Interactivity;
using System.Windows.Media;
using ICSharpCode.AvalonEdit;
using ICSharpCode.AvalonEdit.CodeCompletion;
using ICSharpCode.AvalonEdit.Document;
using ICSharpCode.AvalonEdit.Editing;
using ElasticOps.Parsing;

namespace ElasticOps.Behaviors
{
    public class DSLCompletionData : ICompletionData
    {
        public DSLCompletionData(string text)
        {
            Text = text;
            Content = text;
        }

        public void Complete(TextArea textArea, ISegment completionSegment, EventArgs insertionRequestEventArgs)
        {
            textArea.Document.Replace(completionSegment, string.Format("\"{0}\" : {{\n\n}}", Text));
        }

        public object Content { get; set; }

        public object Description { get; set; }

        public ImageSource Image { get; set; }

        public double Priority { get; set; }

        public string Text { get; set; }

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
            var parseTree = Processing.parse(AssociatedObject.TextArea.Document.Text);
            
            if (Processing.endsOnPropertyName(parseTree.Value))
            {
                _completionWindow = new CompletionWindow(AssociatedObject.TextArea);
                IList<ICompletionData> data = _completionWindow.CompletionList.CompletionData;
                data.Add(new DSLCompletionData("query"));
                data.Add(new DSLCompletionData("query_match_all"));
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
