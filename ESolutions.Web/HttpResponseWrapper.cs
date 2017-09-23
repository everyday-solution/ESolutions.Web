using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ESolutions.Web.UI;

namespace ESolutions.Web
{
	public class HttpResponseWrapper
	{
		//Fields
		#region wrappedResponse
		private System.Web.HttpResponse wrappedResponse = null;
		#endregion

		#region redirectAction
		private Action<String> redirectAction = null;
		#endregion

		//Constructors
		#region HttpResponseWrapper
		internal HttpResponseWrapper(System.Web.HttpResponse response)
		{
			this.wrappedResponse = response;
		}
		#endregion

		//Methods
		#region Redirect
		public void Redirect<PageType>()
			where PageType : System.Web.UI.Page
		{
			String url = PageUrlAttribute.Get<PageType>();

			if (this.redirectAction == null)
			{
				this.wrappedResponse.Redirect(url);
			}
			else
			{
				this.redirectAction(url);
			}
		}
		#endregion

		#region Redirect
		public void Redirect<PageType>(ActiveQueryBase query)
			where PageType : System.Web.UI.Page
		{
			String url = PageUrlAttribute.Get<PageType>(query);

			if (this.redirectAction == null)
			{
				this.wrappedResponse.Redirect(url);
			}
			else
			{
				this.redirectAction(url);
			}
		}
		#endregion

		#region InitializeTestingEnvironment
		internal void InitializeTestingEnvironment(Action<string> redirectAction)
		{
			this.redirectAction = redirectAction;
		}
		#endregion
	}
}
