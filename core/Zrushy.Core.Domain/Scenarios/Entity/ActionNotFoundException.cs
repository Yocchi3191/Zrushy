using System;
using System.Runtime.Serialization;

namespace Zrushy.Core.Domain.Scenarios.Entity
{
	[Serializable]
	internal class ActionNotFoundException : System.Exception
	{
		public ActionNotFoundException()
		{
		}

		public ActionNotFoundException(string message) : base(message)
		{
		}

		public ActionNotFoundException(string message, System.Exception innerException) : base(message, innerException)
		{
		}

		protected ActionNotFoundException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}
	}
}