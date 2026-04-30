# スタート画面 設計仕様

## 概要

ゲーム起動時に表示するスタート画面。タイトルテキスト・Startボタン・Exitボタンで構成し、StartでおさわりシーンへZenjectなしのシンプルなMonoBehaviourで遷移する。

## シーン構成

**TitleScene.unity**（新規作成）

```
Canvas
├── TitleText (TextMeshProUGUI)
├── StartButton (Button)
└── ExitButton (Button)
TitleScreenController (GameObject)
```

## TitleScreenController

MonoBehaviourクラス。責務は遷移処理のみ。

- `OnStartClicked()` → `SceneManager.LoadScene("PoC")`
- `OnExitClicked()` → `Application.Quit()`

ボタンの `OnClick()` から Inspector でメソッドを指定する（PartController と同じ接続パターン）。

## ファイル配置

```
Assets/Title/
└── TitleScene.unity

Assets/Scripts/Presentation/Title/
└── TitleScreenController.cs
```

## Build Settings

| Index | シーン     |
| ----- | ---------- |
| 0     | TitleScene |
| 1     | PoC        |

ゲーム起動時に TitleScene が最初に開く。

## シーン開けない

https://qiita.com/rapirapi/items/9422f3634e43ea6aa15f
ビルドプロファイルに遷移したいシーンを登録しないといけない
てか ## Build Settings に書いてあんじゃん
