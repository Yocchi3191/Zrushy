using System.Collections;
using NSubstitute;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using Zrushy.Core.Presentation.Unity;

/// TODO
/// 振る舞い: 入力に応じてイラストを差し替える
/// 入力: Clickable
/// 入力判定(有効？) -> 有効なら差し替え&状態遷移 : SpriteState
/// Clickable "*" --> "1" SpriteState
/// 
/// 特殊ケース: パーカーのフードの開閉
/// 服LRの開閉はジッパーの状態に依存する
/// ジッパーの状態遷移に応じて服LRの状態も遷移させる必要がある
/// 複数SpriteStateの連携: SpriteStateMediator
///
/// SpriteStateは状態遷移する入力タイプが決まっている
/// 同じ入力タイプを登録すると、入力が片方の状態遷移にだけ持っていかれる
/// => clickable分けたら？ ……そうかも
/// ということはclickableは自分の入力を伝えるSpriteStateを知っている必要がある ほんとか？
/// ここなにかいい方法さがす
///
/// clickable "1" -- "*" SpriteState があるなら、routerは必要
/// ある？
/// ……なさそう
/// というか、無くできる。
/// clickableの解釈を「おさわり」できる箇所に限定する
/// 服とかの一部部位は「マウス入力で状態を変えられる」オブジェクトに解釈を変える。つまり別オブジェクト
/// とすると、別オブジェクト "1' -- "1" SpriteStateにできる
///


namespace Zrushy.Core.Test.Unity.EditMode
{
	public class SpriteStateMediatorTest
	{
		ISpriteStateRouter router;
		SpriteStateMediator mediator;

		[SetUp]
		public void Setup()
		{
			router = Substitute.For<ISpriteStateRouter>();
			mediator = new GameObject().AddComponent<SpriteStateMediator>();
		}

		[TearDown]
		public void TearDown()
		{
			GameObject.DestroyImmediate(mediator.gameObject);
		}

		[UnityTest]
		public IEnumerator Controllerが状態遷移したら_MaxStateより上のDependentsは_強制的に状態遷移させられる()
		{
			// Given
			var controllerState = NewSprite();
			var maxAllowed = NewSprite();

			var controller = Substitute.For<ISpriteStateNode>();
			var dependent = Substitute.For<ISpriteStateNode>();
			router.Controller.Returns(controller);
			router.Dependents.Returns(new[] { dependent });

			var entry = ScriptableObject.CreateInstance<ConstraintEntry>();
			entry.ControllerState = controllerState;
			entry.maxAllowedState = maxAllowed;

			mediator.Construct(router, new[] { entry });

			dependent.IsAbove(maxAllowed).Returns(true);

			yield return null; // Start() を走らせて OnStateChanged を購読させる

			// When
			controller.OnStateChanged += Raise.Event<System.Action<Sprite>>(controllerState);

			// Then
			dependent.Received(1).ForceState(maxAllowed);
		}

		// When:  dependent の currentState が maxAllowedState 以下
		// Then:  dependent に ForceState は呼ばれない
		[Test]
		public void Controllerが状態遷移したとき_MaxAllowedStateより下の状態にいるDependentsは_状態遷移しない() { }

		// When:  変化後の controllerState に対応する ConstraintEntry がない
		// Then:  何もしない
		[Test]
		public void Controllerが状態遷移したとき_対応するConstraintEntryがない場合は_何もしない() { }

		Sprite NewSprite() =>
			Sprite.Create(new Texture2D(1, 1), new Rect(0, 0, 1, 1), Vector2.zero);
	}
}
