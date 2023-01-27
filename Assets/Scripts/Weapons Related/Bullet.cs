using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Bullet : MonoBehaviour
{
    [SerializeField] private GameObject boxCollider;

    // Datos a settear.
    private Vector3 mousePos; // Posicion del mouse
    private Camera mainCam;
    private Rigidbody2D rd2D;

    private GameObject playerGO;
    [SerializeField] private float _knockbackForce;

    private Vector3 direction;
    private Vector3 rotation;

    // Valores de daño y el tiempo en que demora en desaparecer.
    private GameObject weapon;
    public WeaponData weaponInfo;
    private float force;
    private float damage;

    [SerializeField] private bool disappearEnable = false;
    [SerializeField] private float timeBeforeDisappear = 1.5f;

    [SerializeField] private float timeBeforeStopping;

    [Header("Others")]
    [SerializeField] public bool isCollectable;
    private bool attackEnemy = true;


    void OnEnable()
    {
        playerGO = GameObject.FindGameObjectWithTag("Player");

        // Al aparecer en pantalla, comienza la corrutina. Toma como dato el tiempo en que demorara en desaparecer.
        if (disappearEnable) StartCoroutine(automaticDestroy(timeBeforeDisappear));
        if (isCollectable) StartCoroutine(createBoxCollision());

        // Setteo.
        mainCam = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
        rd2D = this.GetComponent<Rigidbody2D>();
        mousePos = mainCam.ScreenToWorldPoint(Input.mousePosition); // Toma posición de pantalla y la transforma en posición x,y

        // Setteo de valores.
        weapon = GameObject.FindGameObjectWithTag("Weapon");
        weaponInfo = weapon.GetComponent<WeaponData>();
        force = weaponInfo.force;
        damage = weaponInfo.damage;

        // Sale en dirección que toque el mouse

        direction = mousePos - transform.position;
        rotation = transform.position - mousePos; // No entiendo.

        rd2D.velocity = new Vector2(direction.x, direction.y).normalized * force; // Normalized lo convierte en valores de 1, tomando en cuenta solo direccion o posicionamiento.

        // Controla la rotacion.
        float rot = Mathf.Atan2(rotation.y, rotation.x) * Mathf.Rad2Deg; // Toma valores 'x' e 'y' para sacar el angulo en 'radians', luego se pasa a grados.
        transform.rotation = Quaternion.Euler(0, 0, rot + 90);

        StartCoroutine(automaticStop());

    }

  
    // Acciones al hacer colisi�n.
    private void OnCollisionEnter2D(Collision2D collision)
    {
        // Recibe da�o el enemigo
        if (collision.gameObject.tag == "Enemy" && attackEnemy)
        {
            // Extrae info del enemigo
            EnemyData enemy = collision.gameObject.GetComponent<EnemyData>();
            KnockbackForce knockback = collision.gameObject.GetComponent<KnockbackForce>();

            if(knockback) knockback.PlayFeedbackFromPlayer();
            // Modifica la vida enemiga y de tener 0 o menos, elimina al objetivo.
            enemy.TakingDamage(damage);
        } 
    }

    // Corrutina para eleminar automaticamente balas.
    private IEnumerator automaticDestroy(float interval)
    {
        yield return new WaitForSeconds(interval);
        Destroy(gameObject);
    }

    // Detiene movimiento de la bala/flecha
    private IEnumerator automaticStop()
    {
        yield return new WaitForSeconds(timeBeforeStopping);
        rd2D.velocity = Vector3.zero;
        attackEnemy = false;
    }

    // Permite las colisiones con el player
    private IEnumerator createBoxCollision()
    {
        yield return new WaitForSeconds(0.5f);
        GameObject.Instantiate(boxCollider, this.gameObject.transform);
    }

}
