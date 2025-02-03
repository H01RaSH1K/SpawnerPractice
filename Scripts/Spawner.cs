using UnityEngine;
using UnityEngine.Pool;

public class Spawner : MonoBehaviour
{
    [SerializeField] private Enemy _enemyPrefab;
    [SerializeField] private int _enemyPoolCapacity = 20;
    [SerializeField] private int _enemyPoolMaxSize = 200;
    [SerializeField] private float _enemySpawnRate = 2f;

    private ObjectPool<Enemy> _enemyPool;

    private void Awake()
    {
        _enemyPool = new ObjectPool<Enemy>(
            createFunc: () => Instantiate(_enemyPrefab),
            actionOnGet: InitializeEnemy,
            actionOnRelease: (enemy) => enemy.gameObject.SetActive(false),
            actionOnDestroy: (enemy) => Destroy(enemy),
            collectionCheck: true,
            defaultCapacity: _enemyPoolCapacity,
            maxSize: _enemyPoolMaxSize
        );
    }

    private void OnEnable()
    {
        InvokeRepeating(nameof(GetEnemy), 0f, _enemySpawnRate);
    }

    private void OnDisable()
    {
        CancelInvoke();
    }

    private void GetEnemy()
    {
        _enemyPool.Get();
    }

    private void InitializeEnemy(Enemy enemy)
    {
        enemy.Initialize(GetRandomHorizontalEnemyDirection());
        enemy.gameObject.SetActive(true);
        enemy.transform.position = transform.position;
    }

    private Vector3 GetRandomHorizontalEnemyDirection()
    {
        float randomRange = 1;
        float y = 0;
        float x = Random.Range(-randomRange, randomRange);
        float z = Random.Range(-randomRange, randomRange);

        return new Vector3(x, y, z);
    }
}
