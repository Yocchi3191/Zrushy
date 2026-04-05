namespace Zrushy.Core.Presentation.Unity
{
	/// <summary>
	/// SpriteStateの変更に応じて他のSpriteStateを調整するためのインターフェース
	/// </summary>
	internal interface ISpriteStateMediator
	{
		void OnStateChanged(ISpriteStateNode newState);
	}
}
