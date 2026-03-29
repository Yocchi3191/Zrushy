using System;
using System.Collections.Generic;
using Yarn.Unity;
using Zrushy.Core.Domain.Events.Entity;
using Zrushy.Core.Domain.Events.Repository;
using Zrushy.Core.Domain.Events.Service;
using Zrushy.Core.Domain.Events.ValueObject;
using Zrushy.Core.Domain.Interactions.ValueObject;
using Zrushy.Core.Domain.Scenarios.ValueObject;

namespace Zrushy.Core.Infrastructure.Unity
{
	public class YarnEventRepository : IEventRepository
	{
		private readonly Dictionary<PartID, List<IScenarioEvent>> cache = new();

		public YarnEventRepository(DialogueRunner dialogueRunner, IConditionFactory conditionFactory)
		{
			foreach (var pair in dialogueRunner.YarnProject.Program.Nodes)
			{
				var nodeName = pair.Key;
				var node = pair.Value;

				string partId = null;
				string conditionString = null;
				int priority = 0;

				foreach (var header in node.Headers)
				{
					switch (header.Key)
					{
						case "tags":
							foreach (var tag in header.Value.Split(' '))
							{
								if (tag.StartsWith("part:"))
								{
									partId = tag.Substring("part:".Length);
									break;
								}
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

				if (partId == null) continue;

				var partID = new PartID(partId);

				ICondition[] conditions = Array.Empty<ICondition>();
				if (conditionString != null)
				{
					var conditionEvent = conditionFactory.Create(conditionString);
					if (conditionEvent != null)
						conditions = new ICondition[] { new ConditionAdapter(conditionEvent) };
				}

				var scenarioEvent = new Event(
					new EventID(nodeName),
					new ScenarioID(nodeName),
					priority,
					conditions);

				if (!cache.ContainsKey(partID))
					cache[partID] = new List<IScenarioEvent>();
				cache[partID].Add(scenarioEvent);
			}
		}

		private static readonly PartID GlobalPartID = new PartID("public");

		public IReadOnlyList<IScenarioEvent> GetEvents(PartID partID)
		{
			return cache.TryGetValue(partID, out var events)
				? events.AsReadOnly()
				: Array.Empty<IScenarioEvent>();
		}

		public IReadOnlyList<IScenarioEvent> GetGlobalEvents()
		{
			return cache.TryGetValue(GlobalPartID, out var events)
				? events.AsReadOnly()
				: Array.Empty<IScenarioEvent>();
		}

		private class ConditionAdapter : ICondition
		{
			private readonly IEvent inner;

			public ConditionAdapter(IEvent inner) { this.inner = inner; }

			public bool CanFire() => inner.CanFire();
		}
	}
}
