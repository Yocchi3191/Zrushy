using UnityEngine;
using Zenject;
using Zrushy.Core.Presentation;

public class HeroinView : MonoBehaviour
{
	[Inject]
	private HeroinViewModel viewModel;

	private void Start()
	{
		viewModel.OnUpdated += OnViewModelUpdated;
	}

	private void OnDestroy()
	{
		viewModel.OnUpdated -= OnViewModelUpdated;
	}

	private void OnViewModelUpdated(HeroinViewModel vm)
	{
		var action = vm.CurrentAction;
		if (action != null)
		{
			Debug.Log($"[Heroin] {action.Dialogue} (anim: {action.AnimationName}, expr: {action.ExpressionName})");
		}
	}
}
