using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SaveTheSystem
{
    public class Generator : MonoBehaviour
    {
        public static Generator Instance;
       
        [Header("Spawn")]
        [SerializeField] private Enemy _enemyPrefab;
        [SerializeField] private List<EnemyData> _enemiesData;
        [Space(5)]
        [SerializeField] private Transform _centerPoint;
        [SerializeField] private float _radius = 10f;
        [Space(5)]
        [SerializeField] private int _minEnemy = 1;
        [SerializeField] private int _maxEnemy = 3;
        private int _currentEnemy = 0;
        [Space(5)]
        private float _enemySpawnFrequency = 1.0f;
        private float _enemySpawnTimer = 0.0f;



        [Header("Difficulty")]
        [SerializeField] private float _timeToReachMaxSpeed = 300f;    // 5 minutes


        [Header("EDITOR")]
        [SerializeField] private bool ShowGizmos = false;


        // Cached
        private GameManager _gameManager;

        #region Properties
        public Transform Target { get => _centerPoint; }
        #endregion

        private void Awake()
        {
            Instance = this;
        }


        private void OnEnable()
        {
            Enemy.OnEnemyDeath += () =>
            {
                _currentEnemy--;
            };
        }

        private void OnDisable()
        {
            Enemy.OnEnemyDeath -= () =>
            {
                _currentEnemy--;
            };
        }


        private void Start()
        {
            _gameManager = GameManager.Instance;
        }


        private void Update()
        {
            if (GameplayManager.Instance.CurrentState != GameplayManager.GameState.PLAYING) return;

            if(Time.time - _enemySpawnTimer > _enemySpawnFrequency)
            {
                _enemySpawnTimer = Time.time;

                if (_gameManager.Score < 1000)
                {
                    if (_currentEnemy < 1)
                    {
                        SpawnEnemy(_enemiesData[0]);
                        _currentEnemy++;
                    }
                }
                else if (_gameManager.Score < 2000)
                {
                    if (_currentEnemy < 2)
                    {
                        SpawnEnemy(_enemiesData[1]);
                        _currentEnemy++;
                    }
                }
                else if (_gameManager.Score < 4000)
                {
                    if (_currentEnemy < 3)
                    {
                        SpawnEnemy(_enemiesData[2]);
                        _currentEnemy++;
                    }
                }
                else
                {
                    if (_currentEnemy < 3)
                    {
                        SpawnEnemy(_enemiesData[Random.Range(0, _enemiesData.Count)]);
                        _currentEnemy++;
                    }
                }             
            } 
        }


        #region Generate
        private Enemy SpawnEnemy(EnemyData enemyData)
        {
            Vector2 position = GetRandomPositionOnCircleEdge(_centerPoint.position, _radius);
            Enemy enemy = Instantiate(_enemyPrefab, position, Quaternion.identity);
            enemy.LoadEnemyData(enemyData);
            return enemy;
        }

        #endregion


        private Vector2 GetRandomPositionOnCircleEdge(Vector2 center, float radius)
        {
            // Generate a random angle in radians
            float randomAngle = Random.Range(0f, Mathf.PI * 2f);

            // Calculate the x and y coordinates using polar coordinates
            float x = center.x + radius * Mathf.Cos(randomAngle);
            float y = center.y + radius * Mathf.Sin(randomAngle);

            // Return the random position
            return new Vector2(x, y);
        }




        private void OnDrawGizmos()
        {
            if(ShowGizmos)
            {
                Gizmos.DrawWireSphere(_centerPoint.position, _radius);  
            }
        }
    }
}