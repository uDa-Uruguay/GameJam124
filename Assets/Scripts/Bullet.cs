using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{

    private int playerDamage = 3;

    private Vector3 mousePos;
    private Camera mainCam;
    private Rigidbody2D rd2D;
    public float force;

    public NormalEnemy normalEnemy;

    // Start is called before the first frame update
    void Start()
    {

        mainCam = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
        rd2D = GetComponent<Rigidbody2D>();
        mousePos = mainCam.ScreenToWorldPoint(Input.mousePosition);

        Vector3 direction = mousePos - transform.position;
        Vector3 rotation = transform.position - mousePos;
        rd2D.velocity = new Vector2(direction.x, direction.y).normalized * force;

        float rot = Mathf.Atan2(rotation.y, rotation.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0, 0, rot + 90);

    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log("Toco el Enemigo");
        if (collision.gameObject.tag == "Enemy")
        {
            Debug.Log(normalEnemy);
            normalEnemy.TakePlayerDamage(playerDamage);
        }

        Destroy(gameObject);

    }



    private void OnBecameInvisible()
    {
        Destroy(gameObject);
    }
}
