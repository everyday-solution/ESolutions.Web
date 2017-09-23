using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ESolutions.Web.UI;

namespace ESolutions.Web.Tests
{
	[PageUrl("~/TestWebPage9.aspx")]
	public partial class TestWebPage9 : ESolutions.Web.UI.Page<TestWebPage8.Query>
	{
		[PageQuery]
		public class Query : ActiveQueryBase<Query>
		{
			[UrlParameter]
			public DateTime Time
			{
				get;
				set;
			}
		}
	}
}
