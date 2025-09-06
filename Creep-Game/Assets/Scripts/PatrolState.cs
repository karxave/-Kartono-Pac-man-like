using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PatrolState : BaseState
{
    private bool _isMoving;
    private Vector3 _destination;

    public void EnterState(Enemy enemy)
    {

        _isMoving = false;
        // masuk ke state : EnterState di PatrolState,
        // jadi buat enemy stop bergerak
        // enemy tidak bergerak dulu menunggu instruksi berikutnya
        // instruksi berikutnya terjadi ketika UpdateState
        //     _isMoving = false;

        //     Debug.Log("Enter State : Patrol State");

        //     enemy.EnemyAnimator.SetTrigger("PatrolStateParameter");

        if (enemy.EnemyAnimator != null)
        {
            enemy.EnemyAnimator.SetTrigger("PatrolStateParameter");
        }


    }

    public void UpdateState(Enemy enemy)
    {
   
        //cek  jarak enemy dan player , kalo kurang dari ChaseDistance berarti SwitchState ke ChaseState
        if (Vector3.Distance(enemy.transform.position, enemy.Player.transform.position) < enemy.ChaseDistance)
        {
            enemy.SwitchState(enemy.ChaseState);
        }

        // cek juga enemy sedang bergerak atau sedang stop (tidak bergerak)
        if (!_isMoving)
        {
            //   ternyata enemy sedang stop, jadi buat enemy bergerak lagi
            _isMoving = true;


            // enemy akan bergerak ke waypoint
            // tentukan waypoint mana yang akan dituju secara Random/acak
            int index = UnityEngine.Random.Range(0, enemy.Waypoints.Count);


            // masukan waypoint baru yang dengan transform position sbg destination baru
            _destination = enemy.Waypoints[index].position;

            // update lagi destination NavMeshAgent
            enemy.NavMeshAgent.destination = _destination;

            enemy.NavMeshAgent.SetDestination(_destination);

        }
        else
        {
            // ternyata enemy sedang bergerak
            // cek apakah enemy sudah sampai ke waypoint tujuan atau belum
            // bandingkan jarak antara titik akhir dengan posisi current enemy
            // jika jaraknya sudah atau kurang dari 0.1f berarti sudah sampe
            // jika sudah sampe , enemy harus stop moving
            if (Vector3.Distance(_destination, enemy.transform.position) <= 0.1f)
            {
                _isMoving = false;
            }
        }
    }
    public void ExitState(Enemy enemy)
    {
        Debug.Log("ExitState : Stop Patrol");

    }
}