// Copyright (c) yoshioyocchi314@gmail.com
// Licensed under the MIT License.

using ILogger = Zrushy.Core.Application.ILogger;

namespace Zrushy.Core.Infrastructure.Unity
{
    public class Logger : ILogger
    {
        public void Debug(string message) => UnityEngine.Debug.Log(message);
        public void Info(string message) => UnityEngine.Debug.Log(message);
        public void Warn(string message) => UnityEngine.Debug.LogWarning(message);
        public void Error(string message) => UnityEngine.Debug.LogError(message);
    }
}
