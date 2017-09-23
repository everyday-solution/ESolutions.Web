using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ESolutions.Web
{
	[global::System.Serializable]
	public class ActiveQueryException : Exception
	{
		public ActiveQueryException() { }
		public ActiveQueryException(string message) : base(message) { }
		public ActiveQueryException(string message, Exception inner) : base(message, inner) { }
		protected ActiveQueryException(
		 System.Runtime.Serialization.SerializationInfo info,
		 System.Runtime.Serialization.StreamingContext context)
			: base(info, context)
		{ }
	}
}
