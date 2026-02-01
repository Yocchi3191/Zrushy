using Zenject;
using Zrushy.Core.Application.UseCase.InteractPart;
using Zrushy.Core.Domain.Entity;
using Zrushy.Core.Domain.Repository;
using Zrushy.Core.Infrastructure.Engine;
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
			Container.Bind<IReactionRepository>().To<ReactionRepository>().AsSingle();
			Container.Bind<IEventRepository>().To<EventRepository>().AsSingle();
			Container.Bind<IScenarioEngine>().To<ListScenarioEngine>().AsSingle();

			// Application層
			// UseCase（シングルトン）
			Container.Bind<InteractPart>().AsSingle();

			// Presentation層
			Container.Bind<ScenarioPlayer>().AsSingle();
			Container.Bind<PartController>().AsSingle();

			// ViewModel
			Container.Bind<HeroinViewModel>().AsSingle();
		}
	}
}
