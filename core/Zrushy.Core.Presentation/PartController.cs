using Zrushy.Core.Application.UseCase.InteractPart;

namespace Zrushy.Core.Presentation
{
	/// <summary>
	/// 部位のコントローラー
	/// ユーザー入力をUseCaseに変換し、結果をViewModelに反映する
	/// </summary>
	public class PartController
	{
		private readonly InteractPart interactPartUseCase;

		public PartController(InteractPart interactPartUseCase)
		{
			this.interactPartUseCase = interactPartUseCase;
		}

		/// <summary>
		/// さわり入力を受け取り、Domainにパラメータ変化を反映する
		/// </summary>
		/// <param name="input">ユーザー入力</param>
		public void SendInput(PartInput input)
		{
			InteractPartCommand command = new InteractPartCommand(input.PartID);
			interactPartUseCase.Execute(command);
		}
	}
}
