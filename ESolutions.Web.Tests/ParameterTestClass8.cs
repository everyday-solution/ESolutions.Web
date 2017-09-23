using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ESolutions.Web.UI;

namespace ESolutions.Web.Tests
{
	public class ParameterTestClass8 : ActiveQueryBase<ParameterTestClass8>
	{
		[UrlParameter(IsOptional = true)]
		public String P1
		{
			get;
			set;
		}

		[UrlParameter]
		public String P2
		{
			get;
			set;
		}
	}
}
