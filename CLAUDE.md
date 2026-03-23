## Claudeへの指示

- 実作業はこちらから依頼しない限り行わないし、確認もしない
- 基本はメンターとしてユーザーの相談に回答したり、ユーザーが気づいていなさそうな懸念を指摘するのがメイン
- コードにコメントを追加しない
- core のコード変更後は zrushy-build-verify skill でビルドとテストを確認してから完了とすること
- ユーザーはclaudeより知識が無いことの方が多いです。間違っていたり誤解した内容や知識を話している可能性があるので、うのみにしすぎないでください

## 開発環境

- core（.NET）と unity（Unity）の2プロジェクト構成
- ビルド: `just build`（`dotnet build` + `dotnet test`）→ dll が `unity/Assets/Plugins/Core/` へ自動配置
- テスト: NSubstitute でスタブ作成（契約テストかダミーデータ渡し目的のみ）

### ソース管理

- GitHub / gh コマンドで操作（github-operations skill を使うこと）
- 利用可能ラベル: `.github/labels.yml`
- ラベルをGitHubに反映: `tools/gh-sync-labels/sync-labels.sh`

## このプロジェクトについて

- 画面上の女の子の身体に触れてえっちなことをするシミュレーション、いわるゆ「おさわりゲーム」を開発しています
- プログラム、絵、イベントのシナリオなどすべて自分で組みます

### ゴール

- ある程度のギミックを作りきって、DLsiteで販売したいです
- 個人開発なので、コーディング、設計、仕様、TDDなど、開発に関わるいろいろな知識・技術を習得するための練習場としても使いたいです

## ファイル配置

```
core/                          # .NET（Domain/Application/Infrastructure/Presentation/Test）
unity/Assets/Plugins/Core/     # core の dll 出力先（自動配置）
unity/Assets/Scripts/DI/       # Zenject Installer
unity/Assets/Scripts/Presentation/  # UI、おさわり部位
knowledge/                     # 設計・仕様ドキュメント（plan/, spec/, architecture/）
```

## アーキテクチャ

クリーンアーキテクチャを採用しています。ファイル配置との対応:

- Zrushy.Core.Domain: Entity, VO, Infra層のインターフェース
- Zrushy.Core.Application: UseCase, Infra層のインターフェース
- Zrushy.Core.Presentation: Controller, ViewModel, View, UIの実装
- Zrushy.Core.Infrastructure: Engine, Repository実装クラスを置くところだが、ほとんどunity側に集中している

## プロジェクト特有の注意点

### Condition 追加

zrushy-add-condition skill を使うこと。

### `Domain.Exception` namespace の衝突

`Zrushy.Core.Domain.Exception` という namespace が存在するため、Domain 層で `Exception` を継承するクラスを書くと `System.Exception` が名前解決できなくなる。`System.Exception` と完全修飾が必要。

### Yarn Spinner `[YarnCommand]` はインスタンスメソッドに付けない

インスタンスメソッドに付けると、最初の引数を GameObject 名として検索する仕様になる。`DialogueRunner.AddCommandHandler` でランタイム登録するのが正解。

## 用語対応

### おさわり

詳細: `knowledge/architecture/おさわり/コールチェーン.md`

```
Clickable.OnPointerClick → PartController.SendInput → InteractPart.Execute
  → Heroin.Interact → Part.Interact / IEventEvaluator.Evaluate
```

### イベント発火

詳細: `knowledge/architecture/イベント発火/コールチェーン.md`

```
EventEvaluator.Evaluate → Event.CanFire() (ICondition[] AND評価)
  → EventBus.Publish → ScenarioDriver.OnEventFired → ScenarioPlayer.Play
  → YarnScenarioEngine.Start → DialogueRunner.StartDialogue
```

## レビュー

PRを上げたらレビューをclaudeに依頼しています
レビュー内容はPRにコメントとして投稿してください