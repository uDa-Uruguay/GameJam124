using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    //Variables para Player

    private Rigidbody2D rd2D;

    private float _horizontal;
    private float _vertical;
    [SerializeField]
    private float _playerMovment = 5f;


    // Update is called once per frame
    void Update()
    {
 
        //Input del teclado
        _horizontal = Input.GetAxisRaw("Horizontal");
        _vertical = Input.GetAxisRaw("Vertical");
        
        //Movimiento y Velocidad
        transform.Translate(Vector3.right * Time.deltaTime * _playerMovment * _horizontal);
        transform.Translate(Vector3.up * Time.deltaTime * _playerMovment * _vertical);

    }

    private void FixedUpdate()
    {

    }


}
