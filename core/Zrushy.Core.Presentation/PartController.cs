using Zrushy.Core.Application.UseCase.InteractPart;
using Zrushy.Core.Domain.ValueObject;

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
		/// ユーザー入力を受け取り、さわり反応の処理を実行してViewModelを更新する
		/// ViewModelが更新されると、自動的にViewに通知される
		/// </summary>
		/// <param name="input">ユーザー入力</param>
		/// <param name="viewModel">更新対象のViewModel</param>
		public void SendInput(PartInput input, PartViewModel viewModel)
		{
			// 入力をコマンドに変換
			InteractPartCommand command = new InteractPartCommand(input.PartID);

			// UseCaseを実行
			InteractPartResult result = interactPartUseCase.Execute(command);

			// ViewModelを更新（自動的にイベントが発火してViewに通知される）
			viewModel.Update(result);
		}
	}
}
