using Zrushy.Core.Domain.ValueObject;

namespace Zrushy.Core.Domain.Entity
{
	internal class Part
	{
		public PartID ID { get; }

		public Pleasure Pleasure { get; private set; } // 快感
		public Development Development { get; private set; } // 発達度
		public Affetction Affection { get; private set; } // 好感度

		internal void Interact()
		{
			Pleasure = Pleasure.CalcRateGain();
			Development = Development.CalcRateGain();
			Affection = Affection.CalcRateGain();
		}
	}
}
