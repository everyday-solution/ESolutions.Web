using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ESolutions.Web.UI;

namespace ESolutions.Web.Tests
{
	[PageQuery]
	public class ParameterTestClass9 : ActiveQueryBase<ParameterTestClass9>
	{
		public enum EditModes
		{
			Edit,
			Create
		}

		#region Mode
		[UrlParameter]
		public EditModes Mode
		{
			get;
			set;
		}
		#endregion

		#region Id
		[UrlParameter(IsOptional = true)]
		private Int32? Id
		{
			get;
			set;
		}
		#endregion

		#region Manufacturer
		public Object Manufacturer
		{
			get
			{
				return Id;
			}
			set
			{
				this.Id = (Int32)value;
			}
		}
		#endregion
	}
}
