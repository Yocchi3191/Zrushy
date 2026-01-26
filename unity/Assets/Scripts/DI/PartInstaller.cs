using Zenject;
using Zrushy.Core.Presentation;

namespace Zrushy.Core.DI
{
	/// <summary>
	/// 各Part（部位）専用のInstaller
	/// GameObjectContextで使用し、Part単位でViewModelを管理する
	/// </summary>
	public class PartInstaller : MonoInstaller
	{
		public override void InstallBindings()
		{
			// このPart専用のViewModel（このGameObjectContext内でシングルトン）
			Container.Bind<PartViewModel>().AsSingle();
		}
	}
}
