using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SubsystemContainer : MonoBehaviour
{
    [SerializeField]
    private SubsystemUI _uiTemplate = null;

    [SerializeField]
    private RectTransform _anchor = null;

    private List<SubsystemUI> _items = new List<SubsystemUI>();

    public void Initialize(List<Subsystem> subsystems)
    {
        GameManager.Instance.Car.OnSubsystemAdded += OnSubsystemAdded;
        GameManager.Instance.Car.OnSubsystemRemoved += OnSubsystemRemoved;

        foreach (Subsystem subsystem in subsystems)
        {
            OnSubsystemAdded(subsystem);
        }
    }

    private void OnDestroy()
    {
        GameManager.Instance.Car.OnSubsystemAdded -= OnSubsystemAdded;
        GameManager.Instance.Car.OnSubsystemRemoved -= OnSubsystemRemoved;
    }

    private void OnSubsystemAdded(Subsystem subsystem)
    {
        SubsystemUI item = GameObject.Instantiate(_uiTemplate, _anchor);
        _items.Add(item);

        item.Initialize(subsystem);
    }

    private void OnSubsystemRemoved(Subsystem subsystem)
    {
        SubsystemUI uiItem = _items.Find(item => item.Subsystem == subsystem);
        if (uiItem != null)
        {
            _items.Remove(uiItem);
            GameObject.Destroy(uiItem.gameObject);
        }
    }

	public void Display(bool show)
	{
		_anchor.gameObject.SetActive(show);
	}

}
