using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpriteSorting : MonoBehaviour
{
	private SpriteRenderer _spriteRenderer;
	private int _deafultOrder;

	private void Start()
	{
		_spriteRenderer = GetComponent<SpriteRenderer>();
		_deafultOrder = _spriteRenderer.sortingOrder;
	}
	private void OnEnable()
	{
		EventBus.Instance.ChangePlayerSortingLayer += ChageLayer;
		EventBus.Instance.SetStandartLayer += ChangeToStandartLayer;
	}
	private void OnDisable()
	{
		EventBus.Instance.ChangePlayerSortingLayer -= ChageLayer;
		EventBus.Instance.SetStandartLayer -= ChangeToStandartLayer;
	}
	private void OnDestroy()
	{
		EventBus.Instance.ChangePlayerSortingLayer -= ChageLayer;
		EventBus.Instance.SetStandartLayer -= ChangeToStandartLayer;
	}

	private void ChageLayer(int _layer)
	{
		_spriteRenderer.sortingOrder = _layer;
	}

	private void ChangeToStandartLayer()
	{
		_spriteRenderer.sortingOrder = _deafultOrder;
	}


	/*
	private void OnTriggerExit2D(Collider2D collision)
	{
		RaycastHit2D[] hits = Physics2D.RaycastAll(transform.position, Vector3.forward, 2f);

		if (hits.Length == 0 || (hits.Length == 1 && hits[0].collider?.gameObject.tag == "Walls"))
		{
			ChageLayer(_deafultOrder);
		}
	}*/

	/*
	private void OnTriggerEnter2D(Collider2D collision)
	{
		switch (collision.tag)
		{
			case "Walls":
				{
					_spriteRenderer.sortingOrder = 0;
				}
				break;
			case "Furniture":
				{
					if (collision.gameObject.GetComponent<Building>().isSitting)
					{
						_spriteRenderer.sortingOrder += 1;
					}
				}
				break;
		}
	}

	private void OnTriggerStay2D(Collider2D collision)
	{
		switch (collision.tag)
		{
			case "Walls":
				{
					_spriteRenderer.sortingOrder = 0;
				}
				break;
			case "Furniture":
				{
					if (collision.gameObject.GetComponent<Building>().isSitting)
					{
						_spriteRenderer.sortingOrder += 1;
					}
				}
				break;
		}
	}

	private void OnTriggerExit2D(Collider2D collision)
	{
		if (collision.tag == "Walls" || collision.tag == "Furniture")
		{
			_spriteRenderer.sortingOrder = _deafultOrder;
		}
	}
	*/
}
