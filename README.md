# Zrushy

> **18歳以上限定** — 本プロジェクトは成人向けコンテンツを含みます。登場するキャラクターはすべて成人の架空の存在です。

2Dキャラクターへのタッチ操作を中心とした成人向けシミュレーションゲームです。クリック・ドラッグなどのマウス操作でキャラクターに触れると反応が返ってきたり、シナリオイベントが発火したりします。

プログラム・イラスト・シナリオすべて個人制作。DLsiteでの公開を目標としています。

---

## 開発の目標

このプロジェクトは2つの目的を兼ねています。

1. **ゲームを完成させてリリースする** — 十分なギミックとコンテンツを作ってDLsiteで販売する
2. **実践で学ぶ** — TDD・クリーンアーキテクチャなどのソフトウェア設計を実プロジェクトで習得する

---

## アーキテクチャ

.NETのコアライブラリとUnityフロントエンドによるクリーンアーキテクチャ構成です。

```
core/       # .NET — Domain / Application / Infrastructure / Presentation
unity/      # Unity — View、DI（Zenject）、Unityサイドのインフラ
knowledge/  # 設計ドキュメント、仕様、アーキテクチャメモ
```

**レイヤー構成:**

| レイヤー     | 名前空間                                     | 内容                                               |
| ------------ | -------------------------------------------- | -------------------------------------------------- |
| Domain       | `Zrushy.Core.Domain`                         | エンティティ、値オブジェクト、インターフェース定義 |
| Application  | `Zrushy.Core.Application`                    | ユースケース                                       |
| Presentation | `Zrushy.Core.Presentation`                   | コントローラー、ViewModel                          |
| Unity        | `Presentation.Unity`, `Infrastructure.Unity` | 画面、マスタ取得などのUnity依存が必要な機能        |

コアライブラリはUnity非依存で、DLLにコンパイルされて `unity/Assets/Plugins/Core/` に自動配置されます。

**主要フレームワーク:** Zenject（DI）、Yarn Spinner（ダイアログ・シナリオ）、NSubstitute（テストスタブ）

---

## 開発スタイル

- **テスト駆動開発（TDD）** — テストとともに実装する
- **クリーンアーキテクチャ** — 依存関係のルールを厳守（内側のレイヤーは外側に依存しない）
- **Github Flow + PR** — すべての変更はプルリクエスト経由でコードレビューを行う

---

## 設計上のこだわり

- Unity非依存のコアを .NET でビルドし、DLL経由でUnityへ注入することでテスタビリティを確保
- ICondition[] のAND評価でイベント発火条件を組み合わせ可能に設計([実装](https://github.com/Yocchi3191/Zrushy/blob/main/core/Zrushy.Core.Domain/Events/Entity/Conditions/EventFiredCondition.cs) ,[利用](https://github.com/Yocchi3191/Zrushy/blob/main/core/Zrushy.Core.Domain/Events/Entity/Event.cs))
- [ConditionFactory](https://github.com/Yocchi3191/Zrushy/blob/main/core/Zrushy.Core.Domain/Events/Service/ConditionFactory.cs) + [IConditionParser](https://github.com/Yocchi3191/Zrushy/blob/main/core/Zrushy.Core.Domain/Events/Service/IConditionParser.cs) によるOCPに準拠したイベント条件の拡張機構

---

## ビルド

[just](https://github.com/casey/just) が必要です。

```sh
just build
```

`dotnet build` + `dotnet test` を実行し、DLLをUnityプロジェクトへ自動コピーします。
