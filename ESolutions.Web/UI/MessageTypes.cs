using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ESolutions.Web.UI
{
	#region MessageTypes
	/// <summary>
	/// The different types of messages
	/// </summary>
	public enum MessageTypes : int
	{
		Success,
		Warning,
		Error,
		Exception,
		Info
	}
	#endregion
}
