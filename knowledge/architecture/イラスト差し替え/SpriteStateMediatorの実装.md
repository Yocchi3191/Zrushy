# SpriteStateMediator の実装設計

作成日: 2026-04-02

## 目的

`SpriteStateMediator` を TDD（BDD スタイル）で実装するための設計。
`SpriteState`（MonoBehaviour）を直接テストに使うと Unity ライフサイクルへの依存が生じるため、
インターフェースを介してテスト可能にする。

---

## インターフェース設計

### 新規: `ISpriteStateNode`

```csharp
internal interface ISpriteStateNode
{
    event Action<Sprite> OnStateChanged;
    bool IsAbove(Sprite maxAllowed);
    void ForceState(Sprite state);
}
```

controller と dependent の両方に使う統合インターフェース。
`SpriteState` がこれを実装する。

### 変更: `ISpriteStateRouter`

```csharp
internal interface ISpriteStateRouter
{
    void Handle(PartInput input);
    ISpriteStateNode Controller { get; }
    ISpriteStateNode[] Dependents { get; }
}
```

`SpriteState` → `ISpriteStateNode` に型を変更。

---

## SpriteState への追加実装

### `ForceState(Sprite state)`

既存の `SetState` に委譲する。

### `IsAbove(Sprite maxAllowed)`

`SpriteStatePattern.transitions` を `initialState` から辿り、
`maxAllowed` より後に `currentState` が現れる場合 `true` を返す。

```
initialState → A → maxAllowed → currentState → ...
                                ↑ IsAbove = true
```

`maxAllowed` が見つからない場合は `false`（安全側に倒す）。

---

## テスト環境セットアップ

### 1. NuGetForUnity の導入

`unity/Packages/manifest.json` に追加:

```json
"com.glitchenzo.nugetforunity": "https://github.com/GlitchEnzo/NuGetForUnity.git?path=/src/NuGetForUnity",
```

Unity Editor を開き、メニュー **NuGet > Manage NuGet Packages** から `NSubstitute` をインストール。

### 2. テスト用アセンブリ定義

`unity/Assets/Tests/EditMode/` に以下を作成:

**`ZrushyPresentationTests.asmdef`**

```json
{
	"name": "ZrushyPresentationTests",
	"references": [
		"UnityEngine.TestRunner",
		"UnityEditor.TestRunner",
		"Zrushy.Core.Presentation.Unity"
	],
	"includePlatforms": ["Editor"],
	"allowUnsafeCode": false,
	"overrideReferences": true,
	"precompiledReferences": [
		"UnityEngine.TestRunner.dll",
		"UnityEditor.TestRunner.dll",
		"NSubstitute.dll",
		"Castle.Core.dll"
	],
	"autoReferenced": false,
	"defineConstraints": ["UNITY_INCLUDE_TESTS"]
}
```

`internal` なインターフェースへのアクセスのため、`Zrushy.Core.Presentation.Unity.asmdef` に追加:

```json
"internalsVisibleTo": ["ZrushyPresentationTests"]
```

> NuGetForUnity が NSubstitute をどこに配置するかによって `precompiledReferences` のパスが変わる場合があります。インストール後に調整してください。

---

## テスト構造（BDD スタイル）

NUnit + NSubstitute で記述。クラス名・メソッド名で Given/When/Then を表現する。

```csharp
public class SpriteStateMediatorTests
{
    // Given: controller が constraintEntry の controllerState に遷移した
    // When:  dependent の currentState が maxAllowedState を超えている
    // Then:  dependent に ForceState(maxAllowedState) が呼ばれる

    [Test]
    public void WhenControllerChanges_DependentAboveMax_IsForcedDown() { ... }

    // When:  dependent の currentState が maxAllowedState 以下
    // Then:  dependent に ForceState は呼ばれない

    [Test]
    public void WhenControllerChanges_DependentBelowMax_IsNotForced() { ... }

    // When:  変化後の controllerState に対応する ConstraintEntry がない
    // Then:  何もしない

    [Test]
    public void WhenControllerChanges_NoMatchingConstraint_DoesNothing() { ... }
}
```

NSubstitute の使い方（参考）:

```csharp
var controller = Substitute.For<ISpriteStateNode>();
var dependent  = Substitute.For<ISpriteStateNode>();
var router     = Substitute.For<ISpriteStateRouter>();

router.Controller.Returns(controller);
router.Dependents.Returns(new[] { dependent });
dependent.IsAbove(maxAllowed).Returns(true);

controller.OnStateChanged += Raise.Event<Action<Sprite>>(controllerState);

dependent.Received(1).ForceState(maxAllowed);
```

---

## 変更ファイル一覧

| ファイル                                        | 変更種別                                               |
| ----------------------------------------------- | ------------------------------------------------------ |
| `ChangeSprite/ISpriteStateNode.cs`              | 新規                                                   |
| `ChangeSprite/SpriteState.cs`                   | `ISpriteStateNode` 実装追加（`ForceState`, `IsAbove`） |
| `ISpriteStateRouter.cs`                         | 型変更（`SpriteState` → `ISpriteStateNode`）           |
| `Tests/EditMode/ZrushyPresentationTests.asmdef` | 新規                                                   |
| `Tests/EditMode/SpriteStateMediatorTests.cs`    | 新規                                                   |
| `Packages/manifest.json`                        | NuGetForUnity 追加                                     |
