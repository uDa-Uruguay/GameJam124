using UnityEngine;

public class Dash : MonoBehaviour
{
    private float _horizontal;
    private float _vertical;

    [SerializeField]
    private float _dashSpeed = 10f;
    [SerializeField]
    private float _movemenentRange = 15f;
    [SerializeField]
    private float _cooldown = 2f;
    private float _nextDash = 0f;


    void FixedUpdate()
    {
        //Input del teclado
        _horizontal = Input.GetAxisRaw("Horizontal");
        _vertical = Input.GetAxisRaw("Vertical");

        KeyCode dashK = KeyCode.Space;

        if (_horizontal != 0 && Input.GetKeyDown(dashK) && Time.time > _nextDash)
        {
            this.transform.Translate(new Vector2(_movemenentRange, 0) * Time.deltaTime * _dashSpeed * _horizontal);
            _nextDash = Time.time + _cooldown;
        }
        else if (_vertical != 0 && Input.GetKey(dashK) && Time.time > _nextDash)
        {
            this.transform.Translate(new Vector2(0, _movemenentRange) * Time.deltaTime * _dashSpeed * _vertical);
            _nextDash = Time.time + _cooldown;
        }
    }
}
