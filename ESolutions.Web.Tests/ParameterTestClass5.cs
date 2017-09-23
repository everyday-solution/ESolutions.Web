using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ESolutions.Web.UI;

namespace ESolutions.Web.Tests
{
	public class ParameterTestClass5 : ActiveQueryBase<ParameterTestClass5>
	{
		[UrlParameter(IsOptional = true)]
		public String P1
		{
			get;
			set;
		}
	}
}
