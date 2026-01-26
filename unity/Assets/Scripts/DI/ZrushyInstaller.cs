using Zenject;
using Zrushy.Core.Application.UseCase.InteractPart;
using Zrushy.Core.Domain.Entity;
using Zrushy.Core.Domain.Repository;
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

			// Application層
			// UseCase（シングルトン）
			Container.Bind<InteractPart>().AsSingle();

			// Presentation層
			// Controller（シングルトン）
			Container.Bind<PartController>().AsSingle();

			// ViewModel は各PartのGameObjectContextで管理するため、ここでは登録しない
		}
	}
}
