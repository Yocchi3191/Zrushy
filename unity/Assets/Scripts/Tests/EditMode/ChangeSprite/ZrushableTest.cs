// Copyright (c) yoshioyocchi314@gmail.com
// Licensed under the MIT License.

using NSubstitute;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.EventSystems;
using Zrushy.Core.Application.UseCase.Execute;
using Zrushy.Core.Presentation.Unity;

namespace Zrushy.Core.Test.Unity.EditMode
{
    /// <summary>
    /// Zrushable のテスト
    /// 
    /// Zrushableの責務
    /// - マウス入力を受け取る
    /// - 入力から「ずらし」情報を作成する
    /// - 作成した「ずらし」を実行していいか、Domainに問い合わせる
    /// - OKなら、マウス入力をSpriteInputHandlerに伝達する
    /// </summary>
    public class ZrushableTest
    {
        Zrushable _zrushable;
        IZrushyClothing _zrushyPermission;
        ISpriteInputHandler _spriteInputHandler;

        [SetUp]
        public void setup()
        {
            _zrushable = new GameObject().AddComponent<Zrushable>();

            _spriteInputHandler = Substitute.For<ISpriteInputHandler>();
            _zrushyPermission = Substitute.For<IZrushyClothing>();

            _zrushable.Construct(_spriteInputHandler, _zrushyPermission);
        }

        [TearDown]
        public void teardown()
        {
            Object.DestroyImmediate(_zrushable);
        }

        // A Test behaves as an ordinary method
        [Test]
        public void ドラッグしたら_ずらしていいか問い合わせる()
        {
            // When
            _zrushable.OnBeginDrag(new PointerEventData(null));
            _zrushable.OnEndDrag(new PointerEventData(null));
            // Then
            _zrushyPermission.Received(1).Execute(Arg.Any<ZrushyInput>());
        }

        [Test]
        public void ずらしが許可されたら_マウス入力をISpriteInputHandlerに伝える()
        {
            // Given
            _zrushyPermission.Execute(Arg.Any<ZrushyInput>()).Returns(true);
            // When
            _zrushable.OnBeginDrag(new PointerEventData(null));
            _zrushable.OnEndDrag(new PointerEventData(null));
            // Then
            _spriteInputHandler.Received(1).TryTransition(Arg.Any<ZrushyInput>());
        }

        [Test]
        public void ずらしが許可されなかったら_ISpriteInputHandlerに伝えない()
        {
            // Given
            _zrushyPermission.Execute(Arg.Any<ZrushyInput>()).Returns(false);
            // When
            _zrushable.OnBeginDrag(new PointerEventData(null));
            _zrushable.OnEndDrag(new PointerEventData(null));
            // Then
            _spriteInputHandler.DidNotReceive().TryTransition(Arg.Any<ZrushyInput>());
        }

        // A UnityTest behaves like a coroutine in Play Mode. In Edit Mode you can use
        // `yield return null;` to skip a frame.
        //[UnityTest]
        //public IEnumerator ZrushableTestWithEnumeratorPasses()
        //{
        //	// Use the Assert class to test conditions.
        //	// Use yield to skip a frame.
        //	yield return null;
        //}
    }
}
