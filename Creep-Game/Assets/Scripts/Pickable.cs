using System;
using System.Collections;
using System.Collections.Generic;  // library for Action 
using UnityEngine;

public class Pickable : MonoBehaviour
{
    [SerializeField]
    public PickableType Pickabletype;

    private bool _isPicked = false;


    // Gunakan Action utk menghubungkan method 
    // OnPickablePicked yang ada di script PickableManager
    public Action<Pickable> OnPickedAction;
    
    private void OnTriggerEnter(Collider other)
    {
        if (_isPicked) return;  // already picked once

        if (other.gameObject.CompareTag("Player")) 
        {
            _isPicked = true;
            Debug.Log("Picked up ontrigger enter: " + this);
            // panggil method OnPickablePicked yang ada di script PickableManager
            //OnPickedAction(this);
            OnPickedAction?.Invoke(this);
            Debug.Log("Picked up ........: " + this);

        }
        
    }

  
}
