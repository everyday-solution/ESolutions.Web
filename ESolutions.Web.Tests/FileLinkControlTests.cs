using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ESolutions.Web.UI;

namespace ESolutions.Web.Tests
{
	/// <summary>
	/// Summary description for FileLinkControlTests
	/// </summary>
	[TestClass]
	public class FileLinkControlTests
	{
		#region FileLinkControlTests
		public FileLinkControlTests()
		{
			//
			// TODO: Add constructor logic here
			//
		}
		#endregion

		#region testContextInstance
		private TestContext testContextInstance;
		#endregion

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

		#region Additional test attributes
		//
		// You can use the following additional attributes as you write your tests:
		//
		// Use ClassInitialize to run code before running the first test in the class
		// [ClassInitialize()]
		// public static void MyClassInitialize(TestContext testContext) { }
		//
		// Use ClassCleanup to run code after all tests in a class have run
		// [ClassCleanup()]
		// public static void MyClassCleanup() { }
		//
		// Use TestInitialize to run code before running each test 
		// [TestInitialize()]
		// public void MyTestInitialize() { }
		//
		// Use TestCleanup to run code after each test has run
		// [TestCleanup()]
		// public void MyTestCleanup() { }
		//
		#endregion

		#region TestThatCSSLinkIsGeneratedCorrectly
		[TestMethod]
		public void TestThatCSSLinkIsGeneratedCorrectly()
		{
			System.IO.TextWriter writer = new System.IO.StringWriter();
			System.Web.UI.HtmlTextWriter htmlWriter = new System.Web.UI.HtmlTextWriter(writer);

			System.Web.UI.Page page = new System.Web.UI.Page();
			FileLinkControl control = new FileLinkControl();
			page.Controls.Add(control);
			control.File = "~/Common.css";
			control.RenderControl(htmlWriter);
			Assert.AreEqual(@"<link href=""~/Common.css"" rel=""stylesheet"" type=""text/css"" />", htmlWriter.InnerWriter.ToString());
		}
		#endregion

		#region TestThatJavaScriptLinkIsGeneratedCorrectly
		[TestMethod]
		public void TestThatJavaScriptLinkIsGeneratedCorrectly()
		{
			System.IO.TextWriter writer = new System.IO.StringWriter();
			System.Web.UI.HtmlTextWriter htmlWriter = new System.Web.UI.HtmlTextWriter(writer);

			System.Web.UI.Page page = new System.Web.UI.Page();
			FileLinkControl control = new FileLinkControl();
			page.Controls.Add(control);
			control.File = "~/Common.js";
			control.RenderControl(htmlWriter);
			Assert.AreEqual(@"<script src=""~/Common.js"" type=""text/javascript""></script>", htmlWriter.InnerWriter.ToString());
		}
		#endregion

		#region TestThatJavaScriptLinkIsGeneratedCorrectlyWithoutExtension
		[TestMethod]
		public void TestThatJavaScriptLinkIsGeneratedCorrectlyWithoutExtension()
		{
			System.IO.TextWriter writer = new System.IO.StringWriter();
			System.Web.UI.HtmlTextWriter htmlWriter = new System.Web.UI.HtmlTextWriter(writer);

			System.Web.UI.Page page = new System.Web.UI.Page();
			FileLinkControl control = new FileLinkControl();
			page.Controls.Add(control);
			control.File = "~/signalr/hubs";
			control.RenderControl(htmlWriter);
			Assert.AreEqual(@"<script src=""~/signalr/hubs"" type=""text/javascript""></script>", htmlWriter.InnerWriter.ToString());
		}
		#endregion

		#region TestThatFaviconIsGeneratedCorrectly
		[TestMethod]
		public void TestThatFaviconIsGeneratedCorrectly( )
		{
			System.IO.TextWriter writer = new System.IO.StringWriter();
			System.Web.UI.HtmlTextWriter htmlWriter = new System.Web.UI.HtmlTextWriter(writer);

			System.Web.UI.Page page = new System.Web.UI.Page();
			FileLinkControl control = new FileLinkControl();
			page.Controls.Add(control);
			control.File = "~/favicon.ico";
			control.RenderControl(htmlWriter);
			Assert.AreEqual(@"<link href=""~/favicon.ico"" rel=""shortcut icon"" type=""image/x-icon"" />", htmlWriter.InnerWriter.ToString());
		}
		#endregion
	}
}
