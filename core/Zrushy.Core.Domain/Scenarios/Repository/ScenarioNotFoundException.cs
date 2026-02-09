using System;
using System.Runtime.Serialization;
using Zrushy.Core.Domain.Scenarios.ValueObject;

namespace Zrushy.Core.Domain.Scenarios.Repository
{
	[Serializable]
	public class ScenarioNotFoundException : System.Exception
	{
		public ScenarioID ScenarioID { get; }

		public ScenarioNotFoundException()
		{
		}

		public ScenarioNotFoundException(string message) : base(message)
		{
		}

		public ScenarioNotFoundException(ScenarioID scenarioID)
			: base($"Scenario '{scenarioID.Value}' not found. Please create a corresponding .yarn file.")
		{
			ScenarioID = scenarioID;
		}

		public ScenarioNotFoundException(string message, System.Exception innerException) : base(message, innerException)
		{
		}

		protected ScenarioNotFoundException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}
	}
}
