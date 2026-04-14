// Copyright (c) yoshioyocchi314@gmail.com
// Licensed under the MIT License.

using Zrushy.Core.Domain.Interactions.Entity;
using Zrushy.Core.Domain.Interactions.Service;
using Zrushy.Core.Domain.Interactions.ValueObject;

namespace Zrushy.Core.Infrastructure.ParameterReader
{
    public class AffectionReader : AffectionReadable
    {
        private readonly Heroin _heroin;

        public AffectionReader(Heroin heroin)
        {
            _heroin = heroin;
        }

        public Affection GetAffection() => _heroin.Affection;
    }
}
