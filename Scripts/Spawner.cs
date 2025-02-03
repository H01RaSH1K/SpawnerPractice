using System.Collections;
using UnityEngine;
using UnityEngine.Pool;

public class Spawner : MonoBehaviour
{
    [SerializeField] private Enemy _enemyPrefab;
    [SerializeField] private int _enemyPoolCapacity = 20;
    [SerializeField] private int _enemyPoolMaxSize = 200;
    [SerializeField] private float _enemySpawnRate = 0.5f;

    private WaitForSeconds _waitForSpawn;
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

        _waitForSpawn = new WaitForSeconds(1 / _enemySpawnRate);
    }

    private void OnEnable()
    {
        StartCoroutine(SpawnCoroutine());
    }

    private void OnDisable()
    {
        StopAllCoroutines();
    }

    private IEnumerator SpawnCoroutine()
    {
        while (true)
        {
            _enemyPool.Get();
            yield return _waitForSpawn;
        }
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
