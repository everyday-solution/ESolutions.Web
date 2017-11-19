using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Web;

namespace ESolutions.Web.Tests
{
	/// <summary>
	/// Summary description for UnitTest1
	/// </summary>
	[TestClass]
	public class ActiveQueryBaseTests
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
		#region TestThatAParameterIsParsedCorrectly
		[TestMethod]
		public void TestThatAParameterIsParsedCorrectly()
		{
			ParameterTestClass c = new ParameterTestClass();
			c.Id = 1;
			c.AssignedName = "gugu";

			String result = c.Serialize();
			Assert.AreEqual("?id=1&assigned_name=gugu", result);
		}
		#endregion

		#region TestThatAParameterIsParsedCorrectlyButNotRequired
		[TestMethod]
		public void TestThatAParameterIsParsedCorrectlyButNotRequired()
		{
			ParameterTestClass2 c = new ParameterTestClass2();
			c.AssignedName = "gugu";

			String result = c.Serialize();
			Assert.AreEqual("?assigned_name=gugu", result);
			c.Id = 1;
			Assert.AreEqual(c.Serialize(), "?id=1&assigned_name=gugu");
		}
		#endregion

		#region TestThatAQueryIsCorrectlyDeserializedToAnAppropriateObject
		[TestMethod]
		public void TestThatAQueryIsCorrectlyDeserializedToAnAppropriateObject()
		{
			System.Web.HttpRequest request = new System.Web.HttpRequest("blah.html", "http://localhost", "id=1&assigned_name=gugu");
			ParameterTestClass actual = ParameterTestClass.Deserialize(request);
			Assert.AreEqual(actual.Id, 1);
			Assert.AreEqual(actual.AssignedName, "gugu");

		}
		#endregion

		#region TestThatAQueryIsCorrectlyDeserializedToAnAppropriateObjectAndThatValuesAreChecked
		[TestMethod]
		[ExpectedException(typeof(ActiveQueryException))]
		public void TestThatAQueryIsCorrectlyDeserializedToAnAppropriateObjectAndThatValuesAreChecked()
		{
			System.Web.HttpRequest request = new System.Web.HttpRequest("blah.html", "http://localhost", "id=1&assigned_name=gugu");
			ParameterTestClass2 actual = ParameterTestClass2.Deserialize(request);
		}
		#endregion

		#region TestThatUrlParameterDecoratedPropertiesCanBeDeclaredPrivate
		[TestMethod]
		public void TestThatUrlParameterDecoratedPropertiesCanBeDeclaredPrivate()
		{
			System.Web.HttpRequest request = new System.Web.HttpRequest("blah.html", "http://localhost", "id=2");
			ParameterTestClass3 actual = ParameterTestClass3.Deserialize(request);
			Assert.AreEqual(2, actual.IdAccessor());
		}
		#endregion

		#region TestDeserialization
		[TestMethod]
		public void TestDeserialization()
		{
			ParameterTestClass9 query = new ParameterTestClass9();
			query.Mode = ParameterTestClass9.EditModes.Create;
			String requestQuery = query.Serialize();
			Assert.AreEqual("?mode=create", requestQuery);

			System.Web.HttpRequest request = new System.Web.HttpRequest("blah.html", "http://localhost", "mode=create");
			ParameterTestClass9 actual = ParameterTestClass9.Deserialize(request);

			Assert.IsNull(actual.Manufacturer);
			Assert.AreEqual(ParameterTestClass9.EditModes.Create, actual.Mode);
		}
		#endregion

		#region TestComplexStringsInParameter
		[TestMethod]
		public void TestComplexStringsInParameter()
		{
			String param = "?sv=2017-04-17&sr=b&sig=r8Q%2FqvCFlnAUUK5xJj4jbaRNhjiLrsZtWvnc9gffIAM%3D&se=2017-11-18T17%3A04%3A09Z&sp=r";
						    
			ParameterTestClass6 query = new ParameterTestClass6();
			query.P1 = param;

			String requestQuery = query.Serialize();

			System.Web.HttpRequest request = new System.Web.HttpRequest("blah.html", "http://localhost", requestQuery.TrimStart('?'));
			ParameterTestClass6 actual = ParameterTestClass6.Deserialize(request);

			Assert.AreEqual(param, actual.P1);
		}
		#endregion

		#region TestThatEnumsCanBeSerializedWithPascalEnum
		[TestMethod]
		public void TestThatEnumsCanBeSerializedWithPascalEnum()
		{
			ParameterTestClass4 c = new ParameterTestClass4();
			c.Mode = ParameterTestClass4.PageModes.LongEnumValue;

			String result = c.Serialize();
			Assert.AreEqual("?mode=long_enum_value", result);
		}
		#endregion

		#region TestThatEnumsCanBeDeserializedWithPascalEnum
		[TestMethod]
		public void TestThatEnumsCanBeDeserializedWithPascalEnum()
		{
			System.Web.HttpRequest request = new System.Web.HttpRequest("blah.html", "http://localhost", "mode=long_enum_value");
			ParameterTestClass4 actual = ParameterTestClass4.Deserialize(request);

			Assert.AreEqual(ParameterTestClass4.PageModes.LongEnumValue, actual.Mode);
		}
		#endregion

		#region TestThatEnumsCanBeSerializedIfDecoradedWithUrlParameterAttribute
		[TestMethod]
		public void TestThatEnumsCanBeSerializedIfDecoradedWithUrlParameterAttribute()
		{
			ParameterTestClass4 c = new ParameterTestClass4();
			c.Mode = ParameterTestClass4.PageModes.Create;

			String result = c.Serialize();
			Assert.AreEqual("?mode=create", result);
		}
		#endregion

		#region TestThatEnumsCanBeDeserializedIfDecoradedWithUrlParameterAttribute
		[TestMethod]
		public void TestThatEnumsCanBeDeserializedIfDecoradedWithUrlParameterAttribute()
		{
			System.Web.HttpRequest request = new System.Web.HttpRequest("blah.html", "http://localhost", "mode=create");
			ParameterTestClass4 actual = ParameterTestClass4.Deserialize(request);

			Assert.AreEqual(ParameterTestClass4.PageModes.Create, actual.Mode);
		}
		#endregion

		#region TestThatGuidCanBeDeserialized
		[TestMethod]
		public void TestThatGuidCanBeSeralizedAndDeserialized()
		{
			ParameterTestClass10 query = new ParameterTestClass10();
			query.Guid = new Guid("6490576a-9d7c-4f55-9e85-e886ba427b1c");
			String queryString = query.Serialize();

			Assert.AreEqual("?guid=6490576a-9d7c-4f55-9e85-e886ba427b1c", queryString);

			System.Web.HttpRequest request = new System.Web.HttpRequest("blah.html", "http://localhost", "guid=6490576a-9d7c-4f55-9e85-e886ba427b1c");
			query = ParameterTestClass10.Deserialize(request);

			Assert.AreEqual(query.Guid, new Guid("6490576a-9d7c-4f55-9e85-e886ba427b1c"));
		}
		#endregion

		#region TestThatDateTimeIsSerializedWithoutCulture
		[TestMethod]
		public void TestThatDateTimeIsSerializedWithoutCulture()
		{
			ParameterTestClass11 query = new ParameterTestClass11();
			query.Time = new DateTime(2014, 12, 31, 1, 2, 3, 4);

			String queryString = query.Serialize();
			String expected = String.Format("?time={0}", HttpUtility.UrlEncode(query.Time.ToString("o")));
			Assert.AreEqual(expected, queryString);
		}
		#endregion

		#region TestThatDateTimeIsSerializedWithoutCultureWithNullable
		[TestMethod]
		public void TestThatDateTimeIsSerializedWithoutCultureWithNullable()
		{
			ParameterTestClass12 query = new ParameterTestClass12();
			query.Time = new DateTime(2014, 12, 31, 1, 2, 3, 4);

			String queryString = query.Serialize();
			String expected = String.Format("?time={0}", HttpUtility.UrlEncode(query.Time.Value.ToString("o")));
			Assert.AreEqual(expected, queryString);
		}
		#endregion

		#region TestThatDateTimeIsSerializedWithoutCultureWithNullableThatIsNull
		[TestMethod]
		public void TestThatDateTimeIsSerializedWithoutCultureWithNullableThatIsNull()
		{
			ParameterTestClass12 query = new ParameterTestClass12();
			query.Time = null;

			String queryString = query.Serialize();
			Assert.AreEqual("", queryString);
		}
		#endregion

		#region TestThatDateTimeCanBeDeserialized
		[TestMethod]
		public void TestThatDateTimeCanBeDeserialized()
		{
			ParameterTestClass11 expected = new ParameterTestClass11();
			expected.Time = new DateTime(2014, 12, 31, 1, 2, 3, 4);
			String queryString = expected.Serialize();

			System.Web.HttpRequest request = new System.Web.HttpRequest("blah.html", "http://localhost", queryString.TrimStart('?'));
			ParameterTestClass11 actual = ParameterTestClass11.Deserialize(request);

			Assert.AreEqual(expected.Time, actual.Time);
		}
		#endregion

		#region TestThatDateTimeIsDeserializedWithoutCultureWithNullable
		[TestMethod]
		public void TestThatDateTimeIsDeserializedWithoutCultureWithNullable()
		{
			ParameterTestClass12 expected = new ParameterTestClass12();
			expected.Time = new DateTime(2014, 12, 31, 1, 2, 3, 4);
			String queryString = expected.Serialize();

			System.Web.HttpRequest request = new System.Web.HttpRequest("blah.html", "http://localhost", queryString.TrimStart('?'));
			ParameterTestClass12 actual = ParameterTestClass12.Deserialize(request);

			Assert.AreEqual(expected.Time, actual.Time);
		}
		#endregion

		#region TestThatDateTimeIsDeserializedWithoutCultureWithNullableThatIsNull
		[TestMethod]
		public void TestThatDateTimeIsDeserializedWithoutCultureWithNullableThatIsNull()
		{
			ParameterTestClass12 expected = new ParameterTestClass12();
			expected.Time = null;
			String queryString = expected.Serialize();

			System.Web.HttpRequest request = new System.Web.HttpRequest("blah.html", "http://localhost", queryString.TrimStart('?'));
			ParameterTestClass12 actual = ParameterTestClass12.Deserialize(request);

			Assert.AreEqual(expected.Time, actual.Time);
		}
		#endregion

		#region TestThatListsOfInt32CanBeSerialized
		[TestMethod]
		public void TestThatListsOfInt32CanBeSerialized()
		{
			ParameterTestClass13 query = new ParameterTestClass13();
			query.Id = new List<int>() { 1, 2, 3 };

			String queryString = query.Serialize();
			String expected = String.Format("?id=1&id=2&id=3");
			Assert.AreEqual(expected, queryString);
		}
		#endregion

		#region TestThatListOfInt32CanBeDeserialized
		[TestMethod]
		public void TestThatListOfInt32CanBeDeserialized()
		{
			ParameterTestClass13 expected = new ParameterTestClass13();
			expected.Id = new List<int>() { 1, 2, 3 };
			String queryString = expected.Serialize();

			System.Web.HttpRequest request = new System.Web.HttpRequest("blah.html", "http://localhost", queryString.TrimStart('?'));
			ParameterTestClass13 actual = ParameterTestClass13.Deserialize(request);

			Assert.AreEqual(3, actual.Id.Count);
			Assert.AreEqual(expected.Id[0], actual.Id[0]);
			Assert.AreEqual(expected.Id[1], actual.Id[1]);
			Assert.AreEqual(expected.Id[2], actual.Id[2]);
		}
		#endregion

		#region TestThatListOfInt32CanBeDeserialized
		[TestMethod]
		public void TestThatEmptyListOfInt32CanBeDeserialized()
		{
			ParameterTestClass13 expected = new ParameterTestClass13();
			expected.Id = new List<int>();
			String queryString = expected.Serialize();

			System.Web.HttpRequest request = new System.Web.HttpRequest("blah.html", "http://localhost", queryString.TrimStart('?'));
			ParameterTestClass13 actual = ParameterTestClass13.Deserialize(request);

			Assert.IsNotNull(actual.Id);
			Assert.AreEqual(0, actual.Id.Count);
		}
		#endregion
	}
}
