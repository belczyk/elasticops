using System.Text;
using System.Text.RegularExpressions;
using Caliburn.Micro;

namespace ElasticOps.ViewModels.Controls
{
    public class CodeEditorViewModel : PropertyChangedBase
    {
        private string _code;
        private string _lineNumberGutter;

        public CodeEditorViewModel()
        {
            Code = "adfad";
        }

        public string Code
        {
            get { return _code; }
            set
            {
                if (value == _code) return;
                _code = value;
                NotifyOfPropertyChange(() => Code);
                NotifyOfPropertyChange(() => LineNumberGutter);

            }
        }


        public string LineNumberGutter
        {
            get
            {
                if (string.IsNullOrEmpty(Code)) return string.Empty;

                var lines = Regex.Matches(Code, "\n").Count;
                var builder = new StringBuilder();
                for (int i = 1; i < lines; i++)
                {
                    builder.AppendLine(i.ToString());
                }

                return builder.ToString();
            }
        }
    }
}
