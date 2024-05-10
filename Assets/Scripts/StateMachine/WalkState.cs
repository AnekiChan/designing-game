using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.EventSystems;

public class WalkState : State
{
    private Creature _creature;
    private Vector3 _newPosition;

    private NavMeshAgent _agent;
    private bool _isCorrectPoint = false;
    private NavMeshPath _navMeshPath;

    public WalkState(Creature creature)
    {
        _creature = creature;
        _agent = creature.GetComponent<NavMeshAgent>();
        _navMeshPath = new NavMeshPath();
        //_agent.isStopped = false;
	}

    public override void Enter()
    {
        base.Enter();
        //Debug.Log("walk enter");
        _creature.Animator.SetFloat("Speed", _agent.speed);

        while (!_isCorrectPoint )
		{
			/*
			_newPosition = new Vector2(_creature.transform.position.x + Random.Range(-4f, 4f), _creature.transform.position.y + Random.Range(-4f, 4f));
            _agent.CalculatePath(_newPosition, _navMeshPath);
            if (_navMeshPath.status == NavMeshPathStatus.PathComplete) 
                _isCorrectPoint = true;
            */

			_newPosition = new Vector2(_creature.transform.position.x + Random.Range(-4f, 4f), _creature.transform.position.y + Random.Range(-4f, 4f));
			Ray ray = new Ray(_newPosition, Vector3.forward);
			RaycastHit2D[] hits = Physics2D.RaycastAll(ray.origin, ray.direction, 2);
			_isCorrectPoint = false;
			foreach (var hit in hits)
			{
                _agent.CalculatePath(_newPosition, _navMeshPath);
                if (hit.collider != null && hit.collider.tag == "Furniture")
                    break;
				else if (hit.collider != null && hit.collider.tag == "Ground" && _navMeshPath.status == NavMeshPathStatus.PathComplete)
				{
					_isCorrectPoint = true;
                    break;
				}
			}
		}

        if (_newPosition.x < _creature.transform.position.x)
            _creature.GetComponent<SpriteRenderer>().flipX = false;
        else
            _creature.GetComponent<SpriteRenderer>().flipX = true;
    }

    public override void Exit()
    {
        base.Exit();
        //Debug.Log("walk exit");
        _creature.Animator.SetFloat("Speed", 0);
    }

    public override void Update()
    {
        base.Update();
        //_creature.transform.position = Vector2.MoveTowards(_creature.transform.position, _newPosition, _speed * Time.deltaTime);
        _agent.SetDestination(_newPosition);

        if (Vector2.Distance(_creature.transform.position, _newPosition) < 0.15f)
        {
			//_agent.isStopped = true;
			_creature.IsStateEnd = true;
        }
    }

}
