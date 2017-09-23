using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ESolutions.Web.UI;

namespace ESolutions.Web.Tests
{
	[PageQuery]
	public class ParameterTestClass12 : ActiveQueryBase<ParameterTestClass12>
	{
		#region Time
		[UrlParameter(IsOptional=true)]
		public DateTime? Time
		{
			get;
			set;
		}
		#endregion
	}
}
