using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Bullet : MonoBehaviour
{
    // Datos a settear.
    private Vector3 mousePos; // Posicion del mouse
    private Camera mainCam;
    private Rigidbody2D rd2D;


    // Valores de daño y el tiempo en que demora en desaparecer.
    [SerializeField] private float force;
    [SerializeField] private float damage = 4f;
    [SerializeField] private float timeBeforeDisappear = 1.5f;


    void OnEnable()
    {
        // Al aparecer en pantalla, comienza la corrutina. Toma como dato el tiempo en que demorara en desaparecer.
        StartCoroutine(automaticDestroy(timeBeforeDisappear));

        // Setteo.
        mainCam = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
        rd2D = this.GetComponent<Rigidbody2D>();
        mousePos = mainCam.ScreenToWorldPoint(Input.mousePosition); // Toma posición de pantalla y la transforma en posición x,y

        // Sale en dirección que toque el mouse
        Vector3 direction = mousePos - transform.position;
        Vector3 rotation = transform.position - mousePos;
        rd2D.velocity = new Vector2(direction.x, direction.y).normalized * force;

        // Controla la rotacion, no se como.
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
            enemy.TakingDamage(damage);
        } 
    }

    // Corrutina para eleminar automaticamente balas.
    private IEnumerator automaticDestroy(float interval)
    {
        yield return new WaitForSeconds(interval);
        Destroy(gameObject);
    }
}
