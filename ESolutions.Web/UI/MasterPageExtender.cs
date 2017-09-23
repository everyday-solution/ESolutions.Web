using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ESolutions.Web.UI
{
	public static class MasterPageExtender
	{
		#region FindMainMaster
		/// <summary>
		/// Traverses the hierarchy of masterpages from the specified one and returns the 
		/// top-most of specialized type ESolutions.Web.UI.MasterPage
		/// </summary>
		/// <param name="master">The master.</param>
		/// <returns></returns>
		public static ESolutions.Web.UI.MasterPage FindTopMostEsolutionsMaster(this System.Web.UI.MasterPage master)
		{
			ESolutions.Web.UI.MasterPage result = null;
			System.Web.UI.MasterPage runner = master;

			while (runner != null)
			{
				if (runner is ESolutions.Web.UI.MasterPage)
				{
					result = runner as ESolutions.Web.UI.MasterPage;
				}
				runner = runner.Master;
			}

			return result;
		}
		#endregion

		#region ShowError
		/// <summary>
		/// Shows an exception on the master page.
		/// </summary>
		/// <param name="master">The master.</param>
		/// <param name="ex">The ex.</param>
		public static void ShowError(this System.Web.UI.MasterPage master, Exception ex)
		{
			ESolutions.Web.UI.MasterPage eMaster = master.FindTopMostEsolutionsMaster();

			if (eMaster != null)
			{
				eMaster.ShowError(ex);
			}
		}
		#endregion

		#region ShowMessage
		/// <summary>
		/// Shows the message on the top most master page.
		/// </summary>
		/// <param name="master">The master.</param>
		/// <param name="message">The message.</param>
		/// <param name="messageTpye">The message tpye.</param>
		public static void ShowMessage(this System.Web.UI.MasterPage master, String message, MessageTypes messageType)
		{
			ESolutions.Web.UI.MasterPage eMaster = master.FindTopMostEsolutionsMaster();

			if (eMaster != null)
			{
				eMaster.ShowMessage(message, messageType);
			}
		}
		#endregion
	}
}
