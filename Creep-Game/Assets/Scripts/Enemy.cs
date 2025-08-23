using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    private BaseState _currentState;

    [HideInInspector]
    public PatrolState PatrolState = new PatrolState();
    [HideInInspector]
    public ChaseState ChaseState = new ChaseState();
    [HideInInspector]
    public RetreatState RetreatState = new RetreatState();

    private void Awake()
    {
        _currentState = PatrolState;
        _currentState.EnterState(this);

    }


    private void Update()
    {
        if (_currentState != null)
        {
            _currentState.UpdateState(this);
        }
    }
}
