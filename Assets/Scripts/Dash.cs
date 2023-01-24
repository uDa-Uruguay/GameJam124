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

    private void Update()
    {
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

    private IEnumerator DashAction()
    {
        canDash = false;
        isDashing = true;
        tr.emitting = true;

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
        rb.velocity = new Vector2(0f, 0f);
        tr.emitting = false;
        isDashing = false;
        yield return new WaitForSeconds(dashingCooldown);
        canDash = true;
    }
}
