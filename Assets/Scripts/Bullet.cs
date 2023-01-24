using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Bullet : MonoBehaviour
{
    // SIN REVISAR.
    private Vector3 mousePos;
    private Camera mainCam;
    private Rigidbody2D rd2D;
    public float force;


    // Valores de da�o y el tiempo en que demora en desaparecer.
    public float damage = 4f;
    [SerializeField] private float timeBeforeDisappear = 1.5f;


    void OnEnable()
    {
        // Al aparecer en pantalla, comienza la corrutina. Toma como dato el tiempo en que demorara en desaparecer.
        StartCoroutine(automaticDestroy(timeBeforeDisappear));

        // SIN REVISAR.
        mainCam = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
        rd2D = GetComponent<Rigidbody2D>();
        mousePos = mainCam.ScreenToWorldPoint(Input.mousePosition);

        Vector3 direction = mousePos - transform.position;
        Vector3 rotation = transform.position - mousePos;
        rd2D.velocity = new Vector2(direction.x, direction.y).normalized * force;

        float rot = Mathf.Atan2(rotation.y, rotation.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0, 0, rot + 90);

    }

  
    // Acciones al hacer colisi�n.
    private void OnCollisionEnter2D(Collision2D collision)
    {
        // Recibe da�o el enemigo
        if (collision.gameObject.tag == "Enemy")
        {
            // Modifica la vida enemiga y de tener 0 o menos, elimina al objetivo.
            EnemyData enemy = collision.gameObject.GetComponent<EnemyData>();
            if (enemy._health <= 0) 
            {
                Destroy(collision.gameObject);
                GameEvents.current.EnemyTakingDamage();
            }
            enemy._health -= damage;
        } 
    }

    // Corrutina para eleminar automaticamente balas.
    private IEnumerator automaticDestroy(float interval)
    {
        yield return new WaitForSeconds(interval);
        Destroy(gameObject);
    }
}
