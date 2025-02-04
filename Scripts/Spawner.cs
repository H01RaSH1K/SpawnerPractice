using System.Collections;
using UnityEngine;
using UnityEngine.Pool;

public class Spawner : MonoBehaviour
{
    [SerializeField] private Mover _enemyPrefab;
    [SerializeField] private int _enemyPoolCapacity = 20;
    [SerializeField] private int _enemyPoolMaxSize = 200;
    [SerializeField] private float _enemySpawnInterval = 2f;
    [SerializeField] private Transform _target;

    private WaitForSeconds _waitForSpawn;
    private ObjectPool<Mover> _enemyPool;

    private void Awake()
    {
        _enemyPool = new ObjectPool<Mover>(
            createFunc: () => Instantiate(_enemyPrefab),
            actionOnGet: InitializeEnemy,
            actionOnRelease: (enemy) => enemy.gameObject.SetActive(false),
            actionOnDestroy: (enemy) => Destroy(enemy),
            collectionCheck: true,
            defaultCapacity: _enemyPoolCapacity,
            maxSize: _enemyPoolMaxSize
        );

        _waitForSpawn = new WaitForSeconds(_enemySpawnInterval);
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
        while (enabled)
        {
            _enemyPool.Get();

            yield return _waitForSpawn;
        }
    }

    private void InitializeEnemy(Mover enemy)
    {
        enemy.SetTarget(_target);
        enemy.gameObject.SetActive(true);
        enemy.transform.position = transform.position;
    }
}
