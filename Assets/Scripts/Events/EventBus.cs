using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class EventBus
{
	private EventBus() { }

	private static EventBus _instance;
	public static EventBus Instance
	{
		get
		{
			if (_instance == null)
				_instance = new EventBus();
			return _instance;
		}
	}

	#region UI
	public Action<bool> EditMode;
	#endregion

	#region Save
	public Action SaveAllObjects;
	#endregion

	#region Gameplay
	public Action<int> ChangeScore;
	public Action<int> CheackScore;
	#endregion
}
