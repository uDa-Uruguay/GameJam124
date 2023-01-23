using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    // Para extraer SpriteRenderer
    [SerializeField] GameObject PlayerSprite;
    private SpriteRenderer sp;
    private SpriteCointainerManager spriteContainer;

    // Variables de movimiento
    private float _horizontal;
    private float _vertical;
    [SerializeField] private float _playerMovment = 5f;

    // Last facing
    private bool facingLeft = false;
    // Check de ir en diagonal
    private bool movingDiagonally = false;

    private void Start()
    {
        sp = PlayerSprite.GetComponent<SpriteRenderer>();
        spriteContainer = PlayerSprite.GetComponent<SpriteCointainerManager>();
    }

    void FixedUpdate()
    {
        //Input del teclado
        _horizontal = Input.GetAxisRaw("Horizontal");
        _vertical = Input.GetAxisRaw("Vertical");

        SetSprites(movingDiagonally);

        if (_horizontal != 0 && _vertical != 0) movingDiagonally = true;
        else movingDiagonally = false;

        Movement(movingDiagonally);

        if (_horizontal < 0 || facingLeft) sp.flipX = true;

        if(_horizontal > 0)
        {
            facingLeft = false;
            sp.flipX = false;
        }
    }

    private void Movement(bool diagonally)
    {
        if (!diagonally)
        {
            transform.Translate(Vector3.right * Time.deltaTime * _playerMovment * _horizontal);
            transform.Translate(Vector3.up * Time.deltaTime * _playerMovment * _vertical);
        } else
        {
            transform.Translate((Vector3.right * Time.deltaTime * _playerMovment * _horizontal) / 2);
            transform.Translate((Vector3.up * Time.deltaTime * _playerMovment * _vertical) / 2);
        }
    }

    private void SetSprites(bool diagonally)
    {
        if (_horizontal != 0 && spriteContainer.lastSpriteSet != 0)
        {
            spriteContainer.setNewSprite(0);
            return;
        }
        else if (diagonally) return;
   
        if (_vertical > 0 && spriteContainer.lastSpriteSet != 1) spriteContainer.setNewSprite(1);
        else if (_vertical < 0 && spriteContainer.lastSpriteSet != 2) spriteContainer.setNewSprite(2);
    }
}
