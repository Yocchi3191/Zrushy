// Copyright (c) yoshioyocchi314@gmail.com
// Licensed under the MIT License.

using System;
using NUnit.Framework;
using UnityEngine;
using Zrushy.Core.Presentation.Unity;

namespace Zrushy.Core.Test.Unity.EditMode
{
    /// 仕様
    /// x IndexOf_指定したSpriteのinitialStateからのインデックスを返す
    /// x IndexOf_Transitionに無いSpriteを渡した場合例外をスロー
    /// Add_fromStateが複数のTransitionを作成しようとすると例外をスロー
    ///

    public class SpriteStatePatternTest
    {
        SpriteStatePattern _pattern;
        private Sprite _initialSprite;
        private Sprite _nextSprite;
        private Sprite _furtherSprite;

        [SetUp]
        public void SetUp()
        {
            _initialSprite = Sprite.Create(new Texture2D(1, 1), new Rect(0, 0, 1, 1), Vector2.zero);
            _nextSprite = Sprite.Create(new Texture2D(1, 1), new Rect(0, 0, 1, 1), Vector2.zero);
            _furtherSprite = Sprite.Create(new Texture2D(1, 1), new Rect(0, 0, 1, 1), Vector2.zero);

            _pattern = ScriptableObject.CreateInstance<SpriteStatePattern>();
            _pattern.initialState = _initialSprite;
            _pattern.Add(new StateTransition
            {
                fromState = _initialSprite,
                requiredDirection = CardinalDirection.Down,
                toState = _nextSprite
            });
            _pattern.Add(new StateTransition
            {
                fromState = _nextSprite,
                requiredDirection = CardinalDirection.Down,
                toState = _furtherSprite
            });
        }

        [Test]
        public void IndexOf_指定したSpriteのinitialStateからのインデックスを返す()
        {
            Assert.AreEqual(0, _pattern.IndexOf(_initialSprite));
            Assert.AreEqual(1, _pattern.IndexOf(_nextSprite));
        }

        [Test]
        public void IndexOf_Transitionに無いSpriteを渡した場合例外をスロー()
        {
            Sprite dummySprite = Sprite.Create(new Texture2D(1, 1), new Rect(0, 0, 1, 1), Vector2.zero);
            Assert.Throws<InvalidOperationException>(() => _pattern.IndexOf(dummySprite));
        }
    }
}
