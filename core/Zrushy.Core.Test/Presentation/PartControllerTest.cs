using NSubstitute;
using Zrushy.Core.Application.UseCase.InteractPart;
using Zrushy.Core.Domain.Interactions.ValueObject;
using Zrushy.Core.Presentation;

namespace Zrushy.Core.Test.Presentation
{
	/// <summary>
	/// PartController の単体テスト
	/// PartController は InteractPart を呼ぶだけの薄いレイヤーなので、契約テストのみ
	/// </summary>
	public class PartControllerTest
	{
		[Test]
		public void SendInput_頭をさわる入力を受け取ったら_InteractPartが正しいコマンドで呼ばれる()
		{
			// Arrange
			var interactPart = Substitute.For<IInteractPart>();
			var controller = new PartController(interactPart);
			var input = new PartInput(new PartID("head"));

			// Act
			controller.SendInput(input);

			// Assert
			interactPart.Received(1).Execute(Arg.Is<InteractPartCommand>(
				cmd => cmd.PartID.Value == "head"
			));
		}
	}
}
