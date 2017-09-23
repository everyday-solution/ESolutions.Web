using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ESolutions.Web.UI
{
	public static class ControlExtender
	{
		#region FindControl
		public static T FindControl<T>(this System.Web.UI.Control parentControl, String id)
			where T : System.Web.UI.Control
		{
			System.Web.UI.Control parent = parentControl ?? throw new Exception(ExceptionStringTable.FindControlParentInvalid);
			System.Web.UI.Control control = parent.FindControl(id) ?? throw new Exception(String.Format(ExceptionStringTable.FindControlIdNotFound, id));
			T castedControl = null;
			try
			{
				castedControl = (T)control;
			}
			catch
			{
				castedControl = (T)control ?? throw new Exception(String.Format(ExceptionStringTable.FindControlTypeDoesNotMatch, control.GetType().Name, typeof(T).Name));
			}

			return castedControl;
		}
		#endregion
	}
}
