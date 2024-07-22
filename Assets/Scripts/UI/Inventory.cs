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
	}

	public void OnEnable()
    {
        //onEdited?.Invoke(true);
        //Render(Connection.GetHouses());
        TaskSystem.onStartedTask += FillFurnitureLists;

    }
	private void OnDisable()
	{
        //onEdited?.Invoke(false);
        TaskSystem.onStartedTask -= FillFurnitureLists;
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
			cell.Render(furniture, gridSystem);
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

	private void FillFurnitureLists(TaskSO currentTask)
	{
		foreach (FurnitureSO furniture in GetFurnitureToTask(currentTask))
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

        OpenPanels();
	}

	private void OpenPanels()
	{
		int currentIndex = 0;
		if (_floor.Count > 0)
		{
            _buttons[currentIndex].GetComponent<Image>().sprite = _floorIcon;
            //_buttons[currentIndex].SetActive(true);
            _buttons[currentIndex].GetComponent<Button>().onClick.AddListener(RenderFloor);
			currentIndex++;
		}
		if (_wall.Count > 0)
        {
            _buttons[currentIndex].GetComponent<Image>().sprite = _wallIcon;
            _buttons[currentIndex].GetComponent<Button>().onClick.AddListener(RenderWall);
            currentIndex++;
        }
        if (_bigFurniture.Count > 0)
        {
            _buttons[currentIndex].GetComponent<Image>().sprite = _bigFurnitureIcon;
            _buttons[currentIndex].GetComponent<Button>().onClick.AddListener(RenderBigFurniture);
            currentIndex++;
        }
        if (_smallFurniture.Count > 0)
        {
            _buttons[currentIndex].GetComponent<Image>().sprite = _smallFurnitureIcon;
            _buttons[currentIndex].GetComponent<Button>().onClick.AddListener(RenderSmallFurniture);
            currentIndex++;
        }
        if (_decore.Count > 0)
        {
            _buttons[currentIndex].GetComponent<Image>().sprite = _decoreIcon;
            _buttons[currentIndex].GetComponent<Button>().onClick.AddListener(RenderDecor);
            currentIndex++;
        }
        if (_plants.Count > 0)
        {
            _buttons[currentIndex].GetComponent<Image>().sprite = _plantsIcon;
            _buttons[currentIndex].GetComponent<Button>().onClick.AddListener(RenderPlants);
            currentIndex++;
        }
        if (_carpets.Count > 0)
        {
            _buttons[currentIndex].GetComponent<Image>().sprite = _carpetsIcon;
            _buttons[currentIndex].GetComponent<Button>().onClick.AddListener(RenderCarpets);
            currentIndex++;
        }
        if (_wallDecore.Count > 0)
        {
            _buttons[currentIndex].GetComponent<Image>().sprite = _wallDecoreIcon;
            _buttons[currentIndex].GetComponent<Button>().onClick.AddListener(RenderWallDecor);
            currentIndex++;
        }

        for (int i = 0; i < currentIndex; i++)
        {
            _panels[i].SetActive(true);
            _buttons[i].SetActive(true);
            _buttons[i].GetComponent<Image>().SetNativeSize();
        }

        _buttons[0].GetComponent<Button>().onClick.Invoke();
	}

	public void RenderFloor()
	{
		Render(_floor);
	}

    public void RenderWall()
    {
        Render(_wall);
    }

    public void RenderBigFurniture()
    {
        Render(_bigFurniture);
    }

    public void RenderSmallFurniture()
    {
        Render(_smallFurniture);
    }

    public void RenderDecor()
    {
        Render(_decore);
    }

    public void RenderPlants()
    {
        Render(_plants);
    }

    public void RenderCarpets()
    {
        Render(_carpets);
    }

    public void RenderWallDecor()
    {
        Render(_wallDecore);
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

    public List<FurnitureSO> GetFurnitureToTask(TaskSO currentTask)
    {
        if (currentTask != null)
        {
            if (currentTask.RoomType == RoomType.None && currentTask.Theme == Theme.None && currentTask.Colors == FurnitureColor.None)
                return FurnitureSystem.AllFurniture.FindAll(p => p.IsObtained);

            List<FurnitureSO> furnitureForTask = new List<FurnitureSO>();
            furnitureForTask.AddRange(FurnitureSystem.AllFurniture.FindAll(
                p => p.RoomTypes.Contains(currentTask.RoomType) && p.Theme.Contains(currentTask.Theme) && p.FurnitureColors.Contains(currentTask.Colors)));
            furnitureForTask.AddRange(FurnitureSystem.AllFurniture.FindAll(
                p => p.RoomTypes.Contains(currentTask.RoomType) && p.IsObtained));
            return furnitureForTask;
        }
        else throw new ArgumentNullException("No task");
    }

    public void ChangePanelSprite(int index)
    {
        _currentPanel.GetComponent<Image>().sprite = _panels[index].GetComponent<Image>().sprite;
    }
}
