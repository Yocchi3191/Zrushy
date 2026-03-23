// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace Zrushy.Core.Application
{
    /// <summary>
    /// ログを出すためのインターフェース
    /// 本当はunity ILoggerでいいが、coreは参照できないのでこっちで定義してunity側で実装する
    /// </summary>
    public interface ILogger
    {
        public void Info(string message);
        public void Warn(string message);
        public void Error(string message);
        public void Debug(string message);
    }
}
