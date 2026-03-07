## Claudeへの指示

- 依頼していない作業をClaudeに委譲するか確認しない
- コードにコメントを追加しない
- core のコード変更後は `just build` でビルドとテストを確認してから完了とすること

## 開発環境

- core（.NETフレームワーク依存）と unity（Unityライブラリ依存）の2プロジェクトに分かれている
- core のビルド: `just build`（`dotnet build` + `dotnet test` を一括実行）
  - コマンド定義: `./justfile`
- ビルド時に dll が `./unity/Assets/Plugins/Core` へ自動配置される（`./core/Directory.Build.props`）、手作業コピー不要

### テスト

- mockライブラリ NSubstitute
- Substitute.Forでスタブを作成

スタブは契約テストか、ダミーデータを渡す目的に使うようにすること

### Yarnファイルについて

### ソース管理

githubを使っています

ghコマンドが使えるため、prやissueの確認を依頼した際は使ってください
使用できるラベルは `./.github/

## このプロジェクトについて

- 画面上の女の子の身体に触れてえっちなことをするシミュレーション、いわるゆ「おさわりゲーム」を開発しています
- プログラム、絵、イベントのシナリオなどすべて自分で組みます

### ゴール

- ある程度のギミックを作りきって、DLsiteで販売したいです
- 個人開発なので、コーディング、設計、仕様、TDDなど、開発に関わるいろいろな知識・技術を習得するための練習場としても使いたいです

## ファイル配置

プロジェクトルートから見て示します

```sh
./justfile # just コマンド定義

# .NET
./core/ # zrushyの.NETフレームワーク依存部分
./core/Zrushy.Core.Domain/ # Domain層実装
./core/Zrushy.Core.Application/ # Application層実装
./core/Zrushy.Core.Infrastructure/ # Infrastructure層実装
./core/Zrushy.Core.Presentation/ # Presentation層実装
./core/Zrushy.Core.Test/ # NUnitテスト
./core/Zrushy.Core.Test/Domain/ # Domain テスト
./core/Zrushy.Core.Test/Application/ # Application テスト
./core/Zrushy.Core.Test/Infrastructure/ # Infrastructure テスト
./core/Zrushy.Core.Test/Presentation/ # Presentation テスト

# unity
./unity/ # zrushyのunityフレームワーク依存部分
./unity/Assets/Plugins/Core/ # Coreのdllを置く場所
./unity/Assets/Scripts/Infrastructure # リポジトリ、エンジンの実装
./unity/Assets/Scripts/Presentation # UI、おさわり部位などの実装
./unity/Assets/Scripts/Config # おさわり部位ごとの開発度上昇パラメータ設定
./unity/Assets/Scripts/DI # Zenject Installerの実装

# ドキュメント置き場
./knowledge/ # 設計、仕様などのメモや実装に必要な自然言語の情報をまとめる
./knowledge/plan/ # 仕様、バグ修正、issue対応などをするときに「こんなことしたい」を書き殴る場所
./knowledge/spec/ # ある程度確定した仕様を置く場所
./knowledge/tasklog/ # 作業メモを置く場所……書き殴る用のplan/を用意したので、いらなくなるかも
./knowledge/architecture/ # 機能実装のときの設計を置く場所。mermaidとか
```

## アーキテクチャ

クリーンアーキテクチャを採用しています。ファイル配置との対応:

- Zrushy.Core.Domain: Entity, VO, Infra層のインターフェース
- Zrushy.Core.Application: UseCase, Infra層のインターフェース
- Zrushy.Core.Presentation: Controller, ViewModel, View, UIの実装
- Zrushy.Core.Infrastructure: Engine, Repository実装クラスを置くところだが、ほとんどunity側に集中している

## プロジェクト特有の注意点

### Condition 追加は3ステップ必須

詳細: `knowledge/architecture/Condition追加手順.md`

1. `ICondition` 実装クラスを `Domain/Events/Entity/Conditions/` に作る
2. `IConditionParser` 実装クラスを `Domain/Events/Service/Parsers/` に作る
   - `Type` プロパティがシナリオ文字列のキーになる（完全一致、ミスマッチは無音で失敗）
3. `ZrushyInstaller.cs` に `Container.Bind<IConditionParser>().To<YourParser>().AsSingle()` を `ConditionFactory` の Bind より前に追加する

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
