using Yarn.Unity;
using Zenject;
using Zrushy.Core.Application;
using Zrushy.Core.Application.Scenarios;
using Zrushy.Core.Application.UseCase.ApplyBonus;
using Zrushy.Core.Application.UseCase.InteractPart;
using Zrushy.Core.Domain.Events.Repository;
using Zrushy.Core.Domain.Events.Service;
using Zrushy.Core.Domain.Events.Service.Parsers;
using Zrushy.Core.Domain.Interactions.Entity;
using Zrushy.Core.Domain.Interactions.Service;
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
			InstallDomain();
			InstallInfrastructure();
			InstallApplication();
			InstallPresentation();
		}

		private void InstallPresentation()
		{
			Container.Bind<ClickableRegistry>().AsSingle();
			Container.Bind<ScenarioInputGate>().AsSingle();
			Container.Bind<ScenarioPlayer>().AsSingle();
			Container.Bind<PartController>().AsSingle();
			Container.Bind<VirtualCursor>().FromComponentInHierarchy().AsSingle();
			Container.Bind<ScenarioCommandHandler>().FromComponentInHierarchy().AsSingle();

			// ViewModel
			Container.Bind<HeroinViewModel>().AsSingle();
		}

		private void InstallInfrastructure()
		{
			Container.Bind<IFiredEventLog>().To<FiredEventLog>().AsSingle();
			Container.Bind<IEventBus>().To<EventBus>().AsSingle();

			// Yarn Spinner（シーンから取得）
			Container.Bind<DialogueRunner>().FromComponentInHierarchy().AsSingle();
			Container.Bind<ZrushyDialoguePresenter>().FromComponentInHierarchy().AsSingle();

			// ScenarioEngine（YarnScenarioEngine を使用）
			Container.Bind<IScenarioEngine>().To<YarnScenarioEngine>().AsSingle();
			Container.Bind<IEventRepository>().To<YarnEventRepository>().AsSingle();

			Container.Bind<IInteractionHistory>().To<InteractionHistory>().AsSingle();
			Container.Bind<IPartParameterReader>().To<BodyParameterReader>().AsSingle();
		}

		private void InstallApplication()
		{
			// Application層
			// UseCase（シングルトン）
			Container.Bind<InteractPart>().To<InteractPart>().AsSingle();
			Container.Bind<ApplyBonus>().AsSingle();
		}

		private void InstallDomain()
		{
			Container.Bind<IEventEvaluator>().To<EventEvaluator>().AsSingle();
			Container.Bind<Heroin>().AsSingle();

			// ConditionFactory（シングルトン）
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
