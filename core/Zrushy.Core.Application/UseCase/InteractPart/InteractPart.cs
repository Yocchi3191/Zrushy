// Copyright (c) yoshioyocchi314@gmail.com
// Licensed under the MIT License.

using Zrushy.Core.Domain.Events.Service;
using Zrushy.Core.Domain.Interactions.Entity;
using Zrushy.Core.Domain.Interactions.ValueObject;

namespace Zrushy.Core.Application.UseCase.InteractPart
{
    /// <summary>
    /// 部位をさわる操作のユースケース
    /// </summary>
    public class InteractPart
    {
        private readonly Heroin _heroin;
        private readonly IEventEvaluator _eventEvaluator;

        public InteractPart(Heroin heroin, IEventEvaluator eventEvaluator)
        {
            _heroin = heroin;
            _eventEvaluator = eventEvaluator;
        }

        /// <summary>
        /// 部位をさわる操作を実行する
        /// </summary>
        /// <param name="command">操作コマンド</param>
        public void Execute(InteractPartCommand command)
        {
            Interaction interaction = new Interaction(command.PartID, command.Type);
            _heroin.Interact(interaction);
            _eventEvaluator.Evaluate(interaction);
            if (_heroin.IsClimax)
            {
                _heroin.ApplyCooldown();
            }
        }
    }
}
