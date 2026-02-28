using Yarn.Unity;
using Zenject;
using Zrushy.Core.Application.UseCase.ApplyBonus;
using Zrushy.Core.Application.UseCase.InteractPart;
using Zrushy.Core.Domain.Events.Repository;
using Zrushy.Core.Domain.Events.Service;
using Zrushy.Core.Domain.Events.Service.Parsers;
using Zrushy.Core.Domain.Interactions.Entity;
using Zrushy.Core.Domain.Interactions.Service;
using Zrushy.Core.Application.Scenarios;
using Zrushy.Core.Infrastructure.EventBus;
using Zrushy.Core.Infrastructure.Repository;
using Zrushy.Core.Presentation;
using Zrushy.Core.Presentation.Unity;
using Zrushy.Unity.Presentation;

namespace Zrushy.Core.DI
{
	/// <summary>
	/// Zrushyアプリケーションの依存性注入設定
	/// Zenject MonoInstallerを使用してDIコンテナにバインドする
	/// </summary>
	public class ZrushyInstaller : MonoInstaller
	{
		public override void InstallBindings()
		{
			// FiredEventLog（シングルトン）
			Container.Bind<IFiredEventLog>().To<FiredEventLog>().AsSingle();

			// EventBus（シングルトン）※ Body より先にバインド（依存順序）
			Container.Bind<IEventBus>().To<EventBus>().AsSingle();

			// Domain層
			Container.Bind<Heroin>().AsSingle();

			// Repository（シングルトン）
			Container.Bind<IEventRepository>().To<YarnEventRepository>().AsSingle();
			Container.Bind<IInteractionHistory>().To<InteractionHistory>().AsSingle();
			Container.Bind<IPartParameterReader>().To<BodyParameterReader>().AsSingle();

			// ConditionFactory（シングルトン）
			Container.Bind<IConditionParser>().To<TouchCountConditionParser>().AsSingle();
			Container.Bind<IConditionParser>().To<FirstTouchConditionParser>().AsSingle();
			Container.Bind<IConditionParser>().To<EventFiredConditionParser>().AsSingle();
			Container.Bind<IConditionParser>().To<ArousalConditionParser>().AsSingle();
			Container.Bind<IConditionParser>().To<DevelopmentConditionParser>().AsSingle();
			Container.Bind<IConditionParser>().To<AffectionConditionParser>().AsSingle();
			Container.Bind<IConditionFactory>().To<ConditionFactory>().AsSingle();

			// Yarn Spinner（シーンから取得）
			Container.Bind<DialogueRunner>().FromComponentInHierarchy().AsSingle();
			Container.Bind<ZrushyDialoguePresenter>().FromComponentInHierarchy().AsSingle();

			// ScenarioEngine（YarnScenarioEngine を使用）
			Container.Bind<IScenarioEngine>().To<YarnScenarioEngine>().AsSingle();

			// Application層
			// UseCase（シングルトン）
			Container.Bind<InteractPart>().To<InteractPart>().AsSingle();
			Container.Bind<ApplyBonus>().AsSingle();

			// Presentation層
			Container.Bind<ClickableRegistry>().AsSingle();
			Container.Bind<ScenarioInputGate>().AsSingle();
			Container.Bind<ScenarioPlayer>().AsSingle();
			Container.Bind<PartController>().AsSingle();
			Container.Bind<VirtualCursor>().FromComponentInHierarchy().AsSingle();
			Container.Bind<ScenarioCommandHandler>().FromComponentInHierarchy().AsSingle();

			// ViewModel
			Container.Bind<HeroinViewModel>().AsSingle();
		}
	}
}
