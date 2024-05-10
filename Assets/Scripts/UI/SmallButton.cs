using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SmallButton : MonoBehaviour
{
    private bool isActive = false;
    private Animator animator;

	private void Start()
	{
		animator = GetComponent<Animator>();
	}
	/*
	private void OnEnable()
	{
		EventBus.Instance.EditMode += ChangeButtonState;
	}
	private void OnDisable()
	{
		EventBus.Instance.EditMode -= ChangeButtonState;
	}
	private void OnDestroy()
	{
		EventBus.Instance.EditMode -= ChangeButtonState;
	}*/

	public void ChangeButtonState()
    {
		isActive = !isActive;
		animator.SetBool("IsSelected", isActive);
    }
}
