using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ESolutions.Web.UI
{
	[AttributeUsage(AttributeTargets.Class, AllowMultiple=false)]
	public class PageQueryAttribute : Attribute
	{
		//Constructors
		#region PageQueryAttribute
		public PageQueryAttribute()
		{
		}
		#endregion
	}
}
