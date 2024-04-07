using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Idle2State : State
{
    private Creature _creature;
    private float _timer;

    public Idle2State(Creature creature)
    {
        _creature = creature;
    }

    public override void Enter()
    {
        base.Enter();
        Debug.Log("idle2 enter");
        _timer = Random.Range(2, 5);
        _creature.Animator.SetBool("IsIdle2", true);
    }

    public override void Exit()
    {
        base.Exit();
        _creature.Animator.SetBool("IsIdle2", false);
        //Debug.Log("idle 2 exit");
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
