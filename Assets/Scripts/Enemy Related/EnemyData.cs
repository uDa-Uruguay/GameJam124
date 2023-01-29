using System.Collections;
using UnityEngine;

public class EnemyData : MonoBehaviour
{
    [Header("Stats")]
    [SerializeField] private Enemy _enemyData;
    [SerializeField] public float _health;
    [SerializeField] public float _attackSpeed;
    [SerializeField] public float _movementSpeed;
    [SerializeField] public float _damage;
    [SerializeField] public int _score;

    [SerializeField] public SpriteRenderer spriteRenderer;

    // Esto asegura que no se spamee el ataque, solo atacar� segun su velocidad de ataque (en segundos). Se actualiza y comienza una vez dentro del area de jugador.
    private float canAttack = 0;


    [SerializeField] public bool startBehaviors = false;

    [Header("Animations")]
    [SerializeField] private AnimationSprites spawnSprites;
    private Sprite[] _spawnAnimation;

    [Header("Taking damage feedback")]
    [SerializeField] private Color damagedColor;
    [SerializeField] private float timeDamagedColor;

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

        if (spawnSprites)
        {
            _spawnAnimation = spawnSprites.SpriteList;
            StartCoroutine(spawnAnimation());
        }
        else Debug.Log("Enemy spawn animation wasn't set");

    }

    private void Update()
    {
        // Se le va sumando el tiempo para ir comprobando si puede o no atacar.
        canAttack += Time.deltaTime;

        if (_health <= 0)
        {
            GameEvents.current.EnemyDyingPoints(_score);
            GameEvents.current.EnemyDying();
            Destroy(this.gameObject);
        }
    }


    private void OnCollisionStay2D(Collision2D collision)
    {
        // Si aun no termino de spawnea, no har� da�o.
        if (!startBehaviors) return;
        // Hace da�o al player
        if (collision.gameObject.tag == "Player" && _attackSpeed <= canAttack)
        {
            PlayerHealth health = collision.gameObject.GetComponent<PlayerHealth>();
            GameEvents.current.PlayerTakingDamage();
            health.TakeDamage(_damage);
            canAttack = 0f;
        }
    }


    public void TakingDamage(float damage)
    {
        _health -= damage;

        StartCoroutine(takingDamageColor());
    }

    private IEnumerator takingDamageColor()
    {
        spriteRenderer.color = damagedColor;
        yield return new WaitForSeconds(0.3f);
        spriteRenderer.color = Color.white;
    }

    private IEnumerator spawnAnimation()
    {
        for (int i = 0; i < _spawnAnimation.Length; i++)
        {
            spriteRenderer.sprite = _spawnAnimation[i];
            yield return new WaitForSeconds(1f / spawnSprites.FramesPerSecond);
        }
        startBehaviors = true;
    }
}
