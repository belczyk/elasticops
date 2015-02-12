using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Interactivity;

namespace ElasticOps.Behaviors
{
    public class OnlyDigitsInTextBoxBehavior : Behavior<TextBox>
    {
        protected override void OnAttached()
        {
            AssociatedObject.KeyDown += AssociatedObject_KeyDown;

            base.OnAttached();
        }

        private void AssociatedObject_KeyDown(object sender, KeyEventArgs e)
        {
            if (!char.IsDigit((char) KeyInterop.VirtualKeyFromKey(e.Key)))
            {
                e.Handled = true;
            }
        }

        protected override void OnDetaching()
        {
            AssociatedObject.KeyDown -= AssociatedObject_KeyDown;

            base.OnDetaching();
        }
    }
}