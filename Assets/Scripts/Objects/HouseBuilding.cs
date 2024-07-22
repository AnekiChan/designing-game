using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class HouseBuilding : MonoBehaviour
{
	/*
	private List<GameObject> furniture = new List<GameObject>();
	private bool isHousesDestroyModeActive = false;
	[SerializeField] private List<SpriteRenderer> renderers;

	private void OnEnable()
	{
		GridBuildingSystem.onDestroyHouse += DestroyFurniture;
		UIManager.onHousesDestroyMode += SetOutline;
	}

	private void OnDisable()
	{
		GridBuildingSystem.onDestroyHouse -= DestroyFurniture;
		UIManager.onHousesDestroyMode -= SetOutline;
	}

	private void OnDestroy()
	{
		GridBuildingSystem.onDestroyHouse -= DestroyFurniture;
		UIManager.onHousesDestroyMode -= SetOutline;
	}

	private void OnTriggerEnter2D(Collider2D collision)
	{
		if (collision.tag == "Furniture" && !furniture.Contains(collision.gameObject))
		{
			furniture.Add(collision.gameObject);
		}
	}

	private void OnTriggerExit2D(Collider2D collision)
	{
		if (collision.tag == "Furniture")
			furniture.Remove(collision.gameObject);
	}

	private void DestroyFurniture()
	{
		int count = furniture.Count - 1;
		for (int i = count; i >= 0; i--)
		{
			if (i < furniture.Count && furniture[i])
				Destroy(furniture[i]);
		}
	}

	private void SetOutline(bool isActive)
	{
		if (isActive)
		{
			isHousesDestroyModeActive = true;
			Material outline = Resources.Load("Materials/Outline") as Material;
			foreach (var r in renderers)
				r.material = outline;

			CameraMovement.NotChangingHouses();
		}
		else
		{
			isHousesDestroyModeActive = false;
			Material nooutline = Resources.Load("Materials/HandDraw") as Material;
			foreach (var r in renderers)
				r.material = nooutline;

			CameraMovement.ChangingHouses();
		}
	}

	private void OnMouseEnter()
	{
		if (isHousesDestroyModeActive)
		{
			isHousesDestroyModeActive = true;
			Material outline = Resources.Load("Materials/ColorOutline") as Material;
			foreach (var r in renderers)
				r.material = outline;
		}
	}

	private void OnMouseExit()
	{
		if (isHousesDestroyModeActive)
		{
			isHousesDestroyModeActive = true;
			Material outline = Resources.Load("Materials/Outline") as Material;
			foreach (var r in renderers)
				r.material = outline;
		}
	}
	*/
}
