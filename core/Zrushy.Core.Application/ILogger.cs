// Copyright (c) yoshioyocchi314@gmail.com
// Licensed under the MIT License.

namespace Zrushy.Core.Application
{
    /// <summary>
    /// ログを出すためのインターフェース
    /// 本当はunity ILoggerでいいが、coreは参照できないのでこっちで定義してunity側で実装する
    /// </summary>
    public interface ILogger
    {
        void Info(string message);
        void Warn(string message);
        void Error(string message);
        void Debug(string message);
    }
}
