using Yarn.Unity;
using Zenject;
using Zrushy.Core.Application.UseCase.InteractPart;
using Zrushy.Core.Domain.Events.Repository;
using Zrushy.Core.Domain.Interactions.Entity;
using Zrushy.Core.Domain.Scenarios.Repository;
using Zrushy.Core.Infrastructure.Engine;
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
			// Domain層
			// Body（シングルトン）
			Container.Bind<Body>().AsSingle();

			// Repository（シングルトン）
			Container.Bind<IEventRepository>().To<EventRepository>().AsSingle();

			// EventBus（シングルトン）
			Container.Bind<IEventBus>().To<EventBus>().AsSingle();

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
