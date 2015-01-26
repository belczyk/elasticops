using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Xml;
using ICSharpCode.AvalonEdit.Folding;
using ICSharpCode.AvalonEdit.Highlighting;
using ICSharpCode.AvalonEdit.Highlighting.Xshd;

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
            using (Stream s = (GetType()).Assembly.GetManifestResourceStream("ElasticOps.JavaScript.xshd"))
            {
                using (XmlTextReader reader = new XmlTextReader(s))
                {
                    textEditor.SyntaxHighlighting = HighlightingLoader.Load(reader, HighlightingManager.Instance);
                }

            }

            textEditor.Foreground = Brushes.AntiqueWhite;
            textEditor.TextArea.TextView.LinkTextForegroundBrush = new SolidColorBrush(Color.FromRgb(0x68,0xCE,0xF9));
            var foldingStrategy = new BraceFoldingStrategy();
            var foldingManager = FoldingManager.Install(textEditor.TextArea);
            foldingStrategy.UpdateFoldings(foldingManager, textEditor.Document);

            textEditor.TextChanged += (sender, args) =>
            {
                foldingStrategy.UpdateFoldings(foldingManager, textEditor.Document);
            };
        }
    }
}
