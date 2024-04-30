using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Inventory : MonoBehaviour
{
    [SerializeField] private GridSystem gridSystem;

    [SerializeField] private InventoryCell _inventoryCellTemplate;
    [SerializeField] private Transform _container;
    [SerializeField] private Transform _draggingParent;

    [SerializeField] private List<Sprite> _images;
    [SerializeField] private GameObject _CurrentExteriorPanel;
    [SerializeField] private GameObject _CurrentInteriorPanel;

    [SerializeField] private GameObject _exteriorPanel;
	[SerializeField] private GameObject _interiorPanel;

	public GridBuildingSystem currentGrid;

    [SerializeField] private Button _exteriorButton;

	private void Awake()
	{
        
	}

	public void OnEnable()
    {
        SetExteriorPanel();
        Render(Connection.GetPlants());
        _exteriorButton.Select();
        _exteriorButton.interactable = true;
	}

    public void Render(List<Furniture> furnitures)
    {
        foreach (Transform child in _container)
        {
            Destroy(child.gameObject);
        }
		foreach (Furniture f in furnitures) Debug.Log(f.Title);
		foreach (Furniture furniture in furnitures)
        {
			var cell = Instantiate(_inventoryCellTemplate, _container);
			cell.Render(furniture, gridSystem.GetObjectGrid(furniture));
		}
    }

    public void RenderPlants()
    {
        Render(Connection.GetPlants());
        _CurrentExteriorPanel.GetComponent<Image>().sprite = _images[0];
    }

    public void RenderOutdoors()
    {
        Render(Connection.GetOutdors());
		_CurrentExteriorPanel.GetComponent<Image>().sprite = _images[1];
	}

    public void RenderBigFurniture()
    {
        Render(Connection.GetBigFurniture());
		_CurrentInteriorPanel.GetComponent<Image>().sprite = _images[0];
	}

	public void RenderSmallFurniture()
	{
		Render(Connection.GetSmallFurniture());
		_CurrentInteriorPanel.GetComponent<Image>().sprite = _images[1];
	}

	public void RenderDecore()
	{
		Render(Connection.GetDecore());
		_CurrentInteriorPanel.GetComponent<Image>().sprite = _images[2];
	}

	public void RenderWall()
	{
		Render(Connection.GetWall());
		_CurrentInteriorPanel.GetComponent<Image>().sprite = _images[3];
	}

	public void RenderCarpet()
	{
		Render(Connection.GetCarpet());
		_CurrentExteriorPanel.GetComponent<Image>().sprite = _images[4];
	}

    public void SetExteriorPanel()
    {
        _exteriorPanel.SetActive(true);
        _interiorPanel.SetActive(false);

        RenderPlants();
    }

	public void SetInteriorPanel()
	{
		_exteriorPanel.SetActive(false);
		_interiorPanel.SetActive(true);

        RenderBigFurniture();
	}

    public void HideInvantory(bool b)
    {
		CanvasGroup canvasGroup = GetComponent<CanvasGroup>();
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

public enum GridType
{
    Environment,
    Interior,
    Decore
}
