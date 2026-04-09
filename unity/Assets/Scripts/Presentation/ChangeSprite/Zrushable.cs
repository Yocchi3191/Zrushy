// Copyright (c) yoshioyocchi314@gmail.com
// Licensed under the MIT License.

using UnityEngine;
using UnityEngine.EventSystems;
using Zrushy.Core.Application.UseCase.CanZrushy;

namespace Zrushy.Core.Presentation.Unity
{
    public class Zrushable : MonoBehaviour, IBeginDragHandler, IEndDragHandler
    {
        private ISpriteInputHandler _spriteInputHandler;
        private IZrushyPermission _zrushyPermission;

        private Vector2 _dragStartPosition;


        internal void Construct(ISpriteInputHandler spriteInputHandler, IZrushyPermission zrushyPermission)
        {
            _spriteInputHandler = spriteInputHandler;
            _zrushyPermission = zrushyPermission;
        }

        public void OnBeginDrag(PointerEventData eventData)
        {
            _dragStartPosition = eventData.position; // ドラッグ開始位置を保存したほうがドラッグ方向の計算が安定するらしい
        }

        public void OnEndDrag(PointerEventData eventData)
        {
            Vector2 endPosition = eventData.position;
            Vector2 dragVector = endPosition - _dragStartPosition;

            ZrushyInput input = new ZrushyInput(new System.Numerics.Vector2(dragVector.x, dragVector.y));
            if (!_zrushyPermission.CanZrushy(input))
                return;

            _spriteInputHandler.TryTransition(input);
        }
    }
}
