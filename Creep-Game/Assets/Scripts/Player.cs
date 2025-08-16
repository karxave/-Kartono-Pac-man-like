using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField]
    private float _speed;
    [SerializeField]
    private Camera _camera;

    private Rigidbody _rigidBody;

    private void Awake()
    {
        //inisialisasi/akses komponent _rigidBody gunakan GetComponent<>()
        _rigidBody = GetComponent<Rigidbody>();

        HideAndLockCursor();
    }
        
    private void HideAndLockCursor()
    {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }

    // Update is called once per frame
    void Update()
    {
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");

        //atur  pergerakan sesuai dengan arah depan kamera
        Vector3 horizontalDirection = horizontal * _camera.transform.right;  // sumbu x
        Vector3 verticalDirection = vertical * _camera.transform.forward; // sumbu z
        verticalDirection.y = 0;
        horizontalDirection.y = 0;

        // definisikan arah pergerakan , simpan di variabel movementDirection
        //Vector3 movementDirection = new Vector3(horizontal, 0, vertical);
        // dengan sudah diaturnya kamera,
        // maka movementDirection = horizontalDirection + verticalDirection
        Vector3 movementDirection = horizontalDirection + verticalDirection;

        //akses komponen Rigidbody bagian velocity utk menggerakan Player
        // karena physical gunakan Time.fixedDeltaTime
        _rigidBody.velocity = movementDirection * _speed * Time.fixedDeltaTime;
            
    }
}
