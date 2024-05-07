using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    [SerializeField] private GameObject StandartPanel;
    [SerializeField] private GameObject EditPanel;
	[SerializeField] private GameObject EditInventory;
	private bool isEditing = false;
    public static Action<bool> onHousesDestroyMode;
    public static bool isHousesDestroyModeActive = false;

	private void OnEnable()
	{
		EventBus.Instance.EditMode += ChangeEditMode;
	}
	private void OnDisable()
	{
		EventBus.Instance.EditMode -= ChangeEditMode;
	}

	void Start()
    {
		EventBus.Instance.EditMode?.Invoke(false);
	}

    public void ChangePanel()
    {
        if (isEditing)
        {
			EventBus.Instance.EditMode?.Invoke(false);
			EventBus.Instance.SaveAllObjects?.Invoke();
		}
        else
        {
			EventBus.Instance.EditMode?.Invoke(true);
		}
		isEditing = !isEditing;
    }

	private void ChangeEditMode(bool state)
	{
		EditPanel.SetActive(state);
		StandartPanel.SetActive(!state);
		if (state)
		{
			isHousesDestroyModeActive = true;
			DestroyHousesMode();
		}
	}

    public void DestroyHousesMode()
    {
        isHousesDestroyModeActive = !isHousesDestroyModeActive;
        onHousesDestroyMode?.Invoke(isHousesDestroyModeActive);
		HideInventory(isHousesDestroyModeActive);
    }

	public void HideInventory(bool b)
	{
		CanvasGroup canvasGroup = EditInventory.GetComponent<CanvasGroup>();
		if (b)
		{
			canvasGroup.alpha = 0f;
			canvasGroup.interactable = false;
		}
		else
		{
			canvasGroup.alpha = 1f;
			canvasGroup.interactable = true;
		}
	}
}
