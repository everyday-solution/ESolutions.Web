using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ESolutions.Web.UI;

namespace ESolutions.Web.Tests
{
	public class ParameterTestClass4 : ActiveQueryBase<ParameterTestClass4>
	{
		public enum PageModes
		{
			Edit,
			Create
		}

		[UrlParameter]
		public PageModes Modes
		{
			get;
			set;
		}
	}
}
