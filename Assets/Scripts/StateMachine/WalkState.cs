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
    }

    public override void Enter()
    {
        base.Enter();
        Debug.Log("walk enter");
        _speed = _creature.speed;
        _creature.Animator.SetFloat("Speed", _speed);
        _newPosition = new Vector2(_creature.transform.position.x + Random.Range(-7f, 7f), _creature.transform.position.y + Random.Range(-7f, 7f));
        
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

        if (_creature.transform.position == _newPosition)
        {
            _creature.IsStateEnd = true;
        }
    }

}
