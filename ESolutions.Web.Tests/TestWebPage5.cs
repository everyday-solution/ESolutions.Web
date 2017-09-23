using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ESolutions.Web.UI;

namespace ESolutions.Web.Tests
{
	[ESolutions.Web.UI.PageUrl("~/TestWebPage5.aspx")]
	public class TestWebPage5 : System.Web.UI.Page
	{
		[PageQuery]
		public class Query : ActiveQueryBase<Query>
		{
		}

		[PageQuery]
		public class Query2 : Object
		{
		}
	}
}
