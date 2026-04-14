// Copyright (c) yoshioyocchi314@gmail.com
// Licensed under the MIT License.

using System;
using Zrushy.Core.Domain.Interactions.Entity;

namespace Zrushy.Core.Application.UseCase.CanZrushy
{
    public class CanZrushy : IZrushyPermission
    {
        private Heroin _heroin;

        public CanZrushy(Heroin heroin)
        {
            _heroin = heroin ?? throw new ArgumentNullException(nameof(heroin));
        }

        bool IZrushyPermission.CanZrushy(ZrushyInput input)
        {
            throw new NotImplementedException();
        }

    }
}
