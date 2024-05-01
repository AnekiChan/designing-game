using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    [SerializeField] private GameObject StandartPanel;
    [SerializeField] private GameObject EditPanel;
    private bool isEditing = false;

    void Start()
    {
        StandartPanel.SetActive(true);
        EditPanel.SetActive(false);
    }

    public void ChangePanel()
    {
        if (isEditing)
        {
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
}
