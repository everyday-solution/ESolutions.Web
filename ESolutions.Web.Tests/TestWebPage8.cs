using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ESolutions.Web.UI;

namespace ESolutions.Web.Tests
{
	public partial class VehicleMaster : ESolutions.Web.UI.MasterPage<VehicleMaster.Query>
	{
		[PageQuery]
		public class Query : ActiveQueryBase<Query>
		{
			#region VehicleGuid
			[UrlParameter]
			protected Guid VehicleGuid
			{
				get;
				set;
			}
			#endregion

			#region Vehicle
			public Guid Vehicle
			{
				get
				{
					return this.VehicleGuid;
				}
				set
				{
					this.VehicleGuid = value;
				}
			}
			#endregion
		}

		public override void ShowError(Exception ex)
		{
			
		}

		public override void ShowMessage(string message, MessageTypes messageType)
		{
			
		}
	}

	[PageUrl("~/TestWebPage8.aspx")]
	public partial class TestWebPage8 : ESolutions.Web.UI.Page<TestWebPage8.Query>
	{
		[PageQuery]
		public class Query : VehicleMaster.Query
		{
		}
	}
}
