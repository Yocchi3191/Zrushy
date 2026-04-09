// Copyright (c) yoshioyocchi314@gmail.com
// Licensed under the MIT License.

using NSubstitute;
using NSubstitute.ReceivedExtensions;
using NUnit.Framework;
using UnityEngine;
using Zrushy.Core.Presentation.Unity;

namespace Zrushy.Core.Test.Unity.EditMode
{
    public class SpriteStateMediatorTest
    {
        HoodieStateMediator _mediator;
        ISpriteStateNode _controller;
        ISpriteStateNode[] _dependents;
        ConstraintEntry[] _constraints;

        [SetUp]
        public void Setup()
        {
            _controller = Substitute.For<ISpriteStateNode>();
            _dependents = new ISpriteStateNode[]
            {
                Substitute.For<ISpriteStateNode>(),
                Substitute.For<ISpriteStateNode>(),
            };
            _constraints = Builder();

            _mediator = new GameObject().AddComponent<HoodieStateMediator>();
            _mediator.Construct(_controller, _dependents, _constraints);
        }


        [TearDown]
        public void TearDown()
        {
            GameObject.DestroyImmediate(_mediator.gameObject);
            foreach (var entry in _constraints)
                ScriptableObject.DestroyImmediate(entry);
        }

        // Dependent遷移時の条件違反チェックはmediator側で行う
        // Dependent側にチェック機構がないので
        // 作るにしてもDependentがControllerを知らなければいけないので、Mediatorパターンのメリットがなくなる

        [Test]
        public void Dependentが状態遷移した場合_違反していなければ遷移させない()
        {
            // Given
            _controller.CurrentState.Returns(_constraints[0].ControllerState); // controllerの状態を遷移させる

            ISpriteStateNode dependent = _dependents[0];
            dependent.CurrentState.Returns(_constraints[0].MaxAllowedState); // 遷移後の状態は許可されている状態
            dependent.IsAbove(Arg.Any<Sprite>()).Returns(false); // 遷移後の状態が違反していない

            // When
            _mediator.OnStateChanged(dependent);

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
            _controller.CurrentState.Returns(_constraints[2].ControllerState); // controllerの状態を遷移させる

            ISpriteStateNode dependent = _dependents[0];
            dependent.CurrentState.Returns(_constraints[1].MaxAllowedState); // 遷移後の状態は許可されている状態
            dependent.IsAbove(Arg.Any<Sprite>()).Returns(false); // 遷移後の状態が違反していない

            // When
            _mediator.OnStateChanged(dependent);

            // Then
            dependent.Received(1).IsAbove(Arg.Any<Sprite>()); // dependent遷移後の条件違反チェックはmediatorで行う
        }

        [Test]
        public void Dependentが状態遷移した場合_違反していれば遷移させる()
        {
            // Given
            _controller.CurrentState.Returns(_constraints[0].ControllerState); // 違反チェックにcontrollerのStateが必要
            ISpriteStateNode dependent = _dependents[0];
            dependent.IsAbove(Arg.Any<Sprite>()).Returns(true); // 遷移後の状態が違反している

            // When
            _mediator.OnStateChanged(dependent);

            // Then
            dependent.Received(1).IsAbove(Arg.Any<Sprite>()); // dependent遷移後の条件違反チェックはmediatorで行う
            dependent.Received(1).ForceState(Arg.Any<Sprite>()); // 違反しているdependentは強制遷移される
        }

        [Test]
        public void Controllerが状態遷移した場合_Dependentsのうち違反していないものは遷移させない()
        {
            // Given
            _controller.CurrentState.Returns(_constraints[0].ControllerState); // controllerの状態を遷移させる
            foreach (var dependent in _dependents)
            {
                dependent.IsAbove(Arg.Any<Sprite>()).Returns(false); // 状態が違反しているdependentはいない
            }

            // When
            _mediator.OnStateChanged(_controller);

            // Then
            foreach (var dependent in _dependents)
            {
                dependent.Received(1).IsAbove(Arg.Any<Sprite>()); // mediatorはdependentsに確認したか
                dependent.DidNotReceive<ISpriteStateNode>().ForceState(Arg.Any<Sprite>()); // 違反しているdependentはいないので、強制遷移したものはいない
            }
        }

        [Test]
        public void Controllerが状態遷移した場合_Dependentsのうち違反しているものを遷移させる()
        {
            // Given
            _controller.CurrentState.Returns(_constraints[0].ControllerState);
            foreach (var dependent in _dependents)
            {
                dependent.IsAbove(Arg.Any<Sprite>()).Returns(true); // 違反している
            }

            // When
            _mediator.OnStateChanged(_controller);

            // Then
            foreach (var dependent in _dependents)
            {
                // 違反しているISpriteStateNodeは、すべて現在許可されている状態に強制遷移させられている
                dependent.Received(1).ForceState(_constraints[0].MaxAllowedState);
            }
        }

        [Test]
        public void 三角測量_違反しているものとしていないものがある場合_違反しているものだけ遷移させられる()
        {
            // Given
            _controller.CurrentState.Returns(_constraints[0].ControllerState);
            _dependents[0].IsAbove(Arg.Any<Sprite>()).Returns(true); // 違反している
            _dependents[1].IsAbove(Arg.Any<Sprite>()).Returns(false); // 違反していない

            // When
            _mediator.OnStateChanged(_controller);

            // Then
            _dependents[0].Received(1).ForceState(_constraints[0].MaxAllowedState); // 0番目は違反しているので強制遷移させられる
            _dependents[1].DidNotReceive().ForceState(Arg.Any<Sprite>()); // 1番目は違反していないので遷移させられていない
        }

        [Test]
        public void Dependentの条件違反時の強制遷移は_Controllerの状態を基準にしたMaxAllowedState()
        {
            // Given
            _controller.CurrentState.Returns(_constraints[1].ControllerState);
            _dependents[0].IsAbove(Arg.Any<Sprite>()).Returns(true); // 強制遷移対象
            _dependents[1].IsAbove(Arg.Any<Sprite>()).Returns(false); // 対象外

            // When
            _mediator.OnStateChanged(_controller);

            // Then
            _dependents[0].Received().ForceState(_constraints[1].MaxAllowedState); // 0ではなく1のmaxに強制遷移している
            _dependents[0].DidNotReceive().ForceState(_constraints[0].MaxAllowedState); // 0ではなく1のmaxに強制遷移している
            _dependents[1].DidNotReceive().ForceState(_constraints[1].MaxAllowedState); // 何もされていない
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
