using System;
using System.Runtime.Serialization;

namespace Zrushy.Core.Infrastructure.Engine
{
	[Serializable]
	internal class ScenarioNotStartedException : Exception
	{
		public ScenarioNotStartedException()
		{
		}

		public ScenarioNotStartedException(string message) : base(message)
		{
		}

		public ScenarioNotStartedException(string message, Exception innerException) : base(message, innerException)
		{
		}

		protected ScenarioNotStartedException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}
	}
}