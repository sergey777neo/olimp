using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Olimp2019.Data.ViewModels;
using Olimp2019.Resources;

namespace Olimp2019.Repositories.Implementations
{
	public class ReportRepository : IReportRepository
	{
		private List<ReportItemViewModel> _Reports;
		public List<ReportItemViewModel> Reports {
			get {
				if (_Reports == null)
				{
					_Reports = new List<ReportItemViewModel>()
					{
						new ReportItemViewModel()
						{
							Name = "ReportUsers",
							Caption = OlimpResource.ReportUser
						}
					};
				}
				return _Reports;
			}
		}

		public ReportItemViewModel GetReport(string name)
		{
			return this.Reports.SingleOrDefault(r => r.Name == name);
		}
	}
}
