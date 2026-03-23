using Zenject;
using Zrushy.Core.Application.UseCase.ChangeSprite;
using Zrushy.Core.Domain.Sprite;
using Zrushy.Core.Infrastructure.Unity.SpriteLayer;
using Zrushy.Core.Presentation;

public class ChangeIllustInstaller : MonoInstaller
{
	public override void InstallBindings()
	{
		Container.Bind<ISpriteLayerRepository>().To<SpriteLayerRepository>().AsSingle();
		Container.Bind<ChangeSprite>().AsSingle();
		Container.Bind<SpriteLayerController>().AsSingle();
	}
}