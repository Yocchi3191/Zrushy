// Copyright (c) yoshioyocchi314@gmail.com
// Licensed under the MIT License.

using System;
using NSubstitute;
using NSubstitute.ReceivedExtensions;
using NUnit.Framework;
using UnityEngine;
using Zrushy.Core.Presentation.Unity;

namespace Zrushy.Core.Test.Unity.EditMode
{
    public class SpriteStateMediatorTest
    {
        SpriteStateMediator _mediator;
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

            _mediator = new GameObject().AddComponent<SpriteStateMediator>();
            _mediator.Construct(_controller, _dependents, _constraints);
        }


        [TearDown]
        public void TearDown()
        {
            GameObject.DestroyImmediate(_mediator.gameObject);
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
            dependent.IsAbove(Arg.Any<int>()).Returns(false); // 遷移後の状態が違反していない

            // When
            dependent.OnStateChanged += Raise.Event<Action<ISpriteStateNode>>(dependent);

            // Then
            dependent.Received(1).IsAbove(Arg.Any<int>()); // dependent遷移後の条件違反チェックはmediatorで行う
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
            dependent.IsAbove(Arg.Any<int>()).Returns(false); // 遷移後の状態が違反していない

            // When
            dependent.OnStateChanged += Raise.Event<Action<ISpriteStateNode>>(dependent);

            // Then
            dependent.Received(1).IsAbove(Arg.Any<int>()); // dependent遷移後の条件違反チェックはmediatorで行う
        }

        [Test]
        public void Dependentが状態遷移した場合_違反していれば遷移させる()
        {
            // Given
            _controller.CurrentState.Returns(_constraints[0].ControllerState); // 違反チェックにcontrollerのStateが必要
            ISpriteStateNode dependent = _dependents[0];
            dependent.IsAbove(Arg.Any<int>()).Returns(true); // 遷移後の状態が違反している

            // When
            dependent.OnStateChanged += Raise.Event<Action<ISpriteStateNode>>(dependent);

            // Then
            dependent.Received(1).IsAbove(Arg.Any<int>()); // dependent遷移後の条件違反チェックはmediatorで行う
            dependent.Received(1).ForceState(Arg.Any<int>()); // 違反しているdependentは強制遷移される
        }

        [Test]
        public void Controllerが状態遷移した場合_Dependentsのうち違反していないものは遷移させない()
        {
            // Given
            _controller.CurrentState.Returns(_constraints[0].ControllerState); // controllerの状態を遷移させる
            foreach (var dependent in _dependents)
            {
                dependent.IsAbove(Arg.Any<int>()).Returns(false); // 状態が違反しているdependentはいない
            }

            // When
            _controller.OnStateChanged += Raise.Event<Action<ISpriteStateNode>>(_controller);

            // Then
            foreach (var dependent in _dependents)
            {
                dependent.Received(1).IsAbove(Arg.Any<int>()); // mediatorはdependentsに確認したか
                dependent.DidNotReceive<ISpriteStateNode>().ForceState(Arg.Any<int>()); // 違反しているdependentはいないので、強制遷移したものはいない
            }
        }

        [Test]
        public void Controllerが状態遷移した場合_Dependentsのうち違反しているものを遷移させる()
        {
            // Given
            _controller.CurrentState.Returns(_constraints[0].ControllerState);
            foreach (var dependent in _dependents)
            {
                dependent.IsAbove(Arg.Any<int>()).Returns(true); // 違反している
            }

            // When
            _controller.OnStateChanged += Raise.Event<Action<ISpriteStateNode>>(_controller);

            // Then
            foreach (var dependent in _dependents)
            {
                // 違反しているISpriteStateNodeは、すべて現在許可されている状態に強制遷移させられている
                dependent.Received(1).ForceState(_constraints[0].MaxAllowedStateIndex);
            }
        }

        [Test]
        public void 三角測量_違反しているものとしていないものがある場合_違反しているものだけ遷移させられる()
        {
            // Given
            _controller.CurrentState.Returns(_constraints[0].ControllerState);
            _dependents[0].IsAbove(Arg.Any<int>()).Returns(true); // 違反している
            _dependents[1].IsAbove(Arg.Any<int>()).Returns(false); // 違反していない

            // When
            _controller.OnStateChanged += Raise.Event<Action<ISpriteStateNode>>(_controller);

            // Then
            _dependents[0].Received(1).ForceState(_constraints[0].MaxAllowedStateIndex); // 0番目は違反しているので強制遷移させられる
            _dependents[1].DidNotReceive().ForceState(Arg.Any<int>()); // 1番目は違反していないので遷移させられていない
        }

        [Test]
        public void Dependentの条件違反時の強制遷移は_Controllerの状態を基準にしたMaxAllowedState()
        {
            // Given
            _controller.CurrentState.Returns(_constraints[1].ControllerState);
            _dependents[0].IsAbove(Arg.Any<int>()).Returns(true); // 強制遷移対象
            _dependents[1].IsAbove(Arg.Any<int>()).Returns(false); // 対象外

            // When
            _controller.OnStateChanged += Raise.Event<Action<ISpriteStateNode>>(_controller);

            // Then
            _dependents[0].Received().ForceState(_constraints[1].MaxAllowedStateIndex); // 0ではなく1のmaxに強制遷移している
            _dependents[0].DidNotReceive().ForceState(_constraints[0].MaxAllowedStateIndex); // 0ではなく1のmaxに強制遷移している
            _dependents[1].DidNotReceive().ForceState(_constraints[1].MaxAllowedStateIndex); // 何もされていない
        }

        private ConstraintEntry[] Builder()
        {
            return new ConstraintEntry[]
            {
                CreateEntry(CreateSprite(), 0),
                CreateEntry(CreateSprite(), 1),
                CreateEntry(CreateSprite(), 2),
            };
        }

        private ConstraintEntry CreateEntry(Sprite controllerState, int maxAllowedIndex)
        {
            ConstraintEntry entry = new ConstraintEntry(controllerState, maxAllowedIndex);
            return entry;
        }

        private Sprite CreateSprite()
        {
            var texture = new Texture2D(1, 1);
            return Sprite.Create(texture, new Rect(0, 0, 1, 1), Vector2.zero);
        }
    }
}
