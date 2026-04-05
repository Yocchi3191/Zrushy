using NSubstitute;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.EventSystems;
using Zrushy.Core.Application.UseCase.CanZrushy;
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
		Zrushable zrushable;
		IZrushyPermission zrushyPermission;
		ISpriteInputHandler spriteInputHandler;

		[SetUp]
		public void setup()
		{
			zrushable = new GameObject().AddComponent<Zrushable>();

			spriteInputHandler = Substitute.For<ISpriteInputHandler>();
			zrushyPermission = Substitute.For<IZrushyPermission>();

			zrushable.Construct(spriteInputHandler, zrushyPermission);
		}

		[TearDown]
		public void teardown()
		{
			Object.DestroyImmediate(zrushable);
		}

		// A Test behaves as an ordinary method
		[Test]
		public void マウス入力を受け取ったら_ずらしていいか問い合わせる()
		{
			// When
			zrushable.OnPointerClick(new PointerEventData(null));
			// Then
			zrushyPermission.Received(1).CanZrushy(Arg.Any<ZrushyInput>());
		}

		[Test]
		public void ずらしが許可されたら_マウス入力をISpriteInputHandlerに伝える()
		{
			// Given
			zrushyPermission.CanZrushy(Arg.Any<ZrushyInput>()).Returns(true);
			// When
			zrushable.OnPointerClick(new PointerEventData(null));
			// Then
			spriteInputHandler.Received(1).TryTransition(Arg.Any<ZrushyInput>());
		}

		[Test]
		public void ずらしが許可されなかったら_ISpriteInputHandlerに伝えない()
		{
			// Given
			zrushyPermission.CanZrushy(Arg.Any<ZrushyInput>()).Returns(false);
			// When
			zrushable.OnPointerClick(new PointerEventData(null));
			// Then
			spriteInputHandler.DidNotReceive().TryTransition(Arg.Any<ZrushyInput>());
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
