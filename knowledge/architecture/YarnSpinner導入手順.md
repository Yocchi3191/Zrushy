# Yarn Spinner 導入手順

Issue #14 (IEvent実装クラス) および Issue #15 (シナリオ分岐・入力待ち) に向けた、Yarn Spinner のプロジェクト統合ガイド。

## 参考リンク

- [公式ドキュメント](https://docs.yarnspinner.dev)
- [インストール](https://docs.yarnspinner.dev/yarn-spinner-for-unity/installation-and-setup)
- [カスタムコマンド・関数](https://docs.yarnspinner.dev/yarn-spinner-for-unity/creating-commands-functions)
- [Yarn.Dialogue API](https://docs.yarnspinner.dev/api/csharp/yarn/yarn.dialogue)
- [GitHub](https://github.com/YarnSpinnerTool/YarnSpinner-Unity)

---

## 1. Unity に Yarn Spinner をインストールする

Unity 6 (6000.2.6f2) は Yarn Spinner 3.1 の対応バージョン (Unity 2022.3+) を満たしている。

### Git URL でインストール (無料・OSS)

1. Unity エディタで **Window → Package Manager** を開く
2. 左上の **+** ボタン → **Add package from git URL**
3. 以下の URL を入力:
   ```
   https://github.com/YarnSpinnerTool/YarnSpinner-Unity.git#current
   ```
4. インストール完了後、Project ペインの Packages に「Yarn Spinner」が表示されることを確認

### 確認

- **Window → Yarn Spinner → Commands** でコマンド一覧ウィンドウが開ければ OK

---

## 2. Yarn スクリプトの基本

### ファイル構造

```yarn
title: head_default
---
や、やめて…髪が… #anim:reaction_default #expr:expression_shy
もう…撫でないでよ… #anim:reaction_default #expr:expression_shy
…ちょっとだけなら、いいけど #anim:reaction_default #expr:expression_happy
===
```

- `title:` → ScenarioID に対応するノード名
- `---` と `===` の間がノード本文
- 各行がセリフ (= Line)。Yarn Spinner が1行ずつ配信する
- `#key:value` はハッシュタグ (メタデータ)。Action の AnimationName, ExpressionName をここに載せる

### なぜコマンド (`<<>>`) ではなくハッシュタグか

`<<expression happy>>` のようなコマンドは「セリフとは独立に即時実行」される。
今のアーキテクチャでは Action = (Dialogue + AnimationName + ExpressionName) を **1つのまとまり** として HeroinViewModel に渡す設計なので、セリフ行に付随するメタデータとして扱うほうが自然。

コマンドは将来「入力待ち」「SE再生」「画面効果」など、Action に含まれない副作用に使う。

```yarn
title: head_first_touch
---
<<wait 0.5>>
え…？ 初めて触られた… #anim:reaction_surprise #expr:expression_shock
…嫌じゃ、ないけど #anim:reaction_default #expr:expression_shy
-> もっと撫でる
    えへへ… #anim:reaction_happy #expr:expression_happy
-> やめる
    …そう。 #anim:reaction_default #expr:expression_neutral
===
```

`->` が選択肢 (Issue #15 の分岐)。Yarn Spinner の標準構文で対応可能。

---

## 3. アーキテクチャ統合

### 現状

```
IScenarioRepository (Domain)
  └→ ListScenarioRepository (Infrastructure) ← ハードコード
       ↓ Scenario (Action のリスト) を返す
ScenarioPlayer (Presentation)
  └→ currentIndex でイテレート
       └→ HeroinViewModel.Act(action)
```

### 目標

```
IScenarioEngine (Domain) ← 新規。1行ずつ返すステートフルなインターフェース
  └→ YarnScenarioEngine (Unity側) ← Yarn Spinner をラップ
       ↓ Action を1つずつ返す
ScenarioPlayer (Presentation)
  └→ IScenarioEngine 経由で進行
       └→ HeroinViewModel.Act(action)
```

### Step 3-1: IScenarioEngine を Domain に定義する

詳細設計書に記載済みのインターフェース。`IScenarioRepository` を置き換える。

```csharp
// core/Zrushy.Core.Domain/Scenarios/Engine/IScenarioEngine.cs
namespace Zrushy.Core.Domain.Scenarios.Engine
{
    public interface IScenarioEngine
    {
        bool IsScenarioFinished { get; }
        Action GetCurrentAction();
        void Next();
        void Start(ScenarioID scenarioID);
    }
}
```

ポイント:
- Domain 層は Yarn Spinner の型を一切参照しない
- ScenarioID.Value がそのまま Yarn のノード名になる

### Step 3-2: ScenarioPlayer を IScenarioEngine に切り替える

```csharp
// core/Zrushy.Core.Presentation/ScenarioPlayer.cs
public class ScenarioPlayer
{
    private readonly IScenarioEngine engine;
    private readonly HeroinViewModel heroin;
    private bool isPlaying = false;

    public ScenarioPlayer(IScenarioEngine engine, HeroinViewModel heroinViewModel)
    {
        this.engine = engine;
        this.heroin = heroinViewModel;
    }

    internal void Play(ScenarioID scenarioID)
    {
        if (isPlaying) return;
        isPlaying = true;

        engine.Start(scenarioID);
        heroin.Act(engine.GetCurrentAction());
    }

    public void Next()
    {
        if (!isPlaying) return;

        engine.Next();
        if (engine.IsScenarioFinished)
        {
            isPlaying = false;
            return;
        }

        heroin.Act(engine.GetCurrentAction());
    }
}
```

変更点:
- `IScenarioRepository` + `Scenario` + `currentIndex` → `IScenarioEngine` に委譲
- ScenarioPlayer 自体のインデックス管理が不要になる

### Step 3-3: ListScenarioEngine を core に残す (テスト・開発用)

Yarn Spinner が入る前のフォールバック / テスト用として、現在の `ListScenarioRepository` を `IScenarioEngine` 準拠に書き換える。

```csharp
// core/Zrushy.Core.Infrastructure/Engine/ListScenarioEngine.cs
public class ListScenarioEngine : IScenarioEngine
{
    private IReadOnlyList<Action> actions;
    private int currentIndex;

    public bool IsScenarioFinished => currentIndex >= actions.Count;

    public void Start(ScenarioID scenarioID)
    {
        // 既存の switch-case ロジックをここに移動
        actions = GetActions(scenarioID);
        currentIndex = 0;
    }

    public Action GetCurrentAction() => actions[currentIndex];

    public void Next() => currentIndex++;

    private IReadOnlyList<Action> GetActions(ScenarioID scenarioID) { /* 既存ロジック */ }
}
```

### Step 3-4: YarnScenarioEngine を Unity 側に実装する

```csharp
// unity/Assets/Scripts/Engine/YarnScenarioEngine.cs
using Yarn.Unity;
using Zrushy.Core.Domain.Scenarios.Engine;
using Zrushy.Core.Domain.Scenarios.Entity;
using Zrushy.Core.Domain.Scenarios.ValueObject;
using Action = Zrushy.Core.Domain.Scenarios.Entity.Action;

public class YarnScenarioEngine : IScenarioEngine
{
    private readonly DialogueRunner dialogueRunner;

    // Yarn Spinner からの Line を Action に変換してバッファする
    private Action currentAction;
    private bool isFinished;

    public bool IsScenarioFinished => isFinished;

    public void Start(ScenarioID scenarioID)
    {
        isFinished = false;
        // ScenarioID.Value = Yarn ノード名
        dialogueRunner.StartDialogue(scenarioID.Value);
    }

    public Action GetCurrentAction() => currentAction;

    public void Next()
    {
        // DialogueRunner に次の行を要求
        // → DialoguePresenter 経由で currentAction が更新される
    }

    /// <summary>
    /// Yarn の Line + ハッシュタグ → Action に変換
    /// DialoguePresenter から呼ばれる
    /// </summary>
    internal void SetLineAsAction(LocalizedLine line)
    {
        string dialogue = line.TextWithoutCharacterName.Text;
        string anim = GetMetadata(line, "anim", "reaction_default");
        string expr = GetMetadata(line, "expr", "expression_neutral");
        currentAction = new Action(dialogue, anim, expr);
    }

    private string GetMetadata(LocalizedLine line, string key, string fallback)
    {
        // line.Metadata からハッシュタグを検索
        foreach (var tag in line.Metadata)
        {
            if (tag.StartsWith(key + ":"))
                return tag.Substring(key.Length + 1);
        }
        return fallback;
    }
}
```

> 上記は設計の骨格。DialogueRunner との接続部分 (DialoguePresenter) の実装は Step 4 で詳述。

### Step 3-5: DI 設定を更新する

```csharp
// ZrushyInstaller.cs
// Before:
Container.Bind<IScenarioRepository>().To<ListScenarioRepository>().AsSingle();

// After:
Container.Bind<IScenarioEngine>().To<YarnScenarioEngine>().AsSingle();
// テスト時は ListScenarioEngine にバインドする
```

---

## 4. DialoguePresenter (Yarn → Action の橋渡し)

Yarn Spinner 3 では `DialoguePresenterBase` を継承して、Line / Command / Options の受け取り方をカスタマイズする。

```csharp
// unity/Assets/Scripts/Engine/ZrushyDialoguePresenter.cs
using System.Threading;
using Yarn.Unity;
using Action = Zrushy.Core.Domain.Scenarios.Entity.Action;

public class ZrushyDialoguePresenter : DialoguePresenterBase
{
    private YarnScenarioEngine engine;

    // ScenarioPlayer.Next() が呼ばれるまで待機するための仕組み
    private TaskCompletionSource<bool> waitForNext;

    public override async YarnTask OnDialogueLineReceived(LocalizedLine line, LineCancellationToken token)
    {
        // Line → Action 変換
        engine.SetLineAsAction(line);

        // ScenarioPlayer.Next() が呼ばれるまで待機
        waitForNext = new TaskCompletionSource<bool>();
        await waitForNext.Task;
    }

    /// <summary>
    /// ScenarioPlayer.Next() から呼ばれる。待機を解除して次の Line に進む
    /// </summary>
    public void Advance()
    {
        waitForNext?.TrySetResult(true);
    }

    public override YarnTask OnDialogueCompleteAsync()
    {
        engine.MarkFinished();
        return YarnTask.CompletedTask;
    }
}
```

これにより、セリフの進行制御が ScenarioPlayer → Advance() → Yarn Spinner という流れになる。

---

## 5. Yarn スクリプトの配置とプロジェクト設定

### ファイル配置

```
unity/Assets/
├── YarnScripts/
│   ├── Zrushy.yarnproject     ← Yarn Project アセット
│   ├── head_default.yarn
│   ├── torso_default.yarn
│   ├── arm_default.yarn
│   ├── hand_default.yarn
│   ├── waist_default.yarn
│   ├── leg_default.yarn
│   └── foot_default.yarn
├── Scripts/
│   ├── Engine/
│   │   ├── YarnScenarioEngine.cs
│   │   └── ZrushyDialoguePresenter.cs
```

### Yarn Project の作成

1. Assets フォルダで右クリック → **Create → Yarn Spinner → Yarn Project**
2. Inspector で Source Scripts に `YarnScripts/` フォルダを指定
3. DialogueRunner の Yarn Project フィールドにドラッグ

---

## 6. 既存データの移行

`ListScenarioRepository` のハードコードを `.yarn` ファイルに変換する。

### 変換例: head_default

Before (C#):
```csharp
case "head_default":
    return new Scenario(scenarioID, new List<Action>
    {
        new Action("や、やめて…髪が…", "reaction_default", "expression_shy"),
        new Action("もう…撫でないでよ…", "reaction_default", "expression_shy"),
        new Action("…ちょっとだけなら、いいけど", "reaction_default", "expression_happy"),
    });
```

After (head_default.yarn):
```yarn
title: head_default
---
や、やめて…髪が… #anim:reaction_default #expr:expression_shy
もう…撫でないでよ… #anim:reaction_default #expr:expression_shy
…ちょっとだけなら、いいけど #anim:reaction_default #expr:expression_happy
===
```

7つの部位 + default の計8ノードを移行する。

---

## 7. 作業順序

| # | 作業 | レイヤー | 依存 |
|---|------|----------|------|
| 1 | Yarn Spinner を Unity にインストール | Unity | なし |
| 2 | IScenarioEngine を Domain に定義 | Core | なし |
| 3 | ListScenarioEngine を作成 (テスト用) | Core | #2 |
| 4 | ScenarioPlayer を IScenarioEngine に切り替え | Core | #2 |
| 5 | ScenarioPlayer のテストを修正・追加 | Core | #3, #4 |
| 6 | YarnScenarioEngine + ZrushyDialoguePresenter 実装 | Unity | #1, #2 |
| 7 | .yarn ファイルに既存データを移行 | Unity | #1 |
| 8 | ZrushyInstaller の DI 設定を更新 | Unity | #6 |
| 9 | ListScenarioRepository を削除 | Core | #8 が動作確認済み後 |

### Core 側 (#2-#5) と Unity 側 (#1, #6-#8) は並行して進められる

Core 側は ListScenarioEngine をモックとして使えるので、Yarn Spinner のインストールを待たずにインターフェース切り替えとテストを完了できる。

---

## 8. 将来の拡張 (Issue #15 向け)

Yarn Spinner の標準機能で対応できるもの:

### 選択肢分岐

```yarn
title: waist_threshold
---
そこは…だめ… #anim:reaction_default #expr:expression_shy
-> 続ける
    もう…！ #anim:reaction_default #expr:expression_angry
    <<jump waist_continue>>
-> やめる
    …ありがと #anim:reaction_default #expr:expression_happy
===
```

### 変数・条件分岐

```yarn
title: head_conditional
---
<<if $head_touch_count >= 5>>
    もう慣れちゃった… #anim:reaction_default #expr:expression_happy
<<else>>
    や、やめて… #anim:reaction_default #expr:expression_shy
<<endif>>
===
```

Yarn の変数 (`$head_touch_count`) と IInteractionHistory の同期は、Yarn Spinner の `VariableStorage` をカスタム実装することで実現できる。

### 入力待ち (AwaitTouch)

カスタムコマンドとして実装:
```yarn
<<await_touch arm>>
腕に触ってくれた！ #anim:reaction_happy #expr:expression_happy
```

`<<await_touch>>` コマンドを `[YarnCommand]` で定義し、指定部位のタッチが来るまで YarnTask で待機する。
