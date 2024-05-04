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

	public void ChangeButtonState()
    {
		isActive = !isActive;
		animator.SetBool("IsSelected", isActive);
    }
}
