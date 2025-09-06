using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{   
    [SerializeField]
    public List<Transform> Waypoints = new List<Transform>();

    [SerializeField]  public float ChaseDistance;
    [SerializeField]  public Player Player;
    [SerializeField]  private AudioSource _audioSourceEnemy;

    private BaseState _currentState;

    [HideInInspector] public PatrolState PatrolState = new PatrolState();
    [HideInInspector] public ChaseState ChaseState = new ChaseState();
    [HideInInspector] public RetreatState RetreatState = new RetreatState();
    [HideInInspector] public NavMeshAgent NavMeshAgent;
    [HideInInspector] public Animator EnemyAnimator;


    private void Awake()
    {
        //init Animator 
        EnemyAnimator = GetComponent<Animator>();

        // State awal enemy adalah patrol state.
        _currentState = PatrolState;
        _currentState.EnterState(this);
   
        // init NavmeshAgent dari gameobject Enemy
        NavMeshAgent = GetComponent<NavMeshAgent>();      
    }

    private void Start()
    {
        if (Player != null)
        {
            Player.OnPowerUpStart += StartRetreating;
            Player.OnPowerUpStop += StopRetreating;
        }
    }
  public void SwitchState(BaseState state)
    {
        _currentState.ExitState(this);
        _currentState = state;
        _currentState.EnterState(this);

    }
    private void Update()
    {
        if (_currentState != null)
        {
            _currentState.UpdateState(this);

        }
    }

    public void UpdateState(Enemy enemy)
    {
        if (enemy.Player != null)
        {
                enemy.NavMeshAgent.destination = enemy.Player.transform.position;                        
        }
    }

    private void StartRetreating()
    {
         SwitchState(RetreatState);        
    }

    private void StopRetreating()
    {
        SwitchState(PatrolState);
    }

    public void EnemyIsDead()
    {        
        Destroy(gameObject);
    }

    private void OnCollisionEnter(Collision collisionObject)
    {
        if (_currentState != RetreatState)
        {
            if (collisionObject.gameObject.CompareTag("Player"))
            {                
                collisionObject.gameObject.GetComponent<Player>().PlayerIsDead();
            }
        }
    }
}
