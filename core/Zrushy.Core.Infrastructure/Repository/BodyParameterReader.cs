// Copyright (c) yoshioyocchi314@gmail.com
// Licensed under the MIT License.

using Zrushy.Core.Domain.Interactions.Entity;
using Zrushy.Core.Domain.Interactions.Service;
using Zrushy.Core.Domain.Interactions.ValueObject;

namespace Zrushy.Core.Infrastructure.Repository
{
    public class BodyParameterReader : IPartParameterReader
    {
        private readonly Heroin _body;

        public BodyParameterReader(Heroin body)
        {
            _body = body;
        }

        /// <summary>
        /// 快感を取得する
        /// </summary>
        public Arousal GetArousal(PartID partID) => _body.Arousal;

        public Development GetDevelopment(PartID partID) => _body.GetPart(partID).Development;
        public Affection GetAffection(PartID partID) => _body.Affection;
    }
}
