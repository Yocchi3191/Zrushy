// Copyright (c) yoshioyocchi314@gmail.com
// Licensed under the MIT License.

using NUnit.Framework;
using Zrushy.Core.Presentation.Unity.ChangeSprite;

namespace Zrushy.Core.Test.Unity.EditMode
{
    /// 仕様
    /// ずらし入力(ZrushyInput)を受け取って、入力に応じて状態を遷移させる
    /// 遷移したらmediatorに通知する(しなくてもいい) <= しなくてもいいの？ よさそう
    /// mediatorから調整する用に、違反判定、強制遷移機能を持たせる
    ///
    /// IsAbove
    /// SpriteStateは遷移可能な状態一覧(StatePattern)を持つ
    /// 状態一覧には並び順がある？(実装してないかも)
    /// IsAbove(Sprite)を渡すと、StatePatternを元により上(先？)の状態かどうかを返す
    /// ConstraintEntryはどうなるんだよ！
    /// ConstraintEntry.MaxAllowedState の扱いを考えないといけない
    /// 状態違反時の遷移先&IsAboveに使うパラメータ？
    ///
    /// IsAboveの根拠データ構造
    /// Statepatternの並び順 => int(index?)
    /// まあindexでいいだろ
    /// TODO インデックス形式でステートの順序データを作る
    ///
    /// イベント発火(OnStateChanged)これは使わない
    /// いや、使ってもいいかも
    /// OnStateChanged(this)して、mediatorにはcontroller, dependentsのイベントを購読させればいけそう
    /// その場合 mediator.OnStateChanged(changed)は呼び出す形じゃなくてイベントに合わせて発火する形にするか
    /// TODO SpriteStateMediatorの調整機能呼び出しルートを OnStateChangedを叩く => ISpriteStateNodeの変更イベント購読 に変える
    ///

    public class SpriteStateTest
    {
        private readonly SpriteState spriteState;

        [Test]
        public void SpriteStateSimplePasses()
        {

        }

        // A UnityTest behaves like a coroutine in Play Mode. In Edit Mode you can use
        // `yield return null;` to skip a frame.
        //[UnityTest]
        //public IEnumerator SpriteStateWithEnumeratorPasses()
        //{
        //    // Use the Assert class to test conditions.
        //    // Use yield to skip a frame.
        //    yield return null;
        //}
    }
}
