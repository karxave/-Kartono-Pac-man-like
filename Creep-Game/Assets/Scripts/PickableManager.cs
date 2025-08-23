using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickableManager : MonoBehaviour
{
    private List<Pickable> _pickableList = new List<Pickable>();

    // Start is called before the first frame update
    void Start()
    {
        InitPickableList();
    }

   
    private void InitPickableList()
    {   // tampung semua gameobject yang punya script Pickable
        // jadi gunakan array : Pickable[]
        // simpan array tsb dengan nama pickableObjects
        Pickable[] pickableObjects = GameObject.FindObjectsOfType<Pickable>();
        for (int i = 0; i < pickableObjects.Length; i++)
        {
            _pickableList.Add(pickableObjects[i]);

            //hubungkan OnPickablePicked dengan OnPickedAction yg ada di script Pickable
            pickableObjects[i].OnPickedAction += OnPickablePicked;

        }

     //   Debug.Log("Pickable List : " + _pickableList.Count);
    }

    
    private void OnPickablePicked(Pickable pickable)
    {   
        _pickableList.Remove(pickable);
   //     Debug.Log("Pickable List : " + _pickableList.Count);
        if (_pickableList.Count <= 0)
        {
            Debug.Log("Win");
        }
    }
}
