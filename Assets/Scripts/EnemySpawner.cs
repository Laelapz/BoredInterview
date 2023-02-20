using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private Dictionary<string, Queue<GameObject>> _poolOfEnemies;
    [SerializeField] private List<Pool> _pools;

    private Camera _camera;
    private int _numberOfEnemiesInstantiated;
    private float _currentTimeWaitedBetweenSpawn = 0f;
    private GameManager _gameManager;

    public float TimeBetweenSpawn = 0.3f;
    public int MaxEnemiesOnScreen = 10;

    [System.Serializable]
    public class Pool
    {
        public string tag;
        public GameObject prefab;
        public int size;
    }

    #region Singleton

    public static EnemySpawner Instance;

    private void Awake()
    {
        Instance = this;
        _gameManager = GetComponent<GameManager>();
    }

    #endregion Singleton

    private void Start()
    {
        _camera = Camera.main;

        _poolOfEnemies = new Dictionary<string, Queue<GameObject>>();

        foreach(Pool pool in _pools)
        {
            Queue<GameObject> objectPool = new();

            for(int i = 0; i < pool.size; i++)
            {
                GameObject obj = Instantiate(pool.prefab);
                obj.SetActive(false);
                objectPool.Enqueue(obj);
            }

            _poolOfEnemies.Add(pool.tag, objectPool);
        }
    }

    public void DeSpawnEnemy()
    {
        if (_numberOfEnemiesInstantiated == 0) return;
        _numberOfEnemiesInstantiated -= 1;
    }

    public GameObject SpawnFromPool(string tag, Vector3 position, Quaternion rotation)
    {

        if (!_poolOfEnemies.ContainsKey(tag)) return null;
        
        GameObject objectToSpawn = _poolOfEnemies[tag].Dequeue();

        objectToSpawn.SetActive(true);
        objectToSpawn.transform.SetPositionAndRotation(position, rotation);

        _poolOfEnemies[tag].Enqueue(objectToSpawn);
        return objectToSpawn;
    }

    void Update()
    {
        if(_numberOfEnemiesInstantiated < MaxEnemiesOnScreen && _currentTimeWaitedBetweenSpawn <= 0)
        {
            Vector3 pos;
            RaycastHit2D[] hits;

            do
            {
                pos = DecideSideToSpawn();
                hits = Physics2D.CircleCastAll(pos, 0.1f, Vector2.zero);

            }
            while (hits.Length > 0);

            pos.z = 0;

            GameObject enemyGameObject;

            if (Random.Range(0, 3) > 1)
            {
                enemyGameObject = enemyGameObject = SpawnFromPool("Chaser", pos, Quaternion.identity);
            }
            else
            {
                enemyGameObject = SpawnFromPool("Shooter", pos, Quaternion.identity);
            }

            //enemyGameObject.GetComponent<HealthHandler>().OnDead += _gameManager.IncreasePoints;
            enemyGameObject.GetComponent<HealthHandler>()._onDead.AddListener(_gameManager.IncreasePoints);

            _numberOfEnemiesInstantiated += 1;
            _currentTimeWaitedBetweenSpawn = TimeBetweenSpawn;
        }

        _currentTimeWaitedBetweenSpawn -= Time.deltaTime;
    }

    private Vector3 DecideSideToSpawn()
    {
        if (Random.Range(0, 3) > 1)
        {
            if (Random.Range(0, 3) > 1)
            {
                return _camera.ViewportToWorldPoint(new Vector3(1.1f, Random.Range(0, 1.1f), 0));
            }
            else
            {
                return _camera.ViewportToWorldPoint(new Vector3(-0.1f, Random.Range(0, 1.1f), 0));
            }
        }
        else
        {
            if (Random.Range(0, 3) > 1)
            {
                return _camera.ViewportToWorldPoint(new Vector3(Random.Range(0, 1.1f), 1.1f, 0));
            }
            else
            {
                return _camera.ViewportToWorldPoint(new Vector3(Random.Range(0, 1.1f), -0.1f, 0));
            }
        }
    }
}
