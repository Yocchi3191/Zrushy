// Copyright (c) yoshioyocchi314@gmail.com
// Licensed under the MIT License.

using Zenject;
using Zrushy.Core.Application.UseCase.FindSprite;
using Zrushy.Core.Domain.Sprite;
using Zrushy.Core.Infrastructure.Unity;
using Zrushy.Core.Presentation;

public class ChangeIllustInstaller : MonoInstaller
{
    public override void InstallBindings()
    {
        Container.Bind<ISpriteLayerRepository>().To<SpriteLayerRepository>().AsSingle();
        Container.Bind<FindSprite>().AsSingle();
        Container.Bind<SpriteLayerController>().AsSingle();
    }
}