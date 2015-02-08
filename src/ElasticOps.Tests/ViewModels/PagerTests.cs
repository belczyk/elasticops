using ElasticOps.ViewModels;
using Xunit;

namespace ElasticOps.Tests.ViewModels
{
    
    public class PagerTests
    {
        [Fact]
        public void Cant_get_prev_page_when_on_first_page()
        {
            var pagger = new PaggerViewModel {Page = 1};

            pagger.PrevPage();

            Assert.Equal(1,pagger.Page);
        }

        [Fact]
        public void Cant_get_next_page_when_on_last_page()
        {
            var pagger = new PaggerViewModel {TotalPages = 10, Page = 10};

            pagger.NextPage();

            Assert.Equal(10, pagger.Page);
        }

        [Fact]
        public void Can_get_next_page()
        {
            var pagger = new PaggerViewModel {TotalPages = 10, Page = 5};

            pagger.NextPage();

            Assert.Equal(6, pagger.Page);
        }

        [Fact]
        public void Can_get_prev_page()
        {
            var pagger = new PaggerViewModel {TotalPages = 10, Page = 5};

            pagger.PrevPage();

            Assert.Equal(4, pagger.Page);
        }

        [Fact]
        public void Calculates_number_of_total_pages_correctly()
        {
            var pagger = new PaggerViewModel();
            pagger.PageSize = 100;

            pagger.Total = 200;
            Assert.Equal(2,pagger.TotalPages);

            pagger.Total = 201;
            Assert.Equal(3, pagger.TotalPages);

            pagger.Total = 199;
            Assert.Equal(2, pagger.TotalPages);

            pagger.Total = 0;
            Assert.Equal(1, pagger.TotalPages);
        }

        [Fact]
        public void When_setting_new_total_current_page_is_not_bigger_then_max()
        {
            var pagger = new PaggerViewModel {PageSize = 100, Total = 1000, Page = 8};

            pagger.Total = 100;

            Assert.Equal(1,pagger.Page);
        }

        [Fact]
        public void When_page_size_changes_reset_page_to_1()
        {
            var pagger = new PaggerViewModel { PageSize = 100, Total = 1000, Page = 8 };

            pagger.PageSize = 50;

            Assert.Equal(1,pagger.Page);

        }

        [Fact]
        public void When_page_size_changes_total_pages_changes()
        {
            var pagger = new PaggerViewModel { PageSize = 100, Total = 1000, Page = 8 };

            pagger.PageSize = 50;

            Assert.Equal(1, pagger.Page);

        }

        [Fact]
        public void Cant_set_page_bigger_then_total_pages()
        {
            var pagger = new PaggerViewModel { PageSize = 100, Total = 1000, Page = 8 };

            pagger.Page = 100000000;

            Assert.Equal(10,pagger.Page);
        }

        [Fact]
        public void Calls_OnPageChanged_when_page_size_changes()
        {
            var pagger = new PaggerViewModel { PageSize = 100, Total = 1000, Page = 1 };
            var called = false;
            pagger.OnPageChanged = () => called = true;

            pagger.PageSize = 101;
            Assert.True(called);

            pagger.Page = 2;
            called = false;
            pagger.PageSize = 99;

            Assert.True(called);
        }
    }
}
