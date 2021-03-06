﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoombaAttackState : State<GoombaMachine>
{
    public static GoombaAttackState Instance { get; private set; }

    static GoombaAttackState()
    {
        Instance = new GoombaAttackState();
    }


    public override void Enter(GoombaMachine entity)
    {
        entity.timer = 0.0f;
    }

    public override void Execute(GoombaMachine entity)
    {
        //DamagePlayer

        /*      var lookPos = entity.player.transform.position - entity.transform.position;
              lookPos.y = 0;
              var rotation = Quaternion.LookRotation(lookPos);
              entity.transform.rotation = Quaternion.Slerp(entity.transform.rotation, rotation, entity.rotationAttackLerp);*/

        entity.timer -= Time.deltaTime;


        if (entity.SeesPlayer())
        {
            if (!entity.IsInAttackDistance())
            {
                entity.pStateMachine.ChangeState(GoombaChaseState.Instance);
            }

            if (entity.timer <= 0)
            {
                entity.player.LoseHP(entity.attackDamage, entity.transform.forward);
                entity.timer = entity.attackCooldown;
            }
        }
        else
        {
            entity.pStateMachine.ChangeState(GoombaPatrolState.Instance);
        }
    }

    public override void Exit(GoombaMachine entity)
    {

    }
}
