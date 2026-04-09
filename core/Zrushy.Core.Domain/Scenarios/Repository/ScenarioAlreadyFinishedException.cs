// Copyright (c) yoshioyocchi314@gmail.com
// Licensed under the MIT License.

using System;
using System.Runtime.Serialization;

namespace Zrushy.Core.Domain.Scenarios.Repository
{
    [Serializable]
    public class ScenarioAlreadyFinishedException : System.Exception
    {
        public ScenarioAlreadyFinishedException()
        {
        }

        public ScenarioAlreadyFinishedException(string message) : base(message)
        {
        }

        public ScenarioAlreadyFinishedException(string message, System.Exception innerException) : base(message, innerException)
        {
        }

        protected ScenarioAlreadyFinishedException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}
