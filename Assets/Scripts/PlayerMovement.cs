using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    //Variables para Player

    private Rigidbody2D rd2D;

    private float _horizontal;
    private float _vertical;
    [SerializeField]
    private float _playerMovment = 5f;
    //private bool facingRight = true;

    private void Start()
    {
        Transform PlayerScale = transform.Find("Player");
    }

    void FixedUpdate()
    {
        //Input del teclado
        _horizontal = Input.GetAxisRaw("Horizontal");
        _vertical = Input.GetAxisRaw("Vertical");

        //Movimiento y Velocidad
        transform.Translate(Vector3.right * Time.deltaTime * _playerMovment * _horizontal);
        transform.Translate(Vector3.up * Time.deltaTime * _playerMovment * _vertical);


        //Flip que se Bugea el RigidBodie2D

        //if (_horizontal > 0 && !facingRight)
        {
            //Flip();
        }

        //if(_horizontal < 0 && facingRight)
        {
            //Flip();
        }

    }

    //void Flip()
    //{
        //Vector3 currentScale = gameObject.transform.localScale;
        //currentScale.x *= -1;
        //gameObject.transform.localScale = currentScale;

        //facingRight = !facingRight;
    //}
}
