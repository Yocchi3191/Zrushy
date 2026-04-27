// Copyright (c) yoshioyocchi314@gmail.com
// Licensed under the MIT License.

using System;
using Zrushy.Core.Domain.Events.Service;
using Zrushy.Core.Domain.Interactions.Entity;
using Zrushy.Core.Domain.Interactions.ValueObject;

namespace Zrushy.Core.Application.UseCase.CanZrushy
{
    public class ZrushyClothing : IZrushyClothing
    {
        private readonly Heroin _heroin;
        private readonly IClothingEventEvaluator _evaluator;

        public ZrushyClothing(Heroin heroin, IClothingEventEvaluator evaluator)
        {
            _heroin = heroin ?? throw new ArgumentNullException(nameof(heroin));
            _evaluator = evaluator ?? throw new ArgumentNullException(nameof(evaluator));
        }

        bool IZrushyClothing.Execute(ZrushyInput input)
        {
            bool canPutOff = _heroin.CanPutOffClothing(input.Target);
            SlideResult result = canPutOff ? SlideResult.Success : SlideResult.Failure;
            _evaluator.Evaluate(input.Target, result);
            return canPutOff;
        }
    }
}
