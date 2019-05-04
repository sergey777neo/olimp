using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Olimp2019.Repositories;
using Olimp2019.Data.ViewModels;

namespace Olimp2019.Web.Pages.Reports
{
	public class ReportsModel : PageModel
	{
		private IReportRepository _reportRepository;
		public List<ReportItemViewModel> Reports { get; set; }

		public ReportsModel(IReportRepository reportRepository)
		{
			_reportRepository = reportRepository;
		}
		public void OnGet()
		{
			Reports = this._reportRepository.Reports;
		}
	}
}