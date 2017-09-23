using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;

namespace ESolutions.Web.UI
{
	/// <summary>
	/// This class can be used to mark classes of aspx-files with their url in the application.
	/// </summary>
	[AttributeUsage(AttributeTargets.Class, AllowMultiple = false)]
	public class PageUrlAttribute : Attribute
	{
		//Fields
		#region Url
		/// <summary>
		/// Query of the page.
		/// </summary>
		public String Url = String.Empty;
		#endregion

		//Constructors
		#region PageUrlAttribute
		/// <summary>
		/// Initializes a new instance of the <see cref="PageUrlAttribute"/> class.
		/// </summary>
		/// <param name="url">The URL.</param>
		public PageUrlAttribute(String url)
		{
			this.Url = url;
		}
		#endregion

		//Methods
		#region Get
		/// <summary>
		/// Return the URL assigned to the page by the PageUrl-Attribute
		/// </summary>
		/// <typeparam name="GotoPage"></typeparam>
		/// <returns></returns>
		public static String Get<GotoPage>()
			where GotoPage : System.Web.UI.Page
		{
			return PageUrlAttribute.Get(typeof(GotoPage));
		}
		#endregion

		#region Get
		/// <summary>
		/// Return the URL assigned to the page by the PageUrl-Attribute
		/// </summary>
		/// <param name="pageType">Type of the page.</param>
		/// <returns></returns>
		internal static String Get(Type pageType)
		{
			object[] parameter = pageType.GetCustomAttributes(typeof(PageUrlAttribute), true);

			if (parameter.Length <= 0)
			{
				throw new ActiveQueryException(String.Format(
					ActiveQueryExceptionStrings.NoPageUrlAttribute,
					pageType.FullName));
			}

			return (parameter[0] as PageUrlAttribute).Url;
		}
		#endregion

		#region Get
		/// <summary>
		/// Return the URL assigned to the page by the PageUrl-Attribute combined
		/// with the querystring generated from the provided query object.
		/// </summary>
		/// <typeparam name="GotoPage"></typeparam>
		/// <param name="query">The query.</param>
		/// <returns></returns>
		public static String Get<GotoPage>(ActiveQueryBase query)
			where GotoPage : System.Web.UI.Page
		{
			return PageUrlAttribute.Get(typeof(GotoPage), query);
		}
		#endregion

		#region Get
		/// <summary>
		/// Return the URL assigned to the page by the PageUrl-Attribute combined
		/// with the querystring generated from the provided query object.
		/// </summary>
		/// <param name="pageType">Type of the page.</param>
		/// <param name="query">The query.</param>
		/// <returns></returns>
		internal static String Get(Type pageType, ActiveQueryBase query)
		{
			String url = PageUrlAttribute.Get(pageType);

			//Amount of nested classes with the PageQueryAttribute must be one
			PageUrlAttribute.VerifyThereIsJustOneNestedClassWithThePageQueryAttribute(pageType);

			/*Type queryType = null;
			foreach (Type current in pageType.GetNestedTypes())
			{
				if (current.GetCustomAttributes(typeof(PageQueryAttribute), true).Length > 0)
				{
					queryType = current;
				}
			}*/

			String result = String.Format("{0}{1}", url, query?.Serialize());
			return result;
		}
		#endregion

		#region GetAbsolute
		public static String GetAbsolute<GotoPage>(System.Web.UI.Page currentPage)
			where GotoPage : System.Web.UI.Page
		{
			return PageUrlAttribute.GetAbsolute(typeof(GotoPage), currentPage);
		}
		#endregion

		#region GetAbsolute
		public static String GetAbsolute(Type gotoPageType, System.Web.UI.Page currentPage)
		{
			String relativePath = currentPage.ResolveUrl(PageUrlAttribute.Get(gotoPageType));
			return String.Format(
				"{0}://{1}{2}",
				currentPage.Request.IsSecureConnection ? "https" : "http",
				currentPage.Request.Url.Host,
				relativePath);
		}
		#endregion

		#region GetAbsolute
		public static String GetAbsolute<GotoPage>(System.Web.UI.Page currentPage, ActiveQueryBase query)
			where GotoPage : System.Web.UI.Page
		{
			return PageUrlAttribute.GetAbsolute(typeof(GotoPage), currentPage, query);
		}
		#endregion

		#region GetAbsolute
		public static String GetAbsolute(Type gotoPageType, System.Web.UI.Page currentPage, ActiveQueryBase query)
		{
			String relativePath = currentPage.ResolveUrl(PageUrlAttribute.Get(gotoPageType, query));
			return String.Format(
				"{0}://{1}{2}",
				currentPage.Request.IsSecureConnection ? "https" : "http",
				currentPage.Request.Url.Host,
				relativePath);
		}
		#endregion

		#region GetAbsolute
		public static String GetAbsolute<GotoPage>(Uri root)
			where GotoPage : System.Web.UI.Page
		{
			return GetAbsolute<GotoPage>(root, null);
		}
		#endregion

		#region GetAbsolute
		public static String GetAbsolute<GotoPage>(Uri root, ActiveQueryBase query)
			where GotoPage : System.Web.UI.Page
		{
			String relativeUri = query == null ?
				PageUrlAttribute.Get<GotoPage>() :
				PageUrlAttribute.Get<GotoPage>(query);
			String result = String.Format("{0}://{1}{2}/{3}", root.Scheme, root.Authority, HttpRuntime.AppDomainAppVirtualPath, relativeUri.TrimStart('~', '/'));
			return result;
		}
		#endregion

		#region VerifyThereIsJustOneNestedClassWithThePageQueryAttribute
		/// <summary>
		/// Verifies the there is just one nested class with the page query attribute.
		/// </summary>
		/// <param name="page">The page.</param>
		private static void VerifyThereIsJustOneNestedClassWithThePageQueryAttribute(Type pageType)
		{
			Type queryType = null;
			Int32 foundTypes = 0;

			foreach (Type current in pageType.GetNestedTypes())
			{
				if (current.GetCustomAttributes(typeof(PageQueryAttribute), true).Length > 0)
				{
					queryType = current;
					foundTypes++;
				}
			}

			if (foundTypes <= 0)
			{
				throw new ActiveQueryException(String.Format(
					ActiveQueryExceptionStrings.NoClassWithPageQueryAttribute,
					pageType.FullName));
			}

			if (foundTypes >= 2)
			{
				throw new ActiveQueryException(String.Format(
					ActiveQueryExceptionStrings.ToManyPageQueryClasses,
					pageType.FullName));
			}
		}
		#endregion

		#region GetFileName
		/// <summary>
		/// Gets the name of the file.
		/// </summary>
		/// <typeparam name="WebPage">The type of the eb page.</typeparam>
		/// <returns></returns>
		public static String GetFileName<WebPage>()
			where WebPage : System.Web.UI.Page
		{
			String result = "/";
			PageUrlAttribute attribute = typeof(WebPage).GetCustomAttributes(typeof(PageUrlAttribute), true).First() as PageUrlAttribute;

			if (attribute != null)
			{
				var parts = attribute.Url.Split('/');
				result = parts.Last();
			}

			return result;
		}
		#endregion
	}
}
