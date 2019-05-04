using Olimp2019.Data.ViewModels;
using Olimp2019.Tests.Stubs;
using Olimp2019.Web.Pages;
using Olimp2019.Web.Pages.Reports;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Olimp2019.Tests.UnitTests
{
    public class ReportsPageTests
    {
            [Fact]
            public bool OnGetAsync_PopulatesThePageModel_WithAListOfReports()
            {
                var repository = new ReportRepositoryStub();
                var pageModel = new ReportsModel(repository);
            
                pageModel.OnGet();
            
                var actualReports = Assert.IsAssignableFrom<List<ReportItemViewModel>>(pageModel.Reports);

                Assert.Equal(
                        repository.Reports.OrderBy(m => m.Name).Select(m => m.Name),
                        actualReports.OrderBy(m => m.Name).Select(m => m.Name));

                return true;
            }
        }
}
