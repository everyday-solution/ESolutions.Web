using ESolutions.Web.UI;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Web;

namespace ESolutions.Web.Tests
{
	/// <summary>
	///This is a test class for PageTest and is intended
	///to contain all PageTest Unit Tests
	///</summary>
	[TestClass()]
	public class PageTest
	{
		//Fields
		#region testContextInstance
		private TestContext testContextInstance;
		#endregion

		//Properties
		#region TestContext
		/// <summary>
		///Gets or sets the test context which provides
		///information about and functionality for the currentProperty test run.
		///</summary>
		public TestContext TestContext
		{
			get
			{
				return testContextInstance;
			}
			set
			{
				testContextInstance = value;
			}
		}
		#endregion

		//Methods
		#region SetRequestForTestingTest
		[TestMethod]
		public void SetRequestForTestingTest()
		{
			TestWebPage6 target = new TestWebPage6();
			target.InitializeTestingEnvironment(
				new System.Web.HttpRequest("blah.html", "http://localhost", "id=2"),
				new System.Web.HttpResponse(new System.IO.StringWriter()),
				(url) => { });
			Assert.AreEqual(2, target.RequestAddOn.Query.Id);
		}
		#endregion

		#region TestThatResponseRedirectWorks
		/// <summary>
		/// Tests the that response redirect works.
		/// </summary>
		/// <remarks>
		/// This test will never work. It must be debugged manually. There will alwys be a NullRefException
		/// because we can not initialize the HttpResonse appropriatly.
		/// </remarks>
		[TestMethod]
		public void TestThatResponseRedirectWorks()
		{
			String redirectUrl = String.Empty;
			TestWebPage6 target = new TestWebPage6();
			target.InitializeTestingEnvironment(
				new System.Web.HttpRequest("blah.html", "http://localhost", "id=2"),
				new System.Web.HttpResponse(new System.IO.StringWriter()),
				(url) => { redirectUrl = url; });
			target.ResponseAddOn.Redirect<TestWebPage5>();
			Assert.AreEqual("~/TestWebPage5.aspx", redirectUrl);
		}
		#endregion

		#region EnsureThatRequestAddOnIsOnlyGeneratedOnce
		[TestMethod]
		public void EnsureThatRequestAddOnIsOnlyGeneratedOnce( )
		{
			TestWebPage6 target = new TestWebPage6();
			target.InitializeTestingEnvironment(
				new System.Web.HttpRequest("blah.html", "http://localhost", "id=2"),
				new System.Web.HttpResponse(new System.IO.StringWriter()),
				null);
			var request1 = target.RequestAddOn;
			var request2 = target.RequestAddOn;

			Assert.ReferenceEquals(request1, request2);
		}
		#endregion

		#region EnsureThatQueryIsOnlyGeneratedOnce
		[TestMethod]
		public void EnsureThatQueryIsOnlyGeneratedOnce( )
		{
			TestWebPage6 target = new TestWebPage6();
			target.InitializeTestingEnvironment(
				new System.Web.HttpRequest("blah.html", "http://localhost", "id=2"),
				new System.Web.HttpResponse(new System.IO.StringWriter()),
				null);
			var query1 = target.RequestAddOn.Query;
			var query2 = target.RequestAddOn.Query;

			Assert.AreEqual(query1, query2);
		}
		#endregion
	}
}
