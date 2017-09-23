using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ESolutions.Web.UI;

namespace ESolutions.Web.Tests
{
	[ESolutions.Web.UI.PageUrl("~/TestWebPage7.aspx")]
	public class TestWebPage7 : ESolutions.Web.UI.Page<TestWebPage7.Query>
	{
		[PageQuery]
		public class Query : BaseQuery
		{

		}
	}

	[PageQuery]
	public class BaseQuery : ActiveQueryBase<BaseQuery>
	{
		#region inner
		[UrlParameter]
		private String inner
		{
			get;
			set;
		}
		#endregion

		#region MyParameter
		public String MyParameter
		{
			get
			{
				return this.inner;
			}
			set
			{
				this.inner = value;
			}
		}
		#endregion
	}
}
