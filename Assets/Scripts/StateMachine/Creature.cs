using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Creature : MonoBehaviour
{
	[SerializeField] private CreatureScriptableObject creatureScriptableObject;

	private StateMachine _stateMachine;
	public Animator Animator;
	public bool IsStateEnd = false;

	private NavMeshAgent _agent;

	private State _nextState = null;

	void Start()
	{
		Animator = GetComponent<Animator>();

		_stateMachine = new StateMachine();
		_stateMachine.Initialize(new IdleState(this));

		_agent = GetComponent<NavMeshAgent>();
		_agent.updateRotation = false;
		_agent.updateUpAxis = false;
		_agent.speed = creatureScriptableObject.Speed;
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
		if (_nextState ==  null)
		{
			switch (Random.Range(1, 5))
			{
				case 1:
					{
						_stateMachine.ChangeState(new IdleState(this));
						_nextState = null;
					}
					break;

				case 2:
					{
						_stateMachine.ChangeState(new WalkState(this));
						_nextState = new IdleState(this);
					}
					break;

				case 3:
					{
						_stateMachine.ChangeState(new SitState(this));
						_nextState = new WalkState(this);
					}
					break;

				case 4:
					{
						_stateMachine.ChangeState(new Idle2State(this));
						_nextState = null;
					}
					break;
			}
		}
		else
		{
			_stateMachine.ChangeState(_nextState);
			_nextState = null;
		}
	}
}
