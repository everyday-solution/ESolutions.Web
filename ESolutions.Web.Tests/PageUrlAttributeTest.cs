using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ESolutions.Web.UI;
using System.Web;
using System.IO;
using System.Reflection;

namespace ESolutions.Web.Tests
{
	/// <summary>
	/// Summary description for UnitTest1
	/// </summary>
	[TestClass]
	public class PageUrlAttributeTest
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
		#region TestThatUrlWithoutQueryCanBeRetreive
		[TestMethod]
		public void TestThatUrlWithoutQueryCanBeRetreive()
		{
			String actual = (new TestWebPage1()).GetUrl();
			String expected = "~/TestWebPage1.aspx";

			Assert.AreEqual(expected, actual);
		}
		#endregion

		#region TestThatUrlWithQueryCanBeRetreive
		[TestMethod]
		public void TestThatUrlWithQueryCanBeRetreive()
		{
			TestWebPage2.Query query = new TestWebPage2.Query();
			query.Id = 1;
			query.Text = "hallowelt";

			String actual = (new TestWebPage2()).GetUrl(query);
			String expected = "~/TestWebPage2.aspx?id=1&text=hallowelt";

			Assert.AreEqual(expected, actual);
		}
		#endregion

		#region TestThatExceptionIsThrownIfAPageHasNoPageUrlAttribute
		[TestMethod]
		[ExpectedException(typeof(ActiveQueryException))]
		public void TestThatExceptionIsThrownIfAPageHasNoPageUrlAttribute()
		{
			(new TestWebPage3()).GetUrl();
		}
		#endregion

		#region TestThatExceptionIsThrownIfPageHasNoNestedClassWithThePageQueryAttribute
		[TestMethod]
		[ExpectedException(typeof(ActiveQueryException))]
		public void TestThatExceptionIsThrownIfPageHasNoNestedClassWithThePageQueryAttribute()
		{
			TestWebPage4.Query query = new TestWebPage4.Query();
			(new TestWebPage4()).GetUrl(query);
		}
		#endregion

		#region TestThatExceptionIsThrownIfThereIsMoreThanOneNestesPageQueryClass
		[TestMethod]
		[ExpectedException(typeof(ActiveQueryException))]
		public void TestThatExceptionIsThrownIfThereIsMoreThanOneNestesPageQueryClass()
		{
			TestWebPage5.Query query = new TestWebPage5.Query();
			(new TestWebPage5()).GetUrl(query);
		}
		#endregion

		#region TestThatNonOptionalParametersCanNotBeNull
		[TestMethod]
		[ExpectedException(typeof(ActiveQueryException))]
		public void TestThatNonOptionalParametersCanNotBeNull()
		{
			ParameterTestClass6 query = new ParameterTestClass6();
			query.P1 = null;

			query.Serialize();
		}
		#endregion

		#region TestThatOptionalParamCanBeNull
		[TestMethod]
		public void TestThatOptionalParamCanBeNull()
		{
			ParameterTestClass7 query = new ParameterTestClass7();
			query.P1 = null;

			String actual = query.Serialize();
			Assert.AreEqual(String.Empty, actual);
		}
		#endregion

		#region TestThatQueryIsEmptyIfAllParametersAreOptionalAndNull
		[TestMethod]
		public void TestThatQueryIsEmptyIfAllParametersAreOptionalAndNull()
		{
			ParameterTestClass5 query = new ParameterTestClass5();
			query.P1 = null;

			String actual = query.Serialize();

			Assert.AreEqual(String.Empty, actual);
		}
		#endregion

		#region TestThatOptionalAndNonOptionalParametersCanBeMixed
		[TestMethod]
		public void TestThatOptionalAndNonOptionalParametersCanBeMixed()
		{
			ParameterTestClass8 query = new ParameterTestClass8();
			query.P1 = null;
			query.P2 = "HAGE";

			String actual = query.Serialize();

			Assert.AreEqual("?p2=HAGE", actual);
		}
		#endregion

		#region TestThatPageUrlAttributesFromDerivedClassesAreParsed
		[TestMethod]
		public void TestThatPageUrlAttributesFromDerivedClassesAreParsed()
		{
			TestWebPage7.Query query = new TestWebPage7.Query();
			query.MyParameter = "test";

			String actual = (new TestWebPage7()).GetUrl(query);
			String expected = "~/TestWebPage7.aspx?inner=test";

			Assert.AreEqual(expected, actual);
		}
		#endregion

		#region TestThatPageUrlAttributesFromDerivedClassesInMasterpagesAreParsed
		[TestMethod]
		public void TestThatPageUrlAttributesFromDerivedClassesInMasterpagesAreParsed()
		{
			TestWebPage8.Query query = new TestWebPage8.Query();
			query.Vehicle = new Guid("{A6B991ED-F065-4747-A564-41A06AE79841}");

			String actual = (new TestWebPage7()).GetUrl(query);
			String expected = "~/TestWebPage7.aspx?vehicle_guid=a6b991ed-f065-4747-a564-41a06ae79841";

			Assert.AreEqual(expected, actual);
		}
		#endregion

		#region TestGetAbsoluteWithManualRootUri
		[TestMethod]
		public void TestGetAbsoluteWithManualRootUri()
		{
			String actual = PageUrlAttribute.GetAbsolute<TestWebPage1>(new Uri("http://www.youmoto.de"));
			String expected = "http://www.youmoto.de/TestWebPage1.aspx";

			Assert.AreEqual(expected, actual);
		}
		#endregion

		#region TestGetAbsoluteWithManualRootUriAndQuery
		[TestMethod]
		public void TestGetAbsoluteWithManualRootUriAndQuery()
		{
			String actual = PageUrlAttribute.GetAbsolute<TestWebPage2>(new Uri("http://www.youmoto.de"), new TestWebPage2.Query() { Id = 1, Text = "test" });
			String expected = "http://www.youmoto.de/TestWebPage2.aspx?id=1&text=test";

			Assert.AreEqual(expected, actual);
		}
		#endregion

		#region TestGetFileName
		[TestMethod]
		public void TestGetFileName()
		{
			String filename = PageUrlAttribute.GetFileName<TestWebPage1>();
			Assert.AreEqual("TestWebPage1.aspx", filename);
		}
		#endregion

		#region TestThatDateTimeIsSerializedWithoutCultureInPage
		[TestMethod]
		public void TestThatDateTimeIsSerializedWithoutCultureInPage()
		{
			String url = PageUrlAttribute.Get<TestWebPage9>(new TestWebPage9.Query() { Time = new DateTime(2014, 12, 31, 1, 2, 3, 4) });

			Assert.AreEqual("~/TestWebPage9.aspx?time=2014-12-31T01%3a02%3a03.0040000", url);
		}
		#endregion

		#region TestGetWithQueryEqualsNull
		[TestMethod]
		public void TestGetWithQueryEqualsNull()
		{
			var actual = PageUrlAttribute.Get<TestWebPage2>(null);
			Assert.AreEqual("~/TestWebPage2.aspx", actual);
		}
		#endregion
	}
}
