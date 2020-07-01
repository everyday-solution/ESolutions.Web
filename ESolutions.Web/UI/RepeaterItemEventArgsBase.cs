using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Web;
using System.Web.UI.WebControls;

namespace ESolutions.Web.UI
{
	public abstract class RepeaterItemEventArgsBase<DataType>
		where DataType : class
	{
		//Constructors
		#region RepeaterItemEventArgsBase
		public RepeaterItemEventArgsBase(RepeaterItemEventArgs item)
		{
			this.item = item;
		}
		#endregion

		//Fields
		#region item
		protected readonly RepeaterItemEventArgs item = null;
		#endregion

		//Properties
		#region Data
		public DataType Data
		{
			get
			{
				return this.item.Item.DataItem as DataType;
			}
		}
		#endregion

		//Methods
		#region GetPropertyName
		public ControlType GetControl<ControlType>([CallerMemberName] String caller = null)
			where ControlType: System.Web.UI.Control
		{
			return this.item.Item.FindControl(caller) as ControlType;
		}
		#endregion

		#region ShowData
		public void ShowData(Object sender, Action showDataCallback)
		{
			if (this.item.Item.ItemType == ListItemType.Footer)
			{
				this.item.Item.Visible = (sender as Repeater).Items.Count <= 0;
			}
			else
			{
				if (this.Data != null)
				{
					showDataCallback();
				}
			}
		}
		#endregion
	}
}