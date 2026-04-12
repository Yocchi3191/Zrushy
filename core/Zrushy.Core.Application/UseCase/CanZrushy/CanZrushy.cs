// Copyright (c) yoshioyocchi314@gmail.com
// Licensed under the MIT License.

namespace Zrushy.Core.Application.UseCase.CanZrushy
{
    public class CanZrushy : IZrushyPermission
    {
        bool IZrushyPermission.CanZrushy(ZrushyInput input) => true; // TODO ずらし確認クエリを実装する
    }
}
