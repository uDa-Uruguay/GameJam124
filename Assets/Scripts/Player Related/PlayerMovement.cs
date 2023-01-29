using System.Collections;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    // Para extraer SpriteRenderer
    [SerializeField] GameObject PlayerSprite;
    private SpriteRenderer sp;
    private SpriteCointainerManager spriteContainer;

    // Rigid Body del personaje
    Rigidbody2D _ridigBody;

    // Variables de movimiento
    private float _horizontal;
    private float _vertical;
    private Vector2 _direction;
    private Vector2 _movementVector;
    [SerializeField] private float _playerMovment = 5f;

    // Check de ir en diagonal
    private bool movingDiagonally = false;
    
    
    private void Start()
    {
        _ridigBody = this.GetComponent<Rigidbody2D>();

        sp = PlayerSprite.GetComponent<SpriteRenderer>();
        spriteContainer = PlayerSprite.GetComponent<SpriteCointainerManager>();
    }

    // Update es mejor para obtener input ya que va frame por frame
    private void Update()
    {
        //Input del teclado
        _horizontal = Input.GetAxisRaw("Horizontal");
        _vertical = Input.GetAxisRaw("Vertical");
     

        _direction = new Vector2(_horizontal, _vertical);
    }
    void FixedUpdate()
    {
        SetSprites(movingDiagonally);

        // Si hay movimiento de ambas direcciones, quiere decir que va en diagonal.
        if (_direction.x != 0 && _direction.y != 0) movingDiagonally = true;
        else movingDiagonally = false;

        //Movement(movingDiagonally);
        _movementVector = ((Vector2)transform.position + (_direction.normalized * Time.deltaTime * _playerMovment));
        _ridigBody.MovePosition(_movementVector);

        // Invierte el sprite segun hacia donde se mueva
        if (_horizontal < 0) sp.flipX = true;
        if(_horizontal > 0) sp.flipX = false;
        
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
