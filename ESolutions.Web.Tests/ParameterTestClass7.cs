using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ESolutions.Web.UI;

namespace ESolutions.Web.Tests
{
	public class ParameterTestClass7 : ActiveQueryBase<ParameterTestClass7>
	{
		[UrlParameter(IsOptional = true)]
		public String P1
		{
			get;
			set;
		}
	}
}
