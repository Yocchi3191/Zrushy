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

		[SetUp]
		public void Setup()
		{
			controller = new PartController();
			input = new PartInput();
		}

		[Test]
		public void Test1()
		{
			controller.SendInput(input);
		}
	}
}
