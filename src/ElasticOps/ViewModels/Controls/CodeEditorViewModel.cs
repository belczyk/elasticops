using System;
using System.Windows.Media;
using System.Xml;
using Caliburn.Micro;
using ElasticOps.Com;
using ICSharpCode.AvalonEdit.Highlighting;
using ICSharpCode.AvalonEdit.Highlighting.Xshd;

namespace ElasticOps.ViewModels.Controls
{
    public class CodeEditorViewModel : PropertyChangedBase, IHandle<ThemeChangedEvent>
    {
        private readonly Infrastructure _infrastructure;
        private string _code;
        private IHighlightingDefinition _highlightingDefinition;
        private Brush _foreground;
        private bool _isReadOnly;

        public CodeEditorViewModel(Infrastructure infrastructure)
        {
            _infrastructure = infrastructure;
            _infrastructure.EventAggregator.Subscribe(this);
            LoadHighlightRules(infrastructure.Config.Appearance.Theme == "BaseDark" ? Theme.Dark : Theme.Light);
        }

        private enum Theme
        {
            Dark,
            Light
        }

        private void LoadHighlightRules(Theme theme)
        {
            Foreground = theme == Theme.Dark ? Brushes.AntiqueWhite : Brushes.Navy;

            using (var s = (GetType()).Assembly.GetManifestResourceStream(String.Format("ElasticOps.Query{0}HighlightingRules.xshd", theme)))
            {
                using (var reader = new XmlTextReader(s))
                {
                    HighlightingDefinition = HighlightingLoader.Load(reader, HighlightingManager.Instance);
                }
            }
        }

        public string Code
        {
            get { return _code; }
            set
            {
                if (value == _code) return;
                _code = value;
                NotifyOfPropertyChange(() => Code);
            }
        }

        public Brush Foreground
        {
            get
            {
                return _foreground;
            }
            set
            {
                if (Equals(value, _foreground)) return;
                _foreground = value;
                NotifyOfPropertyChange(() => Foreground);
            }
        }

        public IHighlightingDefinition HighlightingDefinition
        {
            get { return _highlightingDefinition; }
            set
            {
                if (Equals(value, _highlightingDefinition)) return;
                _highlightingDefinition = value;
                NotifyOfPropertyChange(() => HighlightingDefinition);
            }
        }

        public void Handle(ThemeChangedEvent message)
        {
            LoadHighlightRules(message.IsDark ? Theme.Dark : Theme.Light);
        }

        public bool IsReadOnly
        {
            get { return _isReadOnly; }
            set
            {
                if (value.Equals(_isReadOnly)) return;
                _isReadOnly = value;
                NotifyOfPropertyChange(() => IsReadOnly);
            }
        }
    }
}
