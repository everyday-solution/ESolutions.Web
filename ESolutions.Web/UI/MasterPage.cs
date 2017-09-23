using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ESolutions.Web.UI
{
	public abstract class MasterPage<QueryType> : ESolutions.Web.UI.MasterPage
	where QueryType : ActiveQueryBase
	{
		//Properties
		#region RequestAddOn
		/// <summary>
		/// Gets the an extended HttpRequest class that allows to access
		/// querys via the activequery-framework
		/// </summary>
		/// <value>The request add on.</value>
		public HttpRequestWrapper<QueryType> RequestAddOn
		{
			get
			{
				return new HttpRequestWrapper<QueryType>(this.Request);
			}
		}
		#endregion
	}

	public abstract class MasterPage : System.Web.UI.MasterPage
	{
		//Fields
		#region manualRequestForTesting
		protected System.Web.HttpRequest manualRequestForTesting = null;
		#endregion

		#region manualResponseForTesting
		protected System.Web.HttpResponse manualResponseForTesting = null;
		#endregion

		//Properties
		#region Request
		/// <summary>
		/// Gets the <see cref="T:System.Web.HttpRequest"/> object for the requested page.
		/// </summary>
		/// <value></value>
		/// <returns>
		/// The currentProperty <see cref="T:System.Web.HttpRequest"/> associated with the page.
		/// </returns>
		/// <exception cref="T:System.Web.HttpException">
		/// Occurs when the <see cref="T:System.Web.HttpRequest"/> object is not available.
		/// </exception>
		public new System.Web.HttpRequest Request
		{
			get
			{
				System.Web.HttpRequest result = null;

				if (this.manualRequestForTesting == null)
				{
					result = base.Request;
				}
				else
				{
					result = this.manualRequestForTesting;
				}

				return result;
			}
		}
		#endregion

		#region Response
		/// <summary>
		/// Gets the <see cref="T:System.Web.HttpResponse"/> object associated with the <see cref="T:System.Web.UI.Page"/> object. This object allows you to send HTTP response data to a client and contains information about that response.
		/// </summary>
		/// <value></value>
		/// <returns>
		/// The current <see cref="T:System.Web.HttpResponse"/> associated with the page.
		/// </returns>
		/// <exception cref="T:System.Web.HttpException">
		/// The <see cref="T:System.Web.HttpResponse"/> object is not available.
		/// </exception>
		public new System.Web.HttpResponse Response
		{
			get
			{
				System.Web.HttpResponse result = null;

				if (this.manualResponseForTesting == null)
				{
					result = base.Response;
				}
				else
				{
					result = this.manualResponseForTesting;
				}

				return result;
			}
		}
		#endregion

		#region ResponseAddOn
		/// <summary>
		/// Gets the an extended HttpRequest class that allows to access
		/// querys via the activequery-framework
		/// </summary>
		/// <value>The response add on.</value>
		public HttpResponseWrapper ResponseAddOn
		{
			get
			{
				return new HttpResponseWrapper(this.Response);
			}
		}
		#endregion

		//Methods
		#region SetRequestForTesting
		/// <summary>
		/// Sets the request of the class when operating in a testing environment.
		/// </summary>
		/// <param name="request">The request.</param>
		internal void InitializeTestingEnvironment(
			System.Web.HttpRequest request,
			System.Web.HttpResponse response)
		{
			this.manualRequestForTesting = request;
			this.manualResponseForTesting = response;
		}
		#endregion

		#region ShowError
		/// <summary>
		/// Must be implemented. Gives the masterpage the opportunity to display an exception.
		/// </summary>
		/// <param name="ex">The ex.</param>
		public abstract void ShowError(Exception ex);
		#endregion

		#region ShowMessage
		public abstract void ShowMessage(String message, MessageTypes messageType);
		#endregion

		#region SetTitleIfEmpty
		/// <summary>
		/// Sets the title of the page if it is empty.
		/// </summary>
		/// <param name="title">The title.</param>
		public void SetTitleIfEmpty(String title)
		{
			if (String.IsNullOrWhiteSpace(this.Page.Title))
			{
				this.Page.Title = title;
			}
		}
		#endregion
	}
}
