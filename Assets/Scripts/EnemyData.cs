using System.Collections;
using UnityEngine;

public class EnemyData : MonoBehaviour
{
    [SerializeField] private Enemy _enemyData;
    [SerializeField] public float _health;
    [SerializeField] public float _attackSpeed;
    [SerializeField] public float _movementSpeed;
    [SerializeField] public float _damage;
    [SerializeField] public int _score;

    [SerializeField] public SpriteRenderer spriteRenderer;

    // Esto asegura que no se spamee el ataque, solo atacará segun su velocidad de ataque (en segundos)
    private float canAttack;

    [SerializeField] private Behavior[] behaviors;
    [SerializeField] private bool startBehaviors = false;

    // Animations
    [SerializeField] private AnimationSprites spawnSprites;
    private Sprite[] _spawnAnimation;

    private void Awake()
    {
        //Se extrae los datos del elemento tipo "Enemy"
        _health = _enemyData.EnemyHealth;
        _attackSpeed = _enemyData.AttackSpeed;
        _movementSpeed = _enemyData.MovementSpeed;
        _damage = _enemyData.Damage;
        _score = _enemyData.Score;

        //Se settea de tal forma que pueda dar un primer golpe sin tener que esperar.
        canAttack = _attackSpeed;

        spriteRenderer = this.GetComponent<SpriteRenderer>();
        if (spriteRenderer) spriteRenderer.sprite = _enemyData.EnemySprite;

        _spawnAnimation = spawnSprites.SpriteList;
    }

    private void Update()
    {
        if (startBehaviors)
        {
            // De tener más de un comportamiento, los activa a todos. (Hay que probar si sigue funcionando al tener mas de uno)
            for (int i = 0; i < behaviors.Length; i++)
            {
                behaviors[i].behavior(this);
            }
        }

        // Se le va sumando el tiempo para ir comprobando si puede o no atacar.
        canAttack += Time.deltaTime;

        if (_health <= 0)
        {
            Destroy(this.gameObject);
            GameEvents.current.EnemyTakingDamage(_score);
        }
    }



    private void OnCollisionStay2D(Collision2D collision)
    {
        // Hace daño al player
        if (collision.gameObject.tag == "Player" && _attackSpeed <= canAttack)
        {
            PlayerHealth health = collision.gameObject.GetComponent<PlayerHealth>();
            GameEvents.current.PlayerTakingDamage();
            health.TakeDamage(_damage);
            canAttack = 0f;
        }
    }

    private IEnumerator spawnAnimation()
    {
        for (int i = 0; i < _spawnAnimation.Length; i++)
        {
            spriteRenderer.sprite = _spawnAnimation[i];
            yield return new WaitForSeconds(1f / spawnSprites.FramesPerSecond);
        }
    }
}
