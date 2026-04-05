using NUnit.Framework;
using UnityEngine;
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
		HoodieStateMediator mediator;

		[SetUp]
		public void Setup()
		{
			mediator = new GameObject().AddComponent<HoodieStateMediator>();
		}

		[TearDown]
		public void TearDown()
		{
			GameObject.DestroyImmediate(mediator.gameObject);
		}

		[Test]
		public void Controllerが状態遷移した場合_Dependentsのうち違反していないものは遷移させない()
		{
			// TODO
			Assert.Fail("まだ実装されていません");
			// 1. ConstraintEntryを用意する
			// 2. controllerとdependentsを用意する
			// 3. controllerの状態を遷移させる
			// 4. 遷移後のcontrollerの状態に応じて、dependentsのうち違反していないものが遷移していないことを確認する
		}

		[Test]
		public void Controllerが状態遷移した場合_Dependentsのうち違反しているものを遷移させる()
		{
			// TODO
			Assert.Fail("まだ実装されていません");
			// 1. ConstraintEntryを用意する
			// 2. controllerとdependentsを用意する
			// 3. controllerの状態を遷移させる
			// 4. 遷移後のcontrollerの状態に応じて、dependentsのうち違反しているものが遷移していることを確認する
		}

	}
}
