using Microsoft.EntityFrameworkCore;
using Moq;
using Olimp2019.Data.Models;
using Olimp2019.Data.ViewModels.Reports;
using Olimp2019.Web.Pages.Reports;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Olimp2019.Tests.UnitTests
{
    public class ReportUsersPageTests
    {
        private List<User> Users = new List<User>();
        public ReportUsersPageTests()
        {
            Users = new List<User>() { 
                new User()
                {
                    FullName = "VL",
                    Email = "VL@a.a",
                    UserName = "VL"
                },
                new User()
                {
                    FullName = "DV",
                    Email = "DV@a.a",
                    UserName = "DV"
                },
                new User()
                {
                    FullName = "SM",
                    Email = "SM@a.a",
                    UserName = "SM"
                },
                new User()
                {
                    FullName = "DK",
                    Email = "DK@a.a",
                    UserName = "DK"
                }
            };
        }
        [Fact]
        public void OnGetAsync_PopulatesThePageModel_WithAListOfUsersSortedByUserName()
        {
            var optionsBuilder = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(nameof(Olimp2019));

            var mockAppDbContext = new Mock<ApplicationDbContext>(optionsBuilder.Options);

            var expectedUsers = Users;

            mockAppDbContext.Setup(
                db => db.GetUsersAsync()).Returns(Task.FromResult(expectedUsers));

            var pageModel = new ReportUsersModel(mockAppDbContext.Object);
            pageModel.OnGet();

            var actualUsers = Assert.IsAssignableFrom<List<UserReportItemViewModel>>(pageModel.FullReport);
            Assert.Equal(
                expectedUsers.OrderBy(m => m.UserName).Select(m => m.FullName),
                actualUsers.Select(m => m.FullName));
        }
    }
}
