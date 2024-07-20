using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using FurnitureTypesList;

public class Inventory : MonoBehaviour
{
    [SerializeField] private GridSystem gridSystem;

    [SerializeField] private InventoryCell _inventoryCellTemplate;
    [SerializeField] private Transform _container;
    [SerializeField] private Transform _draggingParent;
    [SerializeField] private List<GameObject> _panels;
	[SerializeField] private List<GameObject> _buttons;
    public GridBuildingSystem currentGrid;

	[SerializeField] private GameObject _currentPanel;
	private List<FurnitureSO> _floor = new List<FurnitureSO>();
	[SerializeField] private Sprite _floorIcon;
	private List<FurnitureSO> _wall = new List<FurnitureSO>();
    [SerializeField] private Sprite _wallIcon;
    private List<FurnitureSO> _bigFurniture = new List<FurnitureSO>();
    [SerializeField] private Sprite _bigFurnitureIcon;
    private List<FurnitureSO> _smallFurniture = new List<FurnitureSO>();
    [SerializeField] private Sprite _smallFurnitureIcon;
    private List<FurnitureSO> _decore = new List<FurnitureSO>();
    [SerializeField] private Sprite _decoreIcon;
    private List<FurnitureSO> _plants = new List<FurnitureSO>();
    [SerializeField] private Sprite _plantsIcon;
    private List<FurnitureSO> _carpets = new List<FurnitureSO>();
    [SerializeField] private Sprite _carpetsIcon;
    private List<FurnitureSO> _wallDecore = new List<FurnitureSO>();
    [SerializeField] private Sprite _wallDecoreIcon;

    //public static Action<bool> onEdited;

    private void Awake()
	{
		ClearLists();
        foreach (GameObject panel in _panels) panel.SetActive(false);
        foreach (GameObject button in _buttons) button.GetComponent<Button>().onClick.RemoveAllListeners();
		FillFurnitureLists();
        OpenPanels();
	}

	public void OnEnable()
    {
		//onEdited?.Invoke(true);
        //Render(Connection.GetHouses());
	}
	private void OnDisable()
	{
		//onEdited?.Invoke(false);
	}

	public void Render(List<FurnitureSO> furnitures)
    {
        foreach (Transform child in _container)
        {
            Destroy(child.gameObject);
        }
		//foreach (Furniture f in furnitures) Debug.Log(f.Title);
		foreach (FurnitureSO furniture in furnitures)
        {
			var cell = Instantiate(_inventoryCellTemplate, _container);
			//cell.Render(furniture, gridSystem.GetObjectGrid(furniture));
		}
    }

	private void ClearLists()
	{
		_floor.Clear();
		_wall.Clear();
		_bigFurniture.Clear();
		_smallFurniture.Clear();
		_decore.Clear();
		_plants.Clear();
		_wallDecore.Clear();
		_carpets.Clear();
	}

	private void FillFurnitureLists()
	{
		foreach (FurnitureSO furniture in TaskSystem.GetFurnitureToTask())
		{
			switch (furniture.FurnitureType)
			{
				case FurnitureType.Floor:
					_floor.Add(furniture);
					break;
				case FurnitureType.Wall:
					_wall.Add(furniture);
					break;
				case FurnitureType.Big:
					_bigFurniture.Add(furniture);
					break;
				case FurnitureType.Small:
					_smallFurniture.Add(furniture);
					break;
				case FurnitureType.Decor:
					_decore.Add(furniture);
					break;
				case FurnitureType.Plant:
					_plants.Add(furniture);
					break;
				case FurnitureType.Carpet:
					_carpets.Add(furniture);
					break;
				case FurnitureType.WallDecor:
					_wallDecore.Add(furniture);
					break;
			}
		}
	}

	private void OpenPanels()
	{
		List<Sprite> icons = new List<Sprite>();
		int currentIndex = 0;
		if (_floor.Count > 0)
		{
			_panels[currentIndex].SetActive(true);
			_buttons[currentIndex].GetComponent<Button>().onClick.AddListener(RenderFloor);
			currentIndex++;
		}
		if (_wall.Count > 0)
        {
            _panels[currentIndex].SetActive(true);
            _buttons[currentIndex].GetComponent<Button>().onClick.AddListener(RenderWall);
            currentIndex++;
        }
        if (_bigFurniture.Count > 0)
        {
            _panels[currentIndex].SetActive(true);
            _buttons[currentIndex].GetComponent<Button>().onClick.AddListener(RenderBigFurniture);
            currentIndex++;
        }
        if (_smallFurniture.Count > 0)
        {
            _panels[currentIndex].SetActive(true);
            _buttons[currentIndex].GetComponent<Button>().onClick.AddListener(RenderSmallFurniture);
            currentIndex++;
        }
        if (_decore.Count > 0)
        {
            _panels[currentIndex].SetActive(true);
            _buttons[currentIndex].GetComponent<Button>().onClick.AddListener(RenderDecor);
            currentIndex++;
        }
        if (_plants.Count > 0)
        {
            _panels[currentIndex].SetActive(true);
            _buttons[currentIndex].GetComponent<Button>().onClick.AddListener(RenderPlants);
            currentIndex++;
        }
        if (_carpets.Count > 0)
        {
            _panels[currentIndex].SetActive(true);
            _buttons[currentIndex].GetComponent<Button>().onClick.AddListener(RenderCarpets);
            currentIndex++;
        }
        if (_wallDecore.Count > 0)
        {
            _panels[currentIndex].SetActive(true);
            _buttons[currentIndex].GetComponent<Button>().onClick.AddListener(RenderWallDecor);
            currentIndex++;
        }

	}

	public void RenderFloor()
	{
		Render(_floor);
		_currentPanel.GetComponent<Image>().sprite = gameObject.transform.parent.GetComponent<Image>().sprite;
	}

    public void RenderWall()
    {
        Render(_wall);
        _currentPanel.GetComponent<Image>().sprite = gameObject.transform.parent.GetComponent<Image>().sprite;
    }

    public void RenderBigFurniture()
    {
        Render(_bigFurniture);
        _currentPanel.GetComponent<Image>().sprite = gameObject.transform.parent.GetComponent<Image>().sprite;
    }

    public void RenderSmallFurniture()
    {
        Render(_smallFurniture);
        _currentPanel.GetComponent<Image>().sprite = gameObject.transform.parent.GetComponent<Image>().sprite;
    }

    public void RenderDecor()
    {
        Render(_decore);
        _currentPanel.GetComponent<Image>().sprite = gameObject.transform.parent.GetComponent<Image>().sprite;
    }

    public void RenderPlants()
    {
        Render(_plants);
        _currentPanel.GetComponent<Image>().sprite = gameObject.transform.parent.GetComponent<Image>().sprite;
    }

    public void RenderCarpets()
    {
        Render(_carpets);
        _currentPanel.GetComponent<Image>().sprite = gameObject.transform.parent.GetComponent<Image>().sprite;
    }

    public void RenderWallDecor()
    {
        Render(_wallDecore);
        _currentPanel.GetComponent<Image>().sprite = gameObject.transform.parent.GetComponent<Image>().sprite;
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
