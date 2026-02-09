using Yarn.Unity;
using Zenject;
using Zrushy.Core.Application.UseCase.InteractPart;
using Zrushy.Core.Domain.Events.Repository;
using Zrushy.Core.Domain.Events.Service;
using Zrushy.Core.Domain.Events.Service.Parsers;
using Zrushy.Core.Domain.Interactions.Entity;
using Zrushy.Core.Domain.Interactions.Service;
using Zrushy.Core.Domain.Scenarios.Repository;
using Zrushy.Core.Infrastructure.EventBus;
using Zrushy.Core.Infrastructure.Repository;
using Zrushy.Core.Presentation;

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
			// Body（シングルトン）※ IEventBus に依存
			Container.Bind<Body>().AsSingle();

			// Repository（シングルトン）
			Container.Bind<IEventRepository>().To<EventRepository>().AsSingle();
			Container.Bind<IInteractionHistory>().To<InteractionHistory>().AsSingle();
			Container.Bind<IPartParameterReader>().To<BodyParameterReader>().AsSingle();

			// ConditionFactory（シングルトン）
			Container.Bind<IConditionParser>().To<TouchCountConditionParser>().AsSingle();
			Container.Bind<IConditionParser>().To<FirstTouchConditionParser>().AsSingle();
			Container.Bind<IConditionParser>().To<EventFiredConditionParser>().AsSingle();
			Container.Bind<IConditionParser>().To<PleasureConditionParser>().AsSingle();
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
			Container.Bind<IInteractPart>().To<InteractPart>().AsSingle();

			// Presentation層
			Container.Bind<ScenarioPlayer>().AsSingle();
			Container.Bind<PartController>().AsSingle();

			// ViewModel
			Container.Bind<HeroinViewModel>().AsSingle();
		}
	}
}
