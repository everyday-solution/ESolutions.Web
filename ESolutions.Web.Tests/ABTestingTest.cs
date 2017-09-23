using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ESolutions.Web.UI;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Web.UI.HtmlControls;

namespace ESolutions.Web.Tests
{
	[TestClass]
	public class ABTestingTest
	{
		//Classes
		#region FakeRAndomizer
		public class FakeRandomizer : IRandomizer
		{
			//Fields
			#region callback
			private Func<Int32, Int32, Int32> callback = null;
			#endregion


			//Constructors
			#region FakeRandomizer
			public FakeRandomizer(Func<Int32, Int32, Int32> callback)
			{
				this.callback = callback;
			}
			#endregion

			//Methods
			#region Next
			public Int32 Next(Int32 minimum, Int32 maximum)
			{
				return this.callback(minimum, maximum);
			}
			#endregion
		}
		#endregion

		//Methods
		#region TestLower
		[TestMethod]
		public void TestLower()
		{
			FakeRandomizer randomizer = new FakeRandomizer((a, b) => 700);
			var actual = ABTesting.GetPage<TestWebPage2, TestWebPage10>(null, null, 0.7, randomizer);
			Assert.AreEqual("~/TestWebPage2.aspx", actual);
		}
		#endregion

		#region TestLower
		[TestMethod]
		public void TestUpper()
		{
			FakeRandomizer randomizer = new FakeRandomizer((a, b) => 701);
			var actual = ABTesting.GetPage<TestWebPage2, TestWebPage10>(null, null, 0.7, randomizer);
			Assert.AreEqual("~/TestWebPage10.aspx", actual);
		}
		#endregion
	}
}
