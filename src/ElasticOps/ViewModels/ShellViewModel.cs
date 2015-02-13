using System.Collections.Generic;
using System.Linq;
using System.Windows.Media;
using Caliburn.Micro;
using ElasticOps.Com;
using ElasticOps.ViewModels.Controls;
using MahApps.Metro;

namespace ElasticOps.ViewModels
{
    public class ShellViewModel : Conductor<object>.Collection.OneActive, IHandle<GoToStudioEvent>,
        IHandle<ThemeChangedEvent>, IHandle<AccentChangedEvent>, IHandle<PreviewValueEvent>
    {
        private readonly Infrastructure _infrastructure;
        private readonly StudioViewModel studioViewModel;
        private FooterViewModel _footer;
        private bool _isValuePreviewFlayoutOpen;


        public ShellViewModel(
            Infrastructure infrastructure,
            StudioViewModel studioViewModel,
            FooterViewModel footer,
            CodeEditorViewModel valuePreviewModel
            )
        {
            Ensure.ArgumentNotNull(infrastructure, "infrastructure");
            Ensure.ArgumentNotNull(valuePreviewModel, "valuePreviewModel");

            _infrastructure = infrastructure;
            this.studioViewModel = studioViewModel;
            Footer = footer;

            valuePreviewModel.IsReadOnly = true;
            ValuePreviewModel = valuePreviewModel;
            base.DisplayName = "Elastic Ops";

            infrastructure.EventAggregator.Subscribe(this);


            AccentColors = ThemeManager.Accents
                .Select(
                    a =>
                        new AccentColorMenuData {Name = a.Name, ColorBrush = a.Resources["AccentColorBrush"] as Brush})
                .ToList();

            AppThemes = ThemeManager.AppThemes
                .Select(
                    a =>
                        new AppThemeMenuData
                        {
                            Name = a.Name,
                            BorderColorBrush = a.Resources["BlackColorBrush"] as Brush,
                            ColorBrush = a.Resources["WhiteColorBrush"] as Brush
                        })
                .ToList();

            base.ActivateItem(studioViewModel);
        }

        public CodeEditorViewModel ValuePreviewModel { get; set; }

        public bool IsValuePreviewFlayoutOpen
        {
            get { return _isValuePreviewFlayoutOpen; }
            set
            {
                if (value.Equals(_isValuePreviewFlayoutOpen)) return;
                _isValuePreviewFlayoutOpen = value;
                NotifyOfPropertyChange(() => IsValuePreviewFlayoutOpen);
            }
        }

        public FooterViewModel Footer
        {
            get { return _footer; }
            set
            {
                _footer = value;
                NotifyOfPropertyChange(() => Footer);
            }
        }

        public IEnumerable<AccentColorMenuData> AccentColors { get; private set; }
        public IEnumerable<AppThemeMenuData> AppThemes { get; private set; }

        public void Handle(AccentChangedEvent message)
        {
            Ensure.ArgumentNotNull(message, "message");

            _infrastructure.Config.Appearance.Accent = message.Accent;
            _infrastructure.Config.Save(Predef.ConfigPath);
        }

        public void Handle(GoToStudioEvent message)
        {
            ActivateItem(studioViewModel);
        }

        public void Handle(PreviewValueEvent message)
        {
            Ensure.ArgumentNotNull(message, "message");

            ValuePreviewModel.Code = message.Value;
            IsValuePreviewFlayoutOpen = true;
        }

        public void Handle(ThemeChangedEvent message)
        {
            Ensure.ArgumentNotNull(message, "message");

            _infrastructure.Config.Appearance.Theme = message.Theme;
            _infrastructure.Config.Save(Predef.ConfigPath);
        }
    }
}