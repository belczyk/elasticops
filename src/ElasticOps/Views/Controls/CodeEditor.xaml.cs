using System.Windows.Controls;
using System.Windows.Media;
using ICSharpCode.AvalonEdit.Folding;

namespace ElasticOps.Views.Controls
{

    /// <summary>
    /// Interaction logic for CodeEditor.xaml
    /// </summary>
    public partial class CodeEditor : UserControl
    {
        public CodeEditor()
        {
            InitializeComponent();

            TextEditor.TextArea.TextView.LinkTextForegroundBrush = new SolidColorBrush(Color.FromRgb(0x68,0xCE,0xF9));

            SetupFolding();
        }

        private void SetupFolding()
        {
            var foldingStrategy = new BraceFoldingStrategy();
            var foldingManager = FoldingManager.Install(TextEditor.TextArea);
            foldingStrategy.UpdateFoldings(foldingManager, TextEditor.Document);
            TextEditor.TextChanged += (sender, args) => { foldingStrategy.UpdateFoldings(foldingManager, TextEditor.Document); };
        }
    }
}
