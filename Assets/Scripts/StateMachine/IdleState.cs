using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IdleState : State
{
    private Creature _creature;
    private float _timer;

    public IdleState(Creature creature)
    {
        _creature = creature;
    }

    public override void Enter()
    {
        base.Enter();
        Debug.Log("idle enter");
        _timer = Random.Range(5, 10);
    }

    public override void Exit()
    {
        base.Exit();
        //Debug.Log("idle exit");
    }

    public override void Update()
    {
        if (_timer > 0)
        {
            _timer -= Time.deltaTime;
        }
        else
        {
            _creature.IsStateEnd = true;
        }
    }
}
