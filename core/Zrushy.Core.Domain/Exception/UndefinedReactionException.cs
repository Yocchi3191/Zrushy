using System;
using Zrushy.Core.Domain.ValueObject;

namespace Zrushy.Core.Domain.Exception
{
	/// <summary>
	/// パーツに対応するリアクションが未定義の場合の例外
	/// </summary>
	public class UndefinedReactionException : System.Exception
	{
		public PartID PartID { get; }

		public UndefinedReactionException(PartID partID)
			: base($"リアクションが未定義です: {partID}")
		{
			PartID = partID;
		}
	}
}
