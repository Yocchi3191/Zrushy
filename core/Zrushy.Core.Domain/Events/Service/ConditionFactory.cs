// Copyright (c) yoshioyocchi314@gmail.com
// Licensed under the MIT License.

using System.Collections.Generic;
using Zrushy.Core.Domain.Events.Entity;
using Zrushy.Core.Domain.Events.ValueObject;
using Zrushy.Core.Domain.Scenarios.ValueObject;

namespace Zrushy.Core.Domain.Events.Service
{
    /// <summary>
    /// 条件文字列からIEventを生成するファクトリ
    /// 書式: "type:param1:param2,type2:param1:param2"
    /// 単条件はIConditionを返す。複数条件はEventに包んで返す。
    /// 新しい条件タイプを追加する場合はIConditionParserを実装して登録する
    /// </summary>
    public class ConditionFactory : IConditionFactory
    {
        private readonly Dictionary<string, IConditionParser> _parsers;

        public ConditionFactory(List<IConditionParser> parsers)
        {
            _parsers = new Dictionary<string, IConditionParser>();
            foreach (IConditionParser parser in parsers)
            {
                _parsers[parser.Type] = parser;
            }
        }

        public IEvent? Create(string conditionString)
        {
            if (string.IsNullOrWhiteSpace(conditionString))
            {
                return null;
            }

            string[] parts = conditionString.Split(',');
            if (parts.Length == 1)
            {
                return CreateSingle(parts[0].Trim());
            }

            ICondition[] conditions = new ICondition[parts.Length];
            for (int i = 0; i < parts.Length; i++)
            {
                ICondition? condition = CreateSingle(parts[i].Trim());
                if (condition == null)
                {
                    return null;
                }

                conditions[i] = condition;
            }

            return new Event(new EventID("__composite__"), new ScenarioID(""), 0, conditions);
        }

        private ICondition? CreateSingle(string token)
        {
            string[] parts = token.Split(':');
            if (parts.Length == 0)
            {
                return null;
            }

            return _parsers.TryGetValue(parts[0], out IConditionParser? parser) ? parser.Parse(parts) : null;
        }
    }
}
