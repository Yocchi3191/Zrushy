// Copyright (c) yoshioyocchi314@gmail.com
// Licensed under the MIT License.

namespace Zrushy.Core.Application.UseCase.CanZrushy
{
    /// <summary>
    /// 服や体勢などの「ずらし」を実行していいか問い合わせるためのインターフェース
    /// </summary>
    public interface IZrushyPermission
    {
        bool CanZrushy(ZrushyInput input);
    }
}
