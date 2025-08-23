using System;
using System.Collections;
using System.Collections.Generic;  // library for Action 
using UnityEngine;

public class Pickable : MonoBehaviour
{
    [SerializeField]
    public PickableType variablePickabletype;

    // Gunakan Action utk menghubungkan method 
    // OnPickablePicked yang ada di script PickableManager
    public Action<Pickable> OnPickedAction;
    
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player")) 
        { 
            // Debug.Log("Picked up : " + variablePickabletype);
            // panggil method OnPickablePicked yang ada di script PickableManager
            OnPickedAction(this);
            Destroy(gameObject);
        }
        
    }

  
}
