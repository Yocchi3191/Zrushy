// Copyright (c) yoshioyocchi314@gmail.com
// Licensed under the MIT License.

using UnityEngine;
using Yarn.Unity;
using Zenject;
using Zrushy.Core.Application;
using Zrushy.Core.Application.UseCase.ApplyBonus;
using Zrushy.Core.Application.UseCase.CanZrushy;
using Zrushy.Core.Application.UseCase.GetScenario;
using Zrushy.Core.Application.UseCase.InteractPart;
using Zrushy.Core.Domain.Events.Repository;
using Zrushy.Core.Domain.Events.Service;
using Zrushy.Core.Domain.Events.Service.Parsers;
using Zrushy.Core.Domain.Interactions.Entity;
using Zrushy.Core.Domain.Interactions.Service;
using Zrushy.Core.Domain.Interactions.ValueObject;
using Zrushy.Core.Domain.Scenarios.Service;
using Zrushy.Core.Domain.Sprite;
using Zrushy.Core.Infrastructure.ParameterReader;
using Zrushy.Core.Infrastructure.Repository;
using Zrushy.Core.Infrastructure.Unity;
using Zrushy.Core.Presentation;
using Zrushy.Core.Presentation.Unity;

namespace Zrushy.Core.DI
{
    /// <summary>
    /// Zrushyアプリケーションの依存性注入設定
    /// Zenject MonoInstallerを使用してDIコンテナにバインドする
    /// </summary>
    public class ZrushyInstaller : MonoInstaller
    {
        [SerializeField] private YarnProject _yarnProject;

        public override void InstallBindings()
        {
            InstallDomain();
            InstallInfrastructure();
            InstallApplication();
            InstallPresentation();
        }

        private void InstallPresentation()
        {
            Container.Bind<ClickableRegistry>().AsSingle();
            Container.Bind<ScenarioInputGate>().AsSingle();
            Container.Bind<PartController>().AsSingle();
            Container.Bind<VirtualCursor>().FromComponentInHierarchy().AsSingle();
            Container.Bind<ScenarioCommandHandler>().FromComponentInHierarchy().AsSingle();

            Container.Bind<HeroinViewModel>().AsSingle();
        }

        private void InstallInfrastructure()
        {
            Container.Bind<IFiredEventLog>().To<FiredEventLog>().AsSingle();
            Container.Bind<EventBus>().AsSingle();

            Container.Bind<DialogueRunner>().FromComponentInHierarchy().AsSingle();

            Container.Bind<IEventRepository>().To<YarnEventRepository>().AsSingle();
            Container.Bind<IClothingEventRepository>().To<YarnClothingEventRepository>().AsSingle();

            Container.Bind<IInteractionHistory>().To<InteractionHistory>().AsSingle();
            Container.Bind<IZrushyHistory>().To<ZrushyHistory>().AsSingle();
            Container.Bind<IArousalReader>().To<ArousalReader>().AsSingle();
            Container.Bind<IDevelopmentReader>().To<DevelopmentReader>().AsSingle();
            Container.Bind<IAffectionReader>().To<AffectionReader>().AsSingle();

            Container.Bind<YarnProject>().FromInstance(_yarnProject).AsSingle();

            Container.Bind<ISpriteLayerRepository>().To<SpriteLayerRepository>().AsSingle();
        }

        private void InstallApplication()
        {
            Container.Bind<InteractPart>().To<InteractPart>().AsSingle();
            Container.Bind<ApplyBonus>().AsSingle();
            Container.Bind<GetScenario>().AsSingle();
            Container.Bind<IScenarioProvider>().To<YarnScenarioProvider>().AsSingle();
            Container.Bind<ScenarioSelector>().AsSingle();
            Container.Bind<IClothingEventEvaluator>().To<ClothingEventEvaluator>().AsSingle();
            Container.Bind<IZrushyClothing>().To<ZrushyClothing>().AsSingle();
        }

        private void InstallDomain()
        {
            Container.Bind<IEventEvaluator>().To<EventEvaluator>().AsSingle();
            Container.Bind<Heroin>()
                .FromMethod(_ => new Heroin(new Arousal(0), new Affection(0)))
                .AsSingle();

            Container.Bind<IConditionParser>().To<TouchCountConditionParser>().AsSingle();
            Container.Bind<IConditionParser>().To<FirstTouchConditionParser>().AsSingle();
            Container.Bind<IConditionParser>().To<EventFiredConditionParser>().AsSingle();
            Container.Bind<IConditionParser>().To<ArousalConditionParser>().AsSingle();
            Container.Bind<IConditionParser>().To<DevelopmentConditionParser>().AsSingle();
            Container.Bind<IConditionParser>().To<AffectionConditionParser>().AsSingle();
            Container.Bind<IConditionParser>().To<HeroinStateConditionParser>().AsSingle();
            Container.Bind<IConditionFactory>().To<ConditionFactory>().AsSingle();
        }
    }
}
