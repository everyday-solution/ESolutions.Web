using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ESolutions.Web.UI;

namespace ESolutions.Web.Tests
{
	[ESolutions.Web.UI.PageUrl("~/TestWebPage2.aspx")]
	public class TestWebPage2 : System.Web.UI.Page
	{
		[PageQuery]
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
