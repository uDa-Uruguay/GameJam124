using System.Collections;
using UnityEngine;

// AGREGAR COMENTARIOS.
public class Dash : MonoBehaviour
{
    private float _horizontal;
    private float _vertical;

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

        rb.AddForce(new Vector2(_horizontal, _vertical).normalized * dashingPower, ForceMode2D.Force);
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
