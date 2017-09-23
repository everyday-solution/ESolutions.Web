using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ESolutions.Web.UI;

namespace ESolutions.Web.Tests
{
	[PageQuery]
	public class ParameterTestClass10 : ActiveQueryBase<ParameterTestClass10>
	{
		[UrlParameter]
		public Guid Guid
		{
			get;
			set;
		}
	}
}
