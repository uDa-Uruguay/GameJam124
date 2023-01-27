using System.Collections;
using UnityEngine;

// AGREGAR COMENTARIOS.
public class Dash : MonoBehaviour
{
    private float _horizontal;
    private float _vertical;
    private bool isDiagonally = false;

    [SerializeField] private bool canDash = true;
    public bool isDashing = false;
    [SerializeField] private float dashingPower = 25f;
    [SerializeField] private float dashingTime = 0.3f;
    [SerializeField] private float dashingCooldown = 1f;


    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private TrailRenderer tr;

    // Elementos para evitar que se pierda vida ni se colisione.
    private PlayerHealth playerHealth;

    private void Start()
    {
        playerHealth = this.GetComponent<PlayerHealth>();

        GameEvents.current.onDashBought += EnableDash;
    }

    private void Update()
    {
        if (!CurrentStats.current.haveDash) canDash = false;

        // Obtiene info del movimiento
        _horizontal = Input.GetAxisRaw("Horizontal");
        _vertical = Input.GetAxisRaw("Vertical");

        // Chequea que no este moviendose en horizontal.
        if (_horizontal != 0 && _vertical != 0) isDiagonally = true;
        else isDiagonally = false;

        if (isDashing || !canDash) return;

        if (_horizontal == 0 && _vertical == 0) return;
        else if (Input.GetKey(KeyCode.Space)) StartCoroutine(DashAction());
    }

    private void EnableDash()
    {
        canDash = true;
    }

    private IEnumerator DashAction()
    {
        canDash = false;
        isDashing = true;
        tr.emitting = true;

        playerHealth.isInvulnerable = true; // Evita que le quite vida

        Physics2D.IgnoreLayerCollision(6, 8, true); // Evita que colisiones con enemigos

        if (_horizontal != 0 && !isDiagonally)
        {
            rb.velocity = new Vector2(_horizontal * dashingPower, 0f);
            //Debug.Log("Dashing horizontal");
        } else if (_vertical != 0 && !isDiagonally)
        {
            rb.velocity = new Vector2(0f, _vertical * dashingPower);
            //Debug.Log("Dashing vertical");
        } else if (isDiagonally)
        {
            rb.velocity = new Vector2((_horizontal * dashingPower) / 2, (_vertical * dashingPower)/2);
            //Debug.Log("Dashing diagonally");
        }
        yield return new WaitForSeconds(dashingTime);

        // Reset de todo
        rb.velocity = new Vector2(0f, 0f);
        tr.emitting = false;
        isDashing = false;

        playerHealth.isInvulnerable = false;

        Physics2D.IgnoreLayerCollision(6, 8, false);

        yield return new WaitForSeconds(dashingCooldown);
        canDash = true;
    }
}
