using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ESolutions.Web.UI
{
	public static class PageExtender
	{
		#region GetUrl
		/// <summary>
		/// Return the URL assigned to the page by the PageUrl-Attribute.
		/// The resulting path is realtive to the current host.
		/// </summary>
		/// <param name="page">The currentProperty page.</param>
		/// <returns>The application relative url to the page.</returns>
		public static String GetUrl(this System.Web.UI.Page page)
		{
			return PageUrlAttribute.Get(page.GetType());
		}
		#endregion

		#region GetUrl
		/// <summary>
		/// Return the URL assigned to the page by the PageUrl-Attribute combined
		/// with the querystring generated from the provided query object. 
		/// The resulting path is realtive to the current host.
		/// </summary>
		/// <param name="page">The page.</param>
		/// <param name="query">The query.</param>
		/// <returns>The application relative page url with specified query parameters</returns>
		public static String GetUrl(this System.Web.UI.Page page, ActiveQueryBase query)
		{
			return PageUrlAttribute.Get(page.GetType(), query);
		}
		#endregion

		#region GetUrlAbsolute
		/// <summary>
		/// Return the URL assigned to the page by the PageUrl-Attribute.
		/// The resulting path is the absolute path to the page including host.
		/// </summary>
		/// <param name="page">The currentProperty page.</param>
		/// <returns>The application relative url to the page.</returns>
		public static String GetUrlAbsolute(this System.Web.UI.Page page)
		{
			return PageUrlAttribute.GetAbsolute(page.GetType(), page);
		}
		#endregion

		#region GetUrlAbsolute
		/// <summary>
		/// Return the URL assigned to the page by the PageUrl-Attribute combined
		/// with the querystring generated from the provided query object. 
		/// The resulting path is the absolute path to the page including host.
		/// </summary>
		/// <param name="page">The page.</param>
		/// <param name="query">The query.</param>
		/// <returns>The application relative page url with specified query parameters</returns>
		public static String GetUrlAbsolute(this System.Web.UI.Page page, ActiveQueryBase query)
		{
			return PageUrlAttribute.GetAbsolute(page.GetType(), page, query);
		}
		#endregion
	}
}
