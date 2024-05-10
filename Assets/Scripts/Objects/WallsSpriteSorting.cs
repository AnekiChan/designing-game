using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallsSpriteSorting : MonoBehaviour
{
	private void OnTriggerStay2D(Collider2D collision)
	{
		//Debug.Log("wall trigger");
		if (collision.gameObject.tag == "Creature")
			EventBus.Instance.ChangePlayerSortingLayer.Invoke(0);
	}
	private void OnTriggerExit2D(Collider2D collision)
	{
		if (collision.gameObject.tag == "Creature")
			EventBus.Instance.SetStandartLayer.Invoke();
	}
}
