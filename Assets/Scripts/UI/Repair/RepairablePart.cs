using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;

public abstract class RepairablePart : MonoBehaviour
{
	[SerializeField]
	protected SubsystemData.SubsystemType _type = SubsystemData.SubsystemType.Count;

	[SerializeField]
	private SpriteRenderer _highlightSprite = null;

	[SerializeField]
	private float _animationDuration = 1.5f;

	protected RepairUI _repairUI = null;
	protected bool _ongoingRepairMode = false;
	protected bool _allowedTarget = false;

	private Vector3 _localHighlightScale = Vector3.zero;
	private Coroutine _animationCoroutine = null;

	private void Start()
	{
		_repairUI = (GameManager.Instance.CurrentGameMode as DrivingMode).Car.RepairUI;
		(GameManager.Instance.CurrentGameMode as DrivingMode).Car.OnRepairMode += OnRepairMode;

		_localHighlightScale = _highlightSprite.transform.localScale;
		_highlightSprite.enabled = false;

		Initialize();
	}

	private void OnDestroy()
	{
		if (GameManager.HasInstance)
		{
			(GameManager.Instance.CurrentGameMode as DrivingMode).Car.OnRepairMode -= OnRepairMode;
		}
		Release();
	}

	private void OnRepairMode(bool ongoingRepairMode)
	{
		_ongoingRepairMode = ongoingRepairMode;
	}

	protected abstract void Initialize();
	protected abstract void Release();
	public abstract void DropItem(InventoryItem item);

	protected void SetAllowedTarget(bool allowed)
	{
		if (_allowedTarget != allowed)
		{
			_allowedTarget = allowed;

			DisplayRepairHighlight(_allowedTarget);
		}
	}

	private void DisplayRepairHighlight(bool display)
	{
		_highlightSprite.enabled = display;

		if (display)
		{
			_animationCoroutine = StartCoroutine(AnimateHighlight());
		}
		else if (_animationCoroutine != null)
		{
			StopCoroutine(_animationCoroutine);
		}
	}

	private IEnumerator AnimateHighlight()
	{
		_highlightSprite.transform.localScale = _localHighlightScale;
		float timer = 0f;
		Vector3 from =  Vector3.zero;
		Vector3 to = _localHighlightScale;
		bool downscale = false;

		while (true)
		{
			_highlightSprite.transform.localScale = Vector3.Lerp(from, to, timer / _animationDuration);

			timer += Time.deltaTime;

			if (timer > _animationDuration)
			{
				timer = 0;				
				downscale = !downscale;

				from = downscale ? _localHighlightScale : Vector3.zero;
				to = downscale ? Vector3.zero : _localHighlightScale;
			}
			yield return null;
		}
	}
}