using Zrushy.Core.Application.UseCase.InteractPart;
using Zrushy.Core.Domain.Entity;
using Zrushy.Core.Domain.Repository;
using Zrushy.Core.Domain.ValueObject;
using Zrushy.Core.Infrastructure.Repository;
using Zrushy.Core.Presentation;

namespace Zrushy.Core.Test.Presentation
{
	/// <summary>
	/// Domain ~ Presentationまでのオブジェクト連携を確認する結合テスト
	/// </summary>
	public class PartControllerTest
	{
		PartController controller;
		PartInput input;
		PartViewModel viewModel;

		[SetUp]
		public void Setup()
		{
			// 依存オブジェクトの構築
			Body body = new Body();
			body.AddPart(new Part(new PartID("head"), new Pleasure(0), new Development(0), new Affection(0)));
			IReactionRepository reactionRepository = new ReactionRepository();
			IEventRepository eventRepository = new EventRepository();
			InteractPart useCase = new InteractPart(body, reactionRepository, eventRepository);

			// ViewModelとControllerの構築
			viewModel = new PartViewModel();
			controller = new PartController(useCase);

			// テスト用の入力
			PartID partID = new PartID("head");
			input = new PartInput(partID);
		}

		[Test]
		public void SendInputで対象パーツとViewModelが更新される()
		{
			// イベントハンドラの登録
			bool updated = false;
			viewModel.OnUpdated += (result) => { updated = true; };

			// 入力送信（ViewModelを渡す）
			controller.SendInput(input, viewModel);

			// ViewModelが更新されることを確認
			Assert.That(updated, Is.True);
		}
	}
}
