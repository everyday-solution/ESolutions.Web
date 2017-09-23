using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ESolutions.Web.UI;

namespace ESolutions.Web.Tests
{
	public class ParameterTestClass3 : ActiveQueryBase<ParameterTestClass3>
	{
		[UrlParameterAttribute(SupportedValues = new String[] { "2" })]
		private Int32? Id
		{
			get;
			set;
		}

		public Int32? IdAccessor()
		{
			return this.Id;
		}
	}
}
