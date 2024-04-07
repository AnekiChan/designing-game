using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Creature : MonoBehaviour
{
	private StateMachine _stateMachine;
	public Animator Animator;
	public bool IsStateEnd = false;

	public float speed = 2.5f;

	void Start()
	{
		Animator = GetComponent<Animator>();
		_stateMachine = new StateMachine();
		_stateMachine.Initialize(new IdleState(this));
	}

	void Update()
	{
		_stateMachine.CurrentState.Update();

		if (IsStateEnd)
		{
			IsStateEnd = false;
			ChooseState();
		}
	}

	private void ChooseState()
	{
		switch (Random.Range(1, 4))
		{
			case 1:
				{
					_stateMachine.ChangeState(new IdleState(this));
				}
				break;

			case 2:
				{
					_stateMachine.ChangeState(new WalkState(this));
				}
				break;

			case 3:
				{
					_stateMachine.ChangeState(new SleepState(this));
				}
				break;

			case 4:
				{
					_stateMachine.ChangeState(new Idle2State(this));
				}
				break;
		}
	}

	private void OnTriggerEnter2D(Collider2D collision)
	{
		//Debug.Log(collision.gameObject.tag);
		if (collision.gameObject.tag == "LeftWall" || collision.gameObject.tag == "RightWall")
			IsStateEnd = true;
	}
}
