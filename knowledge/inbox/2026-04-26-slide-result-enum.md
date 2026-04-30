# SlideResult enum 導入 Implementation Plan

> **For agentic workers:** REQUIRED SUB-SKILL: Use superpowers:subagent-driven-development (recommended) or superpowers:executing-plans to implement this plan task-by-task. Steps use checkbox (`- [ ]`) syntax for tracking.

**Goal:** `bool isSuccess` を `SlideResult` enum に置き換え、Yarn タグも `zrushy:success/failure` → `result:success/failure` に統一する。

**Architecture:** `SlideResult` enum を Domain 層の `Interactions.ValueObject` に追加し、全シグネチャを置き換える。Yarn タグのパース文字列も合わせて変更する。

**Tech Stack:** C# (.NET), NUnit, NSubstitute, Yarn Spinner

---

## 変更ファイル一覧

| 種別 | ファイル |
|------|---------|
| 新規作成 | `core/Zrushy.Core.Domain/Interactions/ValueObject/SlideResult.cs` |
| 修正 | `core/Zrushy.Core.Domain/Events/Repository/IClothingEventRepository.cs` |
| 修正 | `core/Zrushy.Core.Domain/Events/Service/IClothingEventEvaluator.cs` |
| 修正 | `core/Zrushy.Core.Domain/Events/Service/IZrushyHistory.cs` |
| 修正 | `core/Zrushy.Core.Infrastructure/Repository/ZrushyHistory.cs` |
| 修正 | `core/Zrushy.Core.Application/ClothingEventEvaluator.cs` |
| 修正 | `core/Zrushy.Core.Application/UseCase/CanZrushy/ZrushyClothing.cs` |
| 修正(テスト) | `core/Zrushy.Core.Test/Application/ClothingEventEvaluatorTest.cs` |
| 修正(テスト) | `core/Zrushy.Core.Test/Application/UseCase/ZrushyClothingTest.cs` |
| 修正 | `unity/Assets/Scripts/Infrastructure/Repository/YarnClothingEventRepository.cs` |
| 修正 | `unity/Assets/YarnScripts/hoodie/hoodie_failed.yarn` |

---

### Task 1: SlideResult enum を作成し、テストを更新してビルドが壊れることを確認

**Files:**
- Create: `core/Zrushy.Core.Domain/Interactions/ValueObject/SlideResult.cs`
- Modify: `core/Zrushy.Core.Test/Application/ClothingEventEvaluatorTest.cs`
- Modify: `core/Zrushy.Core.Test/Application/UseCase/ZrushyClothingTest.cs`

- [ ] **Step 1: SlideResult enum を作成**

`core/Zrushy.Core.Domain/Interactions/ValueObject/SlideResult.cs` を新規作成:

```csharp
// Copyright (c) yoshioyocchi314@gmail.com
// Licensed under the MIT License.

namespace Zrushy.Core.Domain.Interactions.ValueObject
{
    public enum SlideResult
    {
        Success,
        Failure
    }
}
```

- [ ] **Step 2: ClothingEventEvaluatorTest を SlideResult に更新**

`core/Zrushy.Core.Test/Application/ClothingEventEvaluatorTest.cs` の変更箇所:

```csharp
// using に追加
using Zrushy.Core.Domain.Interactions.ValueObject;
```

各テストメソッドの `isSuccess: true` → `SlideResult.Success` に変更:

```csharp
// 38行目
evaluator.Evaluate(_clothingID, SlideResult.Success);

// 51行目
evaluator.Evaluate(_clothingID, SlideResult.Success);

// 64行目
evaluator.Evaluate(_clothingID, SlideResult.Success);

// 79行目
evaluator.Evaluate(_clothingID, SlideResult.Success);

// 88行目
evaluator.Evaluate(_clothingID, SlideResult.Success);
```

`ずらし履歴が記録される` テストの Received 検証も変更:
```csharp
// 90行目
_history.Received(1).Record(_clothingID, SlideResult.Success);
```

StubRepository の GetEvents シグネチャを変更:
```csharp
// 107行目
public IReadOnlyList<IScenarioEvent> GetEvents(ClothingID clothingID, SlideResult result) => _events;
```

- [ ] **Step 3: ZrushyClothingTest を SlideResult に更新**

`core/Zrushy.Core.Test/Application/UseCase/ZrushyClothingTest.cs` の変更箇所:

```csharp
// using に追加
using Zrushy.Core.Domain.Interactions.ValueObject;
```

```csharp
// 50行目 (Executeが呼ばれたらEvaluateを呼ぶ)
evaluator.Received(1).Evaluate(input.Target, SlideResult.Success);

// 63行目 (失敗時もEvaluateを呼ぶ)
evaluator.Received(1).Evaluate(input.Target, SlideResult.Failure);
```

- [ ] **Step 4: ビルドが失敗することを確認**

```bash
cd D:/Projects/Game/Zrushy && just build
```

期待: コンパイルエラー（インターフェースのシグネチャが `bool` のままなので不一致）

---

### Task 2: Domain インターフェースと Application/Infrastructure 実装を更新

**Files:**
- Modify: `core/Zrushy.Core.Domain/Events/Repository/IClothingEventRepository.cs`
- Modify: `core/Zrushy.Core.Domain/Events/Service/IClothingEventEvaluator.cs`
- Modify: `core/Zrushy.Core.Domain/Events/Service/IZrushyHistory.cs`
- Modify: `core/Zrushy.Core.Infrastructure/Repository/ZrushyHistory.cs`
- Modify: `core/Zrushy.Core.Application/ClothingEventEvaluator.cs`
- Modify: `core/Zrushy.Core.Application/UseCase/CanZrushy/ZrushyClothing.cs`

- [ ] **Step 1: IClothingEventRepository を更新**

```csharp
using System.Collections.Generic;
using Zrushy.Core.Domain.Events.Entity;
using Zrushy.Core.Domain.Interactions.ValueObject;

namespace Zrushy.Core.Domain.Events.Repository
{
    public interface IClothingEventRepository
    {
        IReadOnlyList<IScenarioEvent> GetEvents(ClothingID clothingID, SlideResult result);
    }
}
```

- [ ] **Step 2: IClothingEventEvaluator を更新**

```csharp
using Zrushy.Core.Domain.Interactions.ValueObject;

namespace Zrushy.Core.Domain.Events.Service
{
    public interface IClothingEventEvaluator
    {
        void Evaluate(ClothingID clothingID, SlideResult result);
    }
}
```

- [ ] **Step 3: IZrushyHistory を更新**

```csharp
using Zrushy.Core.Domain.Interactions.ValueObject;

namespace Zrushy.Core.Domain.Events.Service
{
    public interface IZrushyHistory
    {
        void Record(ClothingID clothingID, SlideResult result);
    }
}
```

- [ ] **Step 4: ZrushyHistory を更新**

```csharp
using System.Collections.Generic;
using Zrushy.Core.Domain.Events.Service;
using Zrushy.Core.Domain.Interactions.ValueObject;

namespace Zrushy.Core.Infrastructure.Repository
{
    public class ZrushyHistory : IZrushyHistory
    {
        private readonly List<(ClothingID clothingID, SlideResult result)> _records = new();

        public void Record(ClothingID clothingID, SlideResult result)
        {
            _records.Add((clothingID, result));
        }
    }
}
```

- [ ] **Step 5: ClothingEventEvaluator を更新**

```csharp
using System.Linq;
using Zrushy.Core.Domain.Events.Entity;
using Zrushy.Core.Domain.Events.Repository;
using Zrushy.Core.Domain.Events.Service;
using Zrushy.Core.Domain.Interactions.ValueObject;

namespace Zrushy.Core.Application
{
    public class ClothingEventEvaluator : IClothingEventEvaluator
    {
        private readonly IZrushyHistory _history;
        private readonly IClothingEventRepository _eventRepository;
        private readonly EventBus _eventBus;

        public ClothingEventEvaluator(
            IZrushyHistory history,
            IClothingEventRepository eventRepository,
            EventBus eventBus)
        {
            _history = history;
            _eventRepository = eventRepository;
            _eventBus = eventBus;
        }

        public void Evaluate(ClothingID clothingID, SlideResult result)
        {
            _history.Record(clothingID, result);

            IScenarioEvent fired = _eventRepository.GetEvents(clothingID, result)
                .Where(e => e.CanFire())
                .OrderByDescending(e => e.Priority)
                .FirstOrDefault();

            if (fired != null)
                _eventBus.Publish(fired);
        }
    }
}
```

- [ ] **Step 6: ZrushyClothing を更新**

`bool isSuccess` → `SlideResult` に変換するロジックを追加:

```csharp
using System;
using Zrushy.Core.Domain.Events.Service;
using Zrushy.Core.Domain.Interactions.Entity;
using Zrushy.Core.Domain.Interactions.ValueObject;

namespace Zrushy.Core.Application.UseCase.CanZrushy
{
    public class ZrushyClothing : IZrushyClothing
    {
        private readonly Heroin _heroin;
        private readonly IClothingEventEvaluator _evaluator;

        public ZrushyClothing(Heroin heroin, IClothingEventEvaluator evaluator)
        {
            _heroin = heroin ?? throw new ArgumentNullException(nameof(heroin));
            _evaluator = evaluator ?? throw new ArgumentNullException(nameof(evaluator));
        }

        bool IZrushyClothing.Execute(ZrushyInput input)
        {
            bool canPutOff = _heroin.CanPutOffClothing(input.Target);
            SlideResult result = canPutOff ? SlideResult.Success : SlideResult.Failure;
            _evaluator.Evaluate(input.Target, result);
            return canPutOff;
        }
    }
}
```

- [ ] **Step 7: ビルドとテストが通ることを確認**

```bash
cd D:/Projects/Game/Zrushy && just build
```

期待: SUCCESS (core のテストがすべて PASS)

- [ ] **Step 8: コミット**

```bash
git add core/
git commit -m "refactor: bool isSuccess を SlideResult enum に置き換え"
```

---

### Task 3: Unity 側 (Repository と Yarn ファイル) を更新

**Files:**
- Modify: `unity/Assets/Scripts/Infrastructure/Repository/YarnClothingEventRepository.cs`
- Modify: `unity/Assets/YarnScripts/hoodie/hoodie_failed.yarn`

- [ ] **Step 1: YarnClothingEventRepository のタグパースを更新**

`unity/Assets/Scripts/Infrastructure/Repository/YarnClothingEventRepository.cs` の変更:

```csharp
// using に追加
using Zrushy.Core.Domain.Interactions.ValueObject;
```

Dictionary のキー型を変更:
```csharp
private readonly Dictionary<(ClothingID, SlideResult), List<IScenarioEvent>> _cache = new();
```

タグパースのロジックを変更 (`zrushy:success`/`zrushy:failure` → `result:success`/`result:failure`):
```csharp
string clothingId = null;
SlideResult? slideResult = null;
// ...
foreach (string tag in header.Value.Split(' '))
{
    if (tag.StartsWith("clothing:"))
        clothingId = tag.Substring("clothing:".Length);
    else if (tag == "result:success")
        slideResult = SlideResult.Success;
    else if (tag == "result:failure")
        slideResult = SlideResult.Failure;
}
```

null チェックと key 生成を変更:
```csharp
if (clothingId == null || slideResult == null) continue;

ClothingID clothingID = new ClothingID(clothingId);
// ...
var key = (clothingID, slideResult.Value);
```

`GetEvents` シグネチャを変更:
```csharp
public IReadOnlyList<IScenarioEvent> GetEvents(ClothingID clothingID, SlideResult result)
{
    var key = (clothingID, result);
    return _cache.TryGetValue(key, out var events)
        ? events.AsReadOnly()
        : Array.Empty<IScenarioEvent>();
}
```

ファイル全体として:
```csharp
// Copyright (c) yoshioyocchi314@gmail.com
// Licensed under the MIT License.

using System;
using System.Collections.Generic;
using Yarn;
using Yarn.Unity;
using Zrushy.Core.Domain.Events.Entity;
using Zrushy.Core.Domain.Events.Repository;
using Zrushy.Core.Domain.Events.Service;
using Zrushy.Core.Domain.Events.ValueObject;
using Zrushy.Core.Domain.Interactions.ValueObject;
using Zrushy.Core.Domain.Scenarios.ValueObject;

namespace Zrushy.Core.Infrastructure.Unity
{
    public class YarnClothingEventRepository : IClothingEventRepository
    {
        private readonly Dictionary<(ClothingID, SlideResult), List<IScenarioEvent>> _cache = new();

        public YarnClothingEventRepository(DialogueRunner dialogueRunner, IConditionFactory conditionFactory)
        {
            foreach (var pair in dialogueRunner.YarnProject.Program.Nodes)
            {
                string nodeName = pair.Key;
                Node node = pair.Value;

                string clothingId = null;
                SlideResult? slideResult = null;
                string conditionString = null;
                int priority = 0;

                foreach (Header header in node.Headers)
                {
                    switch (header.Key)
                    {
                        case "tags":
                            foreach (string tag in header.Value.Split(' '))
                            {
                                if (tag.StartsWith("clothing:"))
                                    clothingId = tag.Substring("clothing:".Length);
                                else if (tag == "result:success")
                                    slideResult = SlideResult.Success;
                                else if (tag == "result:failure")
                                    slideResult = SlideResult.Failure;
                            }
                            break;
                        case "condition":
                            conditionString = header.Value.Trim();
                            break;
                        case "priority":
                            int.TryParse(header.Value.Trim(), out priority);
                            break;
                    }
                }

                if (clothingId == null || slideResult == null) continue;

                ClothingID clothingID = new ClothingID(clothingId);

                ICondition[] conditions = Array.Empty<ICondition>();
                if (conditionString != null)
                {
                    IEvent conditionEvent = conditionFactory.Create(conditionString);
                    if (conditionEvent != null)
                        conditions = new ICondition[] { new ConditionAdapter(conditionEvent) };
                }

                Event scenarioEvent = new Event(
                    new EventID(nodeName),
                    new ScenarioID(nodeName),
                    priority,
                    conditions);

                var key = (clothingID, slideResult.Value);
                if (!_cache.ContainsKey(key))
                    _cache[key] = new List<IScenarioEvent>();
                _cache[key].Add(scenarioEvent);
            }
        }

        public IReadOnlyList<IScenarioEvent> GetEvents(ClothingID clothingID, SlideResult result)
        {
            var key = (clothingID, result);
            return _cache.TryGetValue(key, out var events)
                ? events.AsReadOnly()
                : Array.Empty<IScenarioEvent>();
        }

        private class ConditionAdapter : ICondition
        {
            private readonly IEvent _inner;
            public ConditionAdapter(IEvent inner) { _inner = inner; }
            public bool CanFire() => _inner.CanFire();
        }
    }
}
```

- [ ] **Step 2: hoodie_failed.yarn のタグを更新**

```
title: hoodie_failed
tags: clothing:hoodie result:failure
condition: 
priority: 1
---
だ、だめっ！！
===
```

- [ ] **Step 3: コミット**

```bash
git add unity/Assets/Scripts/Infrastructure/Repository/YarnClothingEventRepository.cs
git add unity/Assets/YarnScripts/
git commit -m "refactor: Yarn タグを zrushy:success/failure から result:success/failure に変更"
```
