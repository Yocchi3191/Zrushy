using UnityEngine;
using Zrushy.Core.Presentation.Unity.Assets.Scripts.Presentation.ChangeSprite;
using Zrushy.Core.Presentation.Unity.ChangeSprite;

namespace Zrushy.Core.Presentation.Unity
{
	public class SpriteStateMediator : MonoBehaviour, ISpriteStateTransitional
	{
		[SerializeField] SpriteState controller; // このSpriteStateの状態変化に応じて、dependentsのSpriteStateを変化させる
		[SerializeField] SpriteState[] dependents; // controllerの状態に応じて、dependentsの遷移可能な状態が決まる
		[SerializeField] ConstraintEntry[] constraints; // controllerの状態ごとの、dependentsの遷移可能な状態

		void Start()
		{

		}

		void OnDestroy()
		{

		}

		private void OnZipperStateChanged(Sprite newState)
		{

		}
	}
}
