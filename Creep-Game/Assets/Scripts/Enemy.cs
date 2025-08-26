using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{   
    [SerializeField]
    public List<Transform> Waypoints = new List<Transform>();

    private BaseState _currentState;

    [HideInInspector]
    public PatrolState PatrolState = new PatrolState();
    [HideInInspector]
    public ChaseState ChaseState = new ChaseState();
    [HideInInspector]
    public RetreatState RetreatState = new RetreatState();
    [HideInInspector]
    public NavMeshAgent NavMeshAgent;

    private void Awake()
    {
        // State awal enemy adalah patrol state.
        _currentState = PatrolState;
        _currentState.EnterState(this);

        // init NavmeshAgent dari gameobject Enemy
        NavMeshAgent = GetComponent<NavMeshAgent>();

    }


    private void Update()
    {
        if (_currentState != null)
        {
            _currentState.UpdateState(this);
        }
    }
}
