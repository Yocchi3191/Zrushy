// Copyright (c) yoshioyocchi314@gmail.com
// Licensed under the MIT License.

using System;
using System.Runtime.Serialization;

namespace Zrushy.Core.Domain.Scenarios.Repository
{
    [Serializable]
    public class ScenarioNotStartedException : System.Exception
    {
        public ScenarioNotStartedException()
        {
        }

        public ScenarioNotStartedException(string message) : base(message)
        {
        }

        public ScenarioNotStartedException(string message, System.Exception innerException) : base(message, innerException)
        {
        }

        protected ScenarioNotStartedException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}
