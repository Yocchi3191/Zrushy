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

| レイヤー | 名前空間 | 内容 |
|---|---|---|
| Domain | `Zrushy.Core.Domain` | エンティティ、値オブジェクト、インターフェース定義 |
| Application | `Zrushy.Core.Application` | ユースケース |
| Presentation | `Zrushy.Core.Presentation` | コントローラー、ViewModel |
| Infrastructure | Unityサイド | リポジトリ実装、エンジン |

コアライブラリはUnity非依存で、DLLにコンパイルされて `unity/Assets/Plugins/Core/` に自動配置されます。

**主要フレームワーク:** Zenject（DI）、Yarn Spinner（ダイアログ・シナリオ）、NSubstitute（テストスタブ）

---

## 開発スタイル

- **テスト駆動開発（TDD）** — 実装前にテストを書く
- **クリーンアーキテクチャ** — 依存関係のルールを厳守（内側のレイヤーは外側に依存しない）
- **フィーチャーブランチ + PR** — すべての変更はプルリクエスト経由でコードレビューを行う

---

## ビルド

[just](https://github.com/casey/just) が必要です。

```sh
just build
```

`dotnet build` + `dotnet test` を実行し、DLLをUnityプロジェクトへ自動コピーします。
