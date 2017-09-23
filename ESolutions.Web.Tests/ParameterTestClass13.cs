using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ESolutions.Web.UI;

namespace ESolutions.Web.Tests
{
	[PageQuery]
	public class ParameterTestClass13 : ActiveQueryBase<ParameterTestClass13>
	{
		#region Time
		[UrlParameter]
		public List<Int32> Id
		{
			get;
			set;
		}
		#endregion
	}
}
