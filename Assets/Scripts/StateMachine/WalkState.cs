using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class WalkState : State
{
    private Creature _creature;
    private float _speed = 0;
    private Vector3 _newPosition;

    private NavMeshAgent _agent;

    public WalkState(Creature creature)
    {
        _creature = creature;
        _agent = creature.GetComponent<NavMeshAgent>();
        _agent.isStopped = false;
		//_agent.updateRotation = false;
		//_agent.updateUpAxis = false;
	}

    public override void Enter()
    {
        base.Enter();
        Debug.Log("walk enter");
        _speed = _creature.speed;
        _creature.Animator.SetFloat("Speed", _speed);
        _newPosition = new Vector2(_creature.transform.position.x + Random.Range(-4f, 4f), _creature.transform.position.y + Random.Range(-4f, 4f));
        if (_newPosition.x < _creature.transform.position.x)
            _creature.GetComponent<SpriteRenderer>().flipX = false;
        else
            _creature.GetComponent<SpriteRenderer>().flipX = true;

		Debug.Log(_newPosition);
        
        Collider2D[] colliders = Physics2D.OverlapCircleAll(_newPosition, 0.001f);  // если точка находитс€ в стене, то отмен€ем действие
        foreach (Collider2D collider in colliders)
        {
            if (collider.gameObject.tag == "LeftWall" || collider.gameObject.tag == "RightWall")
            {
                _creature.IsStateEnd = true;
            }
        }
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
        //Debug.Log("walk update");

        //_creature.transform.position = Vector2.MoveTowards(_creature.transform.position, _newPosition, _speed * Time.deltaTime);
        _agent.SetDestination(_newPosition);

        if (Vector2.Distance(_creature.transform.position, _newPosition) < 0.001f)
        {
			_agent.isStopped = true;
			_creature.IsStateEnd = true;
            
        }
    }

}
