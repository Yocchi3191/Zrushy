using System.Collections.Generic;
using Zrushy.Core.Domain.Exception;
using Zrushy.Core.Domain.ValueObject;

namespace Zrushy.Core.Domain.Entity
{
	/// <summary>
	/// 身体エンティティ（集約ルート）
	/// ヒロインの身体全体を管理し、各部位へのアクセスを制御する
	/// </summary>
	public class Body
	{
		private readonly List<Part> parts;

		/// <summary>
		/// 空の身体を作成する
		/// </summary>
		public Body()
		{
			parts = new List<Part>();
		}

		/// <summary>
		/// 部位を追加する
		/// </summary>
		/// <param name="part">追加する部位</param>
		public void AddPart(Part part)
		{
			parts.Add(part);
		}

		/// <summary>
		/// さわり操作を実行する
		/// 対象部位のパラメータを更新する
		/// </summary>
		/// <param name="interaction">さわり操作</param>
		public void Interact(Interaction interaction)
		{
			Part targetPart = GetPart(interaction.PartID);
			targetPart.Interact(interaction);
		}

		/// <summary>
		/// 指定した部位を取得する
		/// </summary>
		/// <param name="partID">部位ID</param>
		/// <returns>部位</returns>
		/// <exception cref="PartNotFoundException">部位が見つからない場合</exception>
		public Part GetPart(PartID partID)
		{
			return parts.Find(p => p.ID.Equals(partID))
				?? throw new PartNotFoundException(partID);
		}
	}
}
