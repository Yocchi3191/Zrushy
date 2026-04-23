// Copyright (c) yoshioyocchi314@gmail.com
// Licensed under the MIT License.

using UnityEngine;
using UnityEngine.EventSystems;
using Zenject;
using Zrushy.Core.Application.UseCase.CanZrushy;

namespace Zrushy.Core.Presentation.Unity
{
    public class Zrushable : MonoBehaviour, IDragHandler, IBeginDragHandler, IEndDragHandler
    {
        private ISpriteInputHandler _spriteInputHandler;
        [Inject] private IZrushyClothing _zrushyPermission;

        private Vector2 _dragStartPosition;
        [SerializeField] private string _clothingID;


        internal void Construct(ISpriteInputHandler spriteInputHandler, IZrushyClothing zrushyPermission)
        {
            _spriteInputHandler = spriteInputHandler;
            _zrushyPermission = zrushyPermission;
        }

        public void OnBeginDrag(PointerEventData eventData)
        {
            _dragStartPosition = eventData.position; // ドラッグ開始位置を保存したほうがドラッグ方向の計算が安定するらしい
        }

        private void Awake()
        {
            _spriteInputHandler = GetComponent<ISpriteInputHandler>();
        }

        public void OnEndDrag(PointerEventData eventData)
        {
            Vector2 endPosition = eventData.position;
            Vector2 dragVector = endPosition - _dragStartPosition;

            ZrushyInput input = new ZrushyInput(_clothingID, new System.Numerics.Vector2(dragVector.x, dragVector.y));
            if (!_zrushyPermission.Execute(input))
                return;

            _spriteInputHandler.TryTransition(input);
        }

        public void OnDrag(PointerEventData eventData) { }
    }
}
