// Copyright (c) yoshioyocchi314@gmail.com
// Licensed under the MIT License.

using NSubstitute;
using Zrushy.Core.Application.UseCase.InteractPart;
using Zrushy.Core.Domain.Events.Service;
using Zrushy.Core.Domain.Interactions.Entity;
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
        private Heroin _heroin;
        private IEventEvaluator _eventEvaluator;

        [SetUp]
        public void setup()
        {
            _eventEvaluator = Substitute.For<IEventEvaluator>();
            _heroin = new Heroin();

            IPart headPart = Substitute.For<IPart>();
            headPart.ID.Returns(new PartID("head"));
            headPart.CalculateArousal(Arg.Any<Arousal>(), Arg.Any<Interaction>()).Returns(new Arousal(0));
            _heroin.AddPart(headPart);
        }

        [Test]
        public void SendInput_頭をさわる入力を受け取ったら_InteractPartが正しいコマンドで呼ばれる()
        {
            InteractPart interactPart = new InteractPart(_heroin, _eventEvaluator);
            PartController controller = new PartController(interactPart, new ScenarioInputGate());
            PartInput input = new PartInput(new PartID("head"));

            controller.SendInput(input);

            _eventEvaluator.Received(1).Evaluate(Arg.Is<Interaction>(i => i.PartID.Value == "head"));
        }
    }
}
