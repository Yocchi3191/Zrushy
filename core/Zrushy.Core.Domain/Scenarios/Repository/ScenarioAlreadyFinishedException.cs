using System;
using System.Runtime.Serialization;

namespace Zrushy.Core.Infrastructure.Engine
{
	[Serializable]
	public class ScenarioAlreadyFinishedException : Exception
	{
		public ScenarioAlreadyFinishedException()
		{
		}

		public ScenarioAlreadyFinishedException(string message) : base(message)
		{
		}

		public ScenarioAlreadyFinishedException(string message, Exception innerException) : base(message, innerException)
		{
		}

		protected ScenarioAlreadyFinishedException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}
	}
}