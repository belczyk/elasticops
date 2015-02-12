using System.Collections.Generic;
using System.Linq;
using Caliburn.Micro;
using Action = System.Action;

namespace ElasticOps.ViewModels
{
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "Pagger")]
    public class PaggerViewModel : PropertyChangedBase
    {
        private long _total;
        private long _page;
        private long _totalPages;
        private long _pageSize;

        public PaggerViewModel()
        {
            PageSizes = new List<long> { 25, 50, 100, 250, 500, 1000 };
            PageSize = PageSizes.First();
            Page = 1;
            TotalPages = 1;
        }

        public IEnumerable<long> PageSizes { get; set; } 

        public long Total
        {
            get { return _total; }
            set
            {
                if (value == _total || value < 0) return;
                _total = value;

                UpdateTotalPages();

                NotifyOfPropertyChange(() => Total);
                NotifyOfPropertyChange(() => TotalPages);
            }
        }

        public long Page
        {
            get { return _page; }
            set
            {
                if (value == _page) return;

                if (value > TotalPages)
                    value = TotalPages;
                _page = value;

                if (OnPageChanged != null)
                    OnPageChanged();

                NotifyOfPropertyChange(() => Page);
            }
        }

        public long TotalPages
        {
            get { return _totalPages; }
            set
            {
                if (value == _totalPages) return;
                _totalPages = value;
                NotifyOfPropertyChange(() => TotalPages);
            }
        }

        public long PageSize
        {
            get { return _pageSize; }
            set
            {
                if (value == _pageSize || value < 1) return;
                _pageSize = value;

                if (Page == 1 && OnPageChanged != null)
                    OnPageChanged();

                Page = 1;
                UpdateTotalPages();

                NotifyOfPropertyChange(() => PageSize);
            }
        }

        public void NextPage()
        {
            if (Page < TotalPages)
                Page += 1;
        }

        public void PreviousPage()
        {
            if (Page > 1)
                Page -= 1;
        }

        private void UpdateTotalPages()
        {
            _totalPages = Total / PageSize;

            if (Total % PageSize > 0)
                _totalPages += 1;

            if (_totalPages < 1)
                _totalPages = 1;

            if (Page > TotalPages)
                Page = TotalPages;
        }

        public Action OnPageChanged { get; set; }
    }
}
