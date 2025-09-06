using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RetreatState : BaseState
{
    public void EnterState(Enemy enemy)
    {
        if (enemy != null)
        {
            Debug.Log(" Enemy Start Retreat");
            enemy.EnemyAnimator.SetTrigger("RetreatStateParameter");
        }
        
    }

    public void UpdateState(Enemy enemy)
    {
       if (enemy.Player != null)
        {
            enemy.NavMeshAgent.destination = enemy.transform.position - enemy.Player.transform.position;
        }
    }

    public void ExitState(Enemy enemy)
    {
        Debug.Log("Enemy Stop Retreat");
    }
}
