// Copyright (c) yoshioyocchi314@gmail.com
// Licensed under the MIT License.

using Zrushy.Core.Domain.Interactions.Entity;
using Zrushy.Core.Domain.Interactions.Service;
using Zrushy.Core.Domain.Interactions.ValueObject;

namespace Zrushy.Core.Infrastructure.ParameterReader
{
    public class DevelopmentReader : DevelopmentReadable
    {
        private readonly Heroin _heroin;

        public DevelopmentReader(Heroin heroin)
        {
            _heroin = heroin;
        }

        public Development GetDevelopment(PartID partID) => _heroin.GetPart(partID).Development;
    }
}
