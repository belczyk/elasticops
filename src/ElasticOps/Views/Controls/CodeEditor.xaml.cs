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

            textEditor.TextArea.TextView.LinkTextForegroundBrush = new SolidColorBrush(Color.FromRgb(0x68,0xCE,0xF9));

            SetupFolding();
        }

        private void SetupFolding()
        {
            var foldingStrategy = new BraceFoldingStrategy();
            var foldingManager = FoldingManager.Install(textEditor.TextArea);
            foldingStrategy.UpdateFoldings(foldingManager, textEditor.Document);
            textEditor.TextChanged += (sender, args) => { foldingStrategy.UpdateFoldings(foldingManager, textEditor.Document); };
        }
    }
}
