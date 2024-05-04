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

    void Start()
    {
        StandartPanel.SetActive(true);
        EditPanel.SetActive(false);
    }

    public void ChangePanel()
    {
        if (isEditing)
        {
			isHousesDestroyModeActive = true;
			DestroyHousesMode();
			StandartPanel.SetActive(true);
			EditPanel.SetActive(false);
            isEditing = false;
		}
        else
        {
			StandartPanel.SetActive(false);
			EditPanel.SetActive(true);
			isEditing = true;
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
