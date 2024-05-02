using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HouseBuilding : MonoBehaviour
{
    [SerializeField] Collider2D[] colliders;

	private void OnEnable()
	{
        Building.onPlaced += DeactivateColliders;
	}
	private void OnDisable()
	{
		Building.onPlaced -= DeactivateColliders;
	}
	private void DeactivateColliders()
    {
        foreach (var collider in colliders)
        {
            //collider.enabled = false;
        }
    }
}
