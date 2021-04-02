using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.UI.WebControls;
using ESolutions.Web.UI;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ESolutions.Web.Tests
{
	class TestData
	{
		public String TestProperty { get; set; }
	}

	class TestEventArgs : RepeaterItemEventArgsBase<TestData>
	{
		#region TestEventArgs
		public TestEventArgs(RepeaterItemEventArgs item) : base(item)
		{
		}
		#endregion

		#region TestLiteral
		public Literal TestLiteral => this.GetControl<Literal>();
		#endregion

		#region TestLiteralNull
		public Literal TestLiteralNull = null;
		#endregion
	}

	[TestClass]
	class RepeaterItemEventArgsBaseTests
	{
		#region GetControlTest
		[TestMethod]
		public void GetControlTest()
		{
			var testString = "test string";
			var testData = new TestData() { TestProperty = "HALLO" };

			var repeaterItem = new RepeaterItem(0, ListItemType.Item) { DataItem = testData };
			repeaterItem.Controls.Add(new Literal() { ID = nameof(TestEventArgs.TestLiteral), Text = testString });
			repeaterItem.Controls.Add(new Literal() { ID = nameof(TestEventArgs.TestLiteralNull), Text = testString });
			var eventArgs = new RepeaterItemEventArgs(repeaterItem);

			var wrappedEventArgs = new TestEventArgs(eventArgs);

			Assert.AreEqual(testString, wrappedEventArgs.TestLiteral.Text);
			Assert.AreEqual(testString, wrappedEventArgs.TestLiteralNull.Text);
			Assert.AreEqual(testData, wrappedEventArgs.Data);
		}
		#endregion
	}
}
