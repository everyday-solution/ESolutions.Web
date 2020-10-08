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

	class TestEventArgs : RepeaterItemEventArgsBase<EventArgs>
	{
		#region TestEventArgs
		public TestEventArgs(RepeaterItemEventArgs item) : base(item)
		{
		}
		#endregion

		#region TestLiteral
		public Literal TestLiteral
		{
			get
			{
				return this.GetControl<Literal>();
			}
		}
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
			var literal = new Literal()
			{
				ID = nameof(TestEventArgs.TestLiteral),
				Text = "test string"
			};
			var item = new RepeaterItem(0, ListItemType.Item);
			item.Controls.Add(literal);
			var eventArgs = new RepeaterItemEventArgs(item);
			var wrappedEventArgs = new TestEventArgs(eventArgs);

			Assert.AreEqual(testString, wrappedEventArgs.TestLiteral.Text);
		}
		#endregion
	}
}
