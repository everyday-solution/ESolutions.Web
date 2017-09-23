using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ESolutions.Web.UI
{
	public static class ABTesting
	{
		#region GetPage
		public static String GetPage<AType, BType>(
			ActiveQueryBase a,
			ActiveQueryBase b)
			where AType : System.Web.UI.Page
			where BType : System.Web.UI.Page
		{
			return ABTesting.GetPage<AType, BType>(a, b, 0.7);
		}
		#endregion

		#region GetPage
		public static String GetPage<AType, BType>(
			ActiveQueryBase a,
			ActiveQueryBase b,
			Double threshold)
			where AType : System.Web.UI.Page
			where BType : System.Web.UI.Page
		{
			return ABTesting.GetPage<AType, BType>(a, b, threshold, new Randomizer());
		}
		#endregion

		#region GetPage
		public static String GetPage<AType, BType>(
			ActiveQueryBase a,
			ActiveQueryBase b,
			Double threshold,
			IRandomizer randomizer)
			where AType : System.Web.UI.Page
			where BType : System.Web.UI.Page
		{
			var aUrl = PageUrlAttribute.Get<AType>(a);
			var bUrl = PageUrlAttribute.Get<BType>(b);

			Int32 max = 1000;
			var random = randomizer.Next(1, max);
			Double quota = (Double) random / (Double)max;

			return quota > threshold ? bUrl : aUrl;
		}
		#endregion
	}
}
