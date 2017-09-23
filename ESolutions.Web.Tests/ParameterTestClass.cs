using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ESolutions.Web.UI;

namespace ESolutions.Web.Tests
{
	public class ParameterTestClass : ActiveQueryBase<ParameterTestClass>
	{

		[UrlParameterAttribute(SupportedValues = new String[] { "1", "2" })]
		public Int32 Id
		{
			get;
			set;
		}

		[UrlParameterAttribute]
		public String AssignedName
		{
			get;
			set;
		}
	}
}
