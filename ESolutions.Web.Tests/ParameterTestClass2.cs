using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ESolutions.Web.UI;

namespace ESolutions.Web.Tests
{
	public class ParameterTestClass2 : ActiveQueryBase<ParameterTestClass2>
	{
		[UrlParameterAttribute(SupportedValues = new String[] { "2" }, IsOptional = true)]
		public Int32? Id
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
