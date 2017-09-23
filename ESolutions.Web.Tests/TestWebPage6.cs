using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ESolutions.Web.UI;

namespace ESolutions.Web.Tests
{
	[ESolutions.Web.UI.PageUrl("~/TestWebPage6.aspx")]
	public class TestWebPage6 : ESolutions.Web.UI.Page<TestWebPage6.PageQuery>
	{
		[PageQuery]
		public class PageQuery : ActiveQueryBase<PageQuery>
		{
			#region Id
			[UrlParameter]
			public Int32 Id
			{
				get;
				set;
			}
			#endregion
		}
	}
}
