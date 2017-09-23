using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ESolutions.Web.UI;

namespace ESolutions.Web.Tests
{
	[ESolutions.Web.UI.PageUrl("~/TestWebPage4.aspx")]
	public class TestWebPage4 : System.Web.UI.Page
	{
		public class Query : ActiveQueryBase<Query>
		{
			#region Id
			[UrlParameter]
			public Int32 Id
			{
				get;
				set;
			}
			#endregion

			#region Text
			[UrlParameter]
			public String Text
			{
				get;
				set;
			}
			#endregion
		}
	}
}
