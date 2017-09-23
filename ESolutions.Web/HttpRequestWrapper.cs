using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ESolutions.Web.UI;

namespace ESolutions.Web
{
	public class HttpRequestWrapper<QueryType>
		where QueryType : ActiveQueryBase
	{
		//Fields
		#region wrappedRequest
		private System.Web.HttpRequest wrappedRequest = null;
		#endregion

		#region cachedQuery
		private QueryType cachedQuery = null;
		#endregion

		//Properties
		#region Query
		/// <summary>
		/// Get the query of the request converted into a class by the activequery-framework
		/// </summary>
		/// <value>The query.</value>
		public QueryType Query
		{
			get
			{
				if (this.cachedQuery == null)
				{
					this.cachedQuery = ActiveQueryBase.Deserialize(this.wrappedRequest, typeof(QueryType)) as QueryType;
				}

				return this.cachedQuery;
			}
		}
		#endregion

		//Constructors
		#region HttpRequestWrapper
		/// <summary>
		/// Initializes a new instance of the <see cref="HttpRequestWrapper&lt;QueryType&gt;"/> class.
		/// </summary>
		/// <param name="originalRequest">The original request.</param>
		public HttpRequestWrapper(System.Web.HttpRequest originalRequest)
		{
			this.wrappedRequest = originalRequest;
		}
		#endregion
	}
}
