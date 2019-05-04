using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Olimp2019.Data.Models;
using Olimp2019.Data.ViewModels.Reports;

namespace Olimp2019.Web.Pages.Reports
{
    public class ReportUsersModel : PageModel
    {
        private readonly ApplicationDbContext _context;
        public List<UserReportItemViewModel> FullReport { get; set; }


        public ReportUsersModel(ApplicationDbContext context)
        {
            _context = context;
        }

        public void OnGet()
        {
            FullReport = new List<UserReportItemViewModel>();

            var users = _context.GetUsersAsync().Result;

            FullReport.AddRange(users.OrderBy(u => u.UserName).Select(u => new UserReportItemViewModel()
            {
                UserName = u.UserName,
                FullName = u.FullName,
                Email = u.Email
            }));
        }
    }
}