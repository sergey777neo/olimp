using Olimp2019.Repositories;
using Olimp2019.Data.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Olimp2019.Tests.Stubs
{
	public class ReportRepositoryStub : IReportRepository
	{
		public List<ReportItemViewModel> Reports => new List<ReportItemViewModel>()
		{
			new ReportItemViewModel()
			{
				Name = "TestName1",
				Caption = "TestCaption1"
			},
			new ReportItemViewModel()
			{
				Name = "TestName2",
				Caption = "TestCaption2"
			}
		};

		public ReportItemViewModel GetReport(string name)
		{
			return Reports.SingleOrDefault(r => r.Name == name);
		}
	}
}
