using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ESolutions.Web.UI
{
	[AttributeUsage(AttributeTargets.Property)]
	public class UrlParameterAttribute : Attribute
	{
		//Properties
		#region SuppoertedValues
		/// <summary>
		/// Gets or sets the supported values.
		/// </summary>
		/// <value>The supported values.</value>
		public String[] SupportedValues
		{
			get;
			set;
		}
		#endregion

		#region IsOptional
		public Boolean IsOptional
		{
			get;
			set;
		}
		#endregion

		//Constructors
		#region UrlParameterAttribute
		public UrlParameterAttribute ( )
		{
			this.SupportedValues = null;
			this.IsOptional = false;
		}
		#endregion
	}
}
