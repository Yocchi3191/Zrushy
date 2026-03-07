# Condition 追加手順

新しい発火条件（`ICondition`）を追加するときに必要な手順。

## 手順

### 1. Condition クラスを作る

`core/Zrushy.Core.Domain/Events/Entity/Conditions/` に `ICondition` を実装するクラスを作る。

```csharp
public class YourCondition : ICondition
{
    public bool CanFire() => /* 判定ロジック */;
}
```

依存するサービス（`IInteractionHistory` など）はコンストラクタで受け取る。

### 2. Parser クラスを作る

`core/Zrushy.Core.Domain/Events/Service/Parsers/` に `IConditionParser` を実装するクラスを作る。

```csharp
public class YourConditionParser : IConditionParser
{
    public string Type => "your_type"; // シナリオファイルでのキー文字列

    public ICondition? Parse(string[] parts)
    {
        if (parts.Length != 期待する長さ) return null;
        // parts[1], parts[2]... からパラメータを取り出す
        return new YourCondition(...);
    }
}
```

**`Type` プロパティ**がシナリオファイルの条件文字列（`"your_type:param1:param2"` の先頭部分）と一致する必要がある。大文字小文字・ハイフン・アンダースコア含め完全一致。ミスマッチは無音で失敗する。

### 3. ZrushyInstaller.cs に登録する

`unity/Assets/Scripts/DI/ZrushyInstaller.cs` の `ConditionFactory` の Bind **より前**に追加する。

```csharp
Container.Bind<IConditionParser>().To<YourConditionParser>().AsSingle();
// ... 他の Parser ...
Container.Bind<IConditionFactory>().To<ConditionFactory>().AsSingle(); // ← これより前
```

Zenject が `List<IConditionParser>` を自動収集して `ConditionFactory` に渡す仕組みのため、順序が重要。

## シナリオファイルでの書き方

条件文字列のフォーマット: `"type:param1:param2,..."`

- コロン区切りでパラメータを渡す（`parts[0]` が `Type`、以降がパラメータ）
- カンマ区切りで複数条件の AND になる

```
touch_count:head:10,your_type:param1:param2
```

## 関連ファイル

- `core/Zrushy.Core.Domain/Events/Service/IConditionParser.cs`
- `core/Zrushy.Core.Domain/Events/Service/ConditionFactory.cs`
- `core/Zrushy.Core.Domain/Events/Service/Parsers/` （既存の実装例）
- `core/Zrushy.Core.Domain/Events/Entity/Conditions/` （既存の Condition 例）
- `unity/Assets/Scripts/DI/ZrushyInstaller.cs`
