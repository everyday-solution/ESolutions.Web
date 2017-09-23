using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ESolutions.Web.UI
{
	public class Page<QueryType> : ESolutions.Web.UI.Page
		where QueryType : ActiveQueryBase
	{
		//Fields
		#region cachedQuery
		/// <summary>
		/// The cached request add on
		/// </summary>
		private HttpRequestWrapper<QueryType> cachedRequestAddOn = null;
		#endregion

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
				if (this.cachedRequestAddOn == null)
				{
					this.cachedRequestAddOn = new HttpRequestWrapper<QueryType>(this.Request);
				}

				return this.cachedRequestAddOn;
			}
		}
		#endregion
	}

	public class Page : System.Web.UI.Page
	{
		//Fields
		#region manualRequestForTesting
		protected System.Web.HttpRequest manualRequestForTesting = null;
		#endregion

		#region manualResponseForTesting
		protected System.Web.HttpResponse manualResponseForTesting = null;
		#endregion

		#region redirectAction
		private Action<String> redirectAction = null;
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
				HttpResponseWrapper result = new HttpResponseWrapper(this.Response);

				if (redirectAction != null)
				{
					result.InitializeTestingEnvironment(this.redirectAction);
				}

				return result;
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
			System.Web.HttpResponse response,
			Action<String> redirectAction)
		{
			this.manualRequestForTesting = request;
			this.manualResponseForTesting = response;
			this.redirectAction = redirectAction;
		}
		#endregion
	}
}
