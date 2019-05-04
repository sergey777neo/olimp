using Olimp2019.Data.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace Olimp2019.Repositories
{
	public interface IReportRepository
	{
		List<ReportItemViewModel> Reports { get; }
		ReportItemViewModel GetReport(string name);
	}
}
