using UnityEngine;

public class EnemyChase : MonoBehaviour
{
    [SerializeField] private string targetTag;
    private GameObject target;
    private EnemyData enemyData;

    private float _enemySpeed;

    private void Awake()
    {
        target = GameObject.FindGameObjectWithTag(targetTag);
        enemyData = this.GetComponent<EnemyData>();

        if (!enemyData) Debug.Log($"Component 'EnemyData' not attached to GameObject {this.name}");
        else _enemySpeed = enemyData._movementSpeed;
    }

    private void Update()
    {
        if (!enemyData.startBehaviors) return;

        if (target) // O sea, si encontró un GameObject con este tag y no dio null.
        {
            this.transform.position = Vector2.MoveTowards(this.transform.position, target.transform.position, _enemySpeed * Time.deltaTime);
        }
        else Debug.Log($"Gameobject with tag '{targetTag}' not found in scene");
    }
}
