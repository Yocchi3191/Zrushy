using NSubstitute;
using NSubstitute.ReceivedExtensions;
using NUnit.Framework;
using UnityEngine;
using Zrushy.Core.Presentation.Unity;

/// Mediatorパターン
/// ISpriteStateNodeは状態遷移したらMediatorに通知する
/// 
/// ISpriteStateNodeには、他のISpriteStateNodeの状態遷移に影響があるもの(Controller)がいる
/// Mediatorは自分のパーツに関するISpriteStateNodeを知っている
/// 誰がControllerかも知っている
///  
/// MediatorはControllerの各状態ごとの、Dependentsに許可されている状態を知っている
/// 
/// 以上を使って、Controllerが状態遷移したら、MediatorはDependentsのうち違反しているものを遷移させる

/// TODO
/// ForceStateがMaxAllowedStateで呼ばれるか
/// 未登録ノードからの変更通知に対する例外スロー
/// 制約条件未対応時に例外スロー
/// 一部だけ違反している場合の挙動

namespace Zrushy.Core.Test.Unity.EditMode
{
	public class SpriteStateMediatorTest
	{
		HoodieStateMediator mediator;
		ISpriteStateNode controller;
		ISpriteStateNode[] dependents;
		ConstraintEntry[] constraints;

		[SetUp]
		public void Setup()
		{
			controller = Substitute.For<ISpriteStateNode>();
			dependents = new ISpriteStateNode[]
			{
				Substitute.For<ISpriteStateNode>(),
				Substitute.For<ISpriteStateNode>(),
			};
			constraints = Builder();

			mediator = new GameObject().AddComponent<HoodieStateMediator>();
			mediator.Construct(controller, dependents, constraints);
		}


		[TearDown]
		public void TearDown()
		{
			GameObject.DestroyImmediate(mediator.gameObject);
			foreach (var entry in constraints)
				ScriptableObject.DestroyImmediate(entry);
		}

		// Dependent遷移時の条件違反チェックはmediator側で行う
		// Dependent側にチェック機構がないので
		// 作るにしてもDependentがControllerを知らなければいけないので、Mediatorパターンのメリットがなくなる

		[Test]
		public void Dependentが状態遷移した場合_違反していなければ遷移させない()
		{
			// Given
			controller.CurrentState.Returns(constraints[0].ControllerState); // controllerの状態を遷移させる

			ISpriteStateNode dependent = dependents[0];
			dependent.CurrentState.Returns(constraints[0].MaxAllowedState); // 遷移後の状態は許可されている状態
			dependent.IsAbove(Arg.Any<Sprite>()).Returns(false); // 遷移後の状態が違反していない

			// When
			mediator.OnStateChanged(dependent);

			// Then
			dependent.Received(1).IsAbove(Arg.Any<Sprite>()); // dependent遷移後の条件違反チェックはmediatorで行う
		}

		[Test]
		public void Dependentが状態遷移した場合_違反していなければ遷移させない_三角測量()
		{
			// ConstraintEntry 0--1--2
			// controller: 2
			// dependent: 1 に遷移
			// この場合もdependentは違反していないので、強制遷移させられない

			// Given
			controller.CurrentState.Returns(constraints[2].ControllerState); // controllerの状態を遷移させる

			ISpriteStateNode dependent = dependents[0];
			dependent.CurrentState.Returns(constraints[1].MaxAllowedState); // 遷移後の状態は許可されている状態
			dependent.IsAbove(Arg.Any<Sprite>()).Returns(false); // 遷移後の状態が違反していない

			// When
			mediator.OnStateChanged(dependent);

			// Then
			dependent.Received(1).IsAbove(Arg.Any<Sprite>()); // dependent遷移後の条件違反チェックはmediatorで行う
		}

		[Test]
		public void Dependentが状態遷移した場合_違反していれば遷移させる()
		{
			// Given
			var dependent = dependents[0];
			dependent.IsAbove(Arg.Any<Sprite>()).Returns(true); // 遷移後の状態が違反している

			// When
			mediator.OnStateChanged(dependent);

			// Then
			dependent.Received(1).IsAbove(Arg.Any<Sprite>()); // dependent遷移後の条件違反チェックはmediatorで行う
			dependent.Received(1).ForceState(Arg.Any<Sprite>()); // 違反しているdependentは強制遷移される
		}

		[Test]
		public void Controllerが状態遷移した場合_Dependentsのうち違反していないものは遷移させない()
		{
			// Given
			controller.CurrentState.Returns(constraints[0].ControllerState); // controllerの状態を遷移させる
			foreach (var dependent in dependents)
			{
				dependent.IsAbove(Arg.Any<Sprite>()).Returns(false); // 状態が違反しているdependentはいない
			}

			// When
			mediator.OnStateChanged(controller);

			// Then
			foreach (var dependent in dependents)
			{
				dependent.Received(1).IsAbove(Arg.Any<Sprite>()); // mediatorはdependentsに確認したか
				dependent.DidNotReceive<ISpriteStateNode>().ForceState(Arg.Any<Sprite>()); // 違反しているdependentはいないので、強制遷移したものはいない
			}
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

		private ConstraintEntry[] Builder()
		{
			return new ConstraintEntry[]
			{
				CreateEntry(CreateSprite(), CreateSprite()),
				CreateEntry(CreateSprite(), CreateSprite()),
				CreateEntry(CreateSprite(), CreateSprite()),
			};
		}

		private ConstraintEntry CreateEntry(Sprite controllerState, Sprite maxAllowedState)
		{
			var entry = ScriptableObject.CreateInstance<ConstraintEntry>();
			entry.ControllerState = controllerState;
			entry.MaxAllowedState = maxAllowedState;
			return entry;
		}

		private Sprite CreateSprite()
		{
			var texture = new Texture2D(1, 1);
			return Sprite.Create(texture, new Rect(0, 0, 1, 1), Vector2.zero);
		}
	}
}
