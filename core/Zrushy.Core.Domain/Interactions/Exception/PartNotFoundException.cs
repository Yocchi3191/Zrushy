using System;
using Zrushy.Core.Domain.Interactions.ValueObject;

namespace Zrushy.Core.Domain.Interactions.Exception
{
	/// <summary>
	/// 存在しないパーツにアクセスしようとした場合の例外
	/// </summary>
	public class PartNotFoundException : System.Exception
	{
		public PartID PartID { get; }

		public PartNotFoundException(PartID partID)
			: base($"パーツが見つかりません: {partID}")
		{
			PartID = partID;
		}
	}
}
