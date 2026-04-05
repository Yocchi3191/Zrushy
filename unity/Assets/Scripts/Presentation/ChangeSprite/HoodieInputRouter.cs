using UnityEngine;

namespace Zrushy.Core.Presentation.Unity
{
	public class HoodieInputRouter : MonoBehaviour, ISpriteStateRouter
	{
		public ISpriteStateNode Controller => throw new System.NotImplementedException();

		public ISpriteStateNode[] Dependents => throw new System.NotImplementedException();

		public void Handle(PartInput input)
		{
			throw new System.NotImplementedException();
		}

		// Start is called once before the first execution of Update after the MonoBehaviour is created
		void Start()
		{

		}

		// Update is called once per frame
		void Update()
		{

		}
	}
}
