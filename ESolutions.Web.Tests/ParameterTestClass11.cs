using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ESolutions.Web.UI;

namespace ESolutions.Web.Tests
{
	[PageQuery]
	public class ParameterTestClass11 : ActiveQueryBase<ParameterTestClass11>
	{
		#region Time
		[UrlParameter]
		public DateTime Time
		{
			get;
			set;
		}
		#endregion
	}
}
