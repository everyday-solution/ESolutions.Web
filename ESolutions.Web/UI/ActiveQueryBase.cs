using ESolutions;
using System;
using System.Collections.Generic;
using System.Collections;
using System.Linq;
using System.Text;
using System.Reflection;
using System.Web;
using System.Text.RegularExpressions;
using System.Globalization;

namespace ESolutions.Web.UI
{
	public class ActiveQueryBase<T> : ActiveQueryBase
		where T : ActiveQueryBase
	{
		#region Deserialize
		public static T Deserialize(System.Web.HttpRequest request)
		{
			return ActiveQueryBase.Deserialize(request, typeof(T)) as T;
		}
		#endregion
	}

	public class ActiveQueryBase
	{
		//Delegate
		#region SerializerDelegate
		private delegate String SerializerDelegate(PropertyInfo property);
		#endregion

		#region DeserializerDelegate
		private delegate Object DeserializerDelegate(PropertyInfo property, String serializedValue);
		#endregion

		//Fields
		#region serializers
		private Dictionary<Type, SerializerDelegate> serializers = new Dictionary<Type, SerializerDelegate>();
		#endregion

		#region defaultSerializer
		private SerializerDelegate defaultSerializer = null;
		#endregion

		#region deserializers
		private static Dictionary<Type, DeserializerDelegate> deserializers = new Dictionary<Type, DeserializerDelegate>();
		#endregion

		#region defaultDeserializer
		private static DeserializerDelegate defaultDeserializer = null;
		#endregion

		//Constructors
		#region ActiveQueryBase
		public ActiveQueryBase()
		{
			this.defaultSerializer = this.SerializeString;
			this.serializers.Add(typeof(DateTime), this.SerializeDateTime);
			this.serializers.Add(typeof(DateTime?), this.SerializeDateTime);
			this.serializers.Add(typeof(List<Int32>), this.SerializeListOfInt32);
		}
		#endregion

		#region ActiveQueryBase
		static ActiveQueryBase()
		{
			ActiveQueryBase.defaultDeserializer = ActiveQueryBase.DeserializeString;
			ActiveQueryBase.deserializers.Add(typeof(List<Int32>), ActiveQueryBase.DeserializeListOfInt32);
		}
		#endregion

		//Methods
		#region Serialize
		/// <summary>
		/// Serializes the query instance to a URL compatible query string
		/// </summary>
		/// <returns></returns>
		public String Serialize()
		{
			String query = String.Empty;
			var requiredPropertyInfoList = ActiveQueryBase.GetAllPrivateAndPublicPropertiesDecoratedWithUrlParameter(this.GetType());

			foreach (var currentProperty in requiredPropertyInfoList)
			{
				UrlParameterAttribute parameter = ActiveQueryBase.GetUrlParameter(currentProperty);
				Object value = currentProperty.GetValue(this, null);

				if (value == null && !parameter.IsOptional)
				{
					String message = String.Format(
						ActiveQueryExceptionStrings.ParameterIsNullButNonOptional,
						currentProperty.Name);
					throw new ActiveQueryException(message);
				}

				if (value != null)
				{
					query += this.SerializeSingleProperty(currentProperty);
				}
			}

			query = query.Trim('&');

			if (!String.IsNullOrEmpty(query))
			{
				query = String.Format("?{0}", query);
			}

			return query;
		}
		#endregion

		#region SerializeSingleProperty
		private String SerializeSingleProperty(PropertyInfo currentProperty)
		{
			return
				this.serializers.ContainsKey(currentProperty.PropertyType) ?
				this.serializers[currentProperty.PropertyType](currentProperty) :
				this.defaultSerializer(currentProperty);
		}
		#endregion

		#region SerializeString
		private String SerializeString(PropertyInfo property)
		{
			object value = property.GetValue(this, null);
			return String.Format(
				"{0}={1}&",
				property.Name.Underscore(),
				HttpUtility.UrlEncode(value.ToString()));
		}
		#endregion

		#region SerializeDate
		private String SerializeDateTime(PropertyInfo property)
		{
			object value = property.GetValue(this, null);
			return String.Format(
				"{0}={1}&",
				property.Name.Underscore(),
				HttpUtility.UrlEncode(((DateTime)value).ToString("o")));
		}
		#endregion

		#region SerializeListOfInt32
		private String SerializeListOfInt32(PropertyInfo property)
		{
			String result = String.Empty;
			object value = property.GetValue(this, null);
			List<Int32> castedValue = value as List<Int32>;

			foreach (var runner in castedValue)
			{
				result += String.Format(
					"{0}={1}&",
					property.Name.Underscore(),
					HttpUtility.UrlEncode(runner.ToString()));
			}

			return result;
		}
		#endregion

		#region GetUrlParameter
		/// <summary>
		/// Returns the UrlParameterAttribute associated to the specified property-argument.
		/// </summary>
		/// <param name="property">The property.</param>
		/// <returns></returns>
		private static UrlParameterAttribute GetUrlParameter(PropertyInfo property)
		{
			return (from current in property.GetCustomAttributes(false)
					where current.GetType() == typeof(UrlParameterAttribute)
					select current).First() as UrlParameterAttribute;
		}
		#endregion

		#region GetAllPrivateAndPublicPropertiesDecoratedWithUrlParameter
		/// <summary>
		/// Returns a collection of all properties in the specified type which are
		/// decorated with the UrlParameterAttribute.
		/// </summary>
		/// <param name="type">The type.</param>
		/// <returns></returns>
		private  static IEnumerable<PropertyInfo> GetAllPrivateAndPublicPropertiesDecoratedWithUrlParameter(Type type)
		{
			List<PropertyInfo> result = new List<PropertyInfo>();

			Type runner = type;
			while (runner != null)
			{
				var propertyInfoList = from current in runner.GetProperties(BindingFlags.NonPublic | BindingFlags.Public | BindingFlags.GetProperty | BindingFlags.SetProperty | BindingFlags.Instance | BindingFlags.DeclaredOnly)
									   where current.GetCustomAttributes(false).OfType<UrlParameterAttribute>().Count() > 0
									   select current;
				result.AddRange(propertyInfoList);
				runner = runner.BaseType;
			}

			return result;
		}
		#endregion

		#region Deserialize
		/// <summary>
		/// Deserializes the specified request to a ActiveQueryBaseClass
		/// </summary>
		/// <param name="request">The request.</param>
		/// <param name="queryType">Type of the query.</param>
		/// <returns></returns>
		internal static ActiveQueryBase Deserialize(System.Web.HttpRequest request, Type queryType)
		{
			ActiveQueryBase result = null;

			try
			{
				result = Activator.CreateInstance(queryType) as ActiveQueryBase;
				var propertyInfoList = ActiveQueryBase.GetAllPrivateAndPublicPropertiesDecoratedWithUrlParameter(queryType);

				foreach (var propertyRunner in propertyInfoList)
				{
					String serializedValue = request.Params[propertyRunner.Name.Underscore()];
					ActiveQueryBase.DeserializeSingleProperty(propertyRunner, serializedValue, result);
				}
			}
			catch (Exception ex)
			{
				throw new ActiveQueryException(ActiveQueryExceptionStrings.CantDeserialize, ex);
			}

			return result;
		}
		#endregion

		#region DeserializeSingleProperty
		private static void DeserializeSingleProperty(PropertyInfo property, String serializedValue, ActiveQueryBase result)
		{
			try
			{
				UrlParameterAttribute attribute = property.GetCustomAttributes(false).OfType<UrlParameterAttribute>().First();

				if (attribute.SupportedValues != null && !attribute.SupportedValues.Contains(serializedValue))
				{
					throw new ArgumentException(String.Format(ActiveQueryExceptionStrings.ArgumentValueIsNotSupported, property.Name, serializedValue));
				}

				Object deserializedValue = ActiveQueryBase.deserializers.ContainsKey(property.PropertyType) ?
					ActiveQueryBase.deserializers[property.PropertyType](property, serializedValue) :
					ActiveQueryBase.DeserializeString(property, serializedValue);

				property.SetValue(
						result,
						deserializedValue,
						null);
			}
			catch (Exception ex)
			{
				throw new ActiveQueryException(String.Format(ActiveQueryExceptionStrings.CantDeserializeProperty, property.Name), ex);
			}
		}
		#endregion

		#region DeserializeString
		private static Object DeserializeString(PropertyInfo property, String serializedValue)
		{
			return ActiveQueryBase.ChangeType(serializedValue, property.PropertyType);
		}
		#endregion

		#region DeserializeListOfInt32
		private static Object DeserializeListOfInt32(PropertyInfo property, String serializedValue)
		{
			return serializedValue == null ?
				new List<Int32>() :
				serializedValue.ToString().Split(',').Select(runner => Convert.ToInt32(runner)).ToList();
		}
		#endregion

		#region ChangeType
		/// <summary>
		/// Changes the type of the value to the conversionType.
		/// </summary>
		/// <param name="value">The value.</param>
		/// <param name="conversionType">Type of the conversion.</param>
		/// <returns></returns>
		private static Object ChangeType(Object value, Type conversionType)
		{
			Object result = null;

			try
			{
				if (value != null)
				{
					//According to the MSDN this has to be done for nullable types.
					if (
						conversionType.IsGenericType &&
						conversionType.GetGenericTypeDefinition().Equals(typeof(Nullable<>)))
					{
						conversionType = conversionType.GetGenericArguments()[0];
					}

					//Now really convert
					if (conversionType.IsEnum)
					{
						result = Enum.Parse(conversionType, value.ToString());
					}
					else if (conversionType == typeof(Guid))
					{
						result = new Guid(value.ToString());
					}
					else if (conversionType == typeof(DateTime) || conversionType == typeof(DateTime?))
					{
						result = DateTime.Parse(value.ToString(), CultureInfo.InvariantCulture);
					}
					else
					{
						result = Convert.ChangeType(value, conversionType, CultureInfo.InvariantCulture);
					}
				}
			}
			catch (Exception ex)
			{
				throw new ActiveQueryException(ActiveQueryExceptionStrings.CantChangeType, ex);
			}

			return result;
		}
		#endregion
	}
}
