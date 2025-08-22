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
        // karena physical gunakan Time.deltaTime
        //_rigidBody.velocity = movementDirection * _speed * Time.deltaTime;
        // jangan pake velocity karena sumbu y akan terpengaruh ( jatuhnya player lambat)
        // pake AddForce
        //     myRigidbody.AddForce(new Vector3(0, 10, 0)); // Correct!
        // This line of code calls the AddForce method on the myRigidbody object,
        // providing a Vector3 representing an upward force.
        // You can also specify a ForceMode as a second argument for different types of force application
        // (e.g., ForceMode.Impulse, ForceMode.VelocityChange)
        _rigidBody.AddForce (movementDirection * _speed * Time.deltaTime );
            
    }
}
