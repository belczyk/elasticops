using System;
using System.Windows;
using System.Windows.Interactivity;
using ICSharpCode.AvalonEdit;

namespace ElasticOps.Behaviors
{
    public sealed class AvalonEditBehavior : Behavior<TextEditor>
    {
        public static readonly DependencyProperty CodeProperty =
            DependencyProperty.Register("Code", typeof (string), typeof (AvalonEditBehavior),
                new FrameworkPropertyMetadata(default(string), FrameworkPropertyMetadataOptions.BindsTwoWayByDefault,
                    PropertyChangedCallback));

        public string Code
        {
            get { return (string) GetValue(CodeProperty); }
            set { SetValue(CodeProperty, value); }
        }

        protected override void OnAttached()
        {
            base.OnAttached();
            if (AssociatedObject != null)
                AssociatedObject.TextChanged += AssociatedObjectOnTextChanged;
        }

        protected override void OnDetaching()
        {
            base.OnDetaching();
            if (AssociatedObject != null)
                AssociatedObject.TextChanged -= AssociatedObjectOnTextChanged;
        }

        private void AssociatedObjectOnTextChanged(object sender, EventArgs eventArgs)
        {
            var textEditor = sender as TextEditor;
            if (textEditor != null)
            {
                if (textEditor.Document != null)
                    Code = textEditor.Document.Text;
            }
        }

        private static void PropertyChangedCallback(
            DependencyObject dependencyObject,
            DependencyPropertyChangedEventArgs dependencyPropertyChangedEventArgs)
        {
            var behavior = dependencyObject as AvalonEditBehavior;
            if (behavior.AssociatedObject != null)
            {
                var editor = behavior.AssociatedObject as TextEditor;
                if (editor.Document != null)
                {
                    var caretOffset = editor.CaretOffset;
                    editor.Document.Text = dependencyPropertyChangedEventArgs.NewValue.ToString();
                    editor.CaretOffset = caretOffset;
                }
            }
        }
    }
}