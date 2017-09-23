using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ESolutions.Web.UI;

namespace ESolutions.Web.Tests
{
	public class ParameterTestClass6 : ActiveQueryBase<ParameterTestClass6>
	{
		[UrlParameter(IsOptional = false)]
		public String P1
		{
			get;
			set;
		}
	}
}
