using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HouseWhileZoom : MonoBehaviour
{
    private Animator _animator;

	private void OnEnable()
	{
		CameraMovement.onZoomed += ShowOrHide;
	}

	private void OnDisable()
	{
		CameraMovement.onZoomed -= ShowOrHide;
	}

	private void Start()
	{
		_animator = GetComponent<Animator>();
	}

	private void ShowOrHide(bool Hide)
	{
        if (Hide)
        {
			_animator.SetBool("IsZoom", true);
        }
		else
		{
			_animator.SetBool("IsZoom", false);
		}
    }
}
